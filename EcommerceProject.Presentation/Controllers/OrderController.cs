using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders_part.ViewModels;
using Orders_part.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Models;
using ECommerce.Data;
using System.Security.Claims;

namespace Orders_part.Controllers
{
    public class OrderController : Controller
    {
        private readonly EcommerceContext _context;
        private readonly ILogger<OrderController> _logger;

        public OrderController(
            EcommerceContext context,
            ILogger<OrderController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Checkout(int CarId)
        {
            // استرجاع محتويات الكارت من الـ Session
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("Cart");

            // إذا كان الكارت فارغًا، يتم إعادة توجيه المستخدم مباشرة إلى الـ Checkout
            if (cart == null || !cart.Any())
            {
                // قد ترغب في إضافة رسالة تنبيه للمستخدم هنا
                TempData["Error"] = "Your cart is empty, you are being redirected to checkout.";

                // توجيه مباشرة إلى صفحة الـ Checkout
                return View("Checkout", new CheckoutViewModel());
            }

            var address = User.FindFirst("UserAddress")?.Value;
            var phoneNumber = User.FindFirst("PhoneNumber")?.Value;


            CheckoutViewModel checkoutViewModel = new CheckoutViewModel();
            checkoutViewModel.Address = address;
            checkoutViewModel.PhoneNumber = phoneNumber; 


            // إذا كان الكارت يحتوي على عناصر، قم بتحميل الـ Checkout page بشكل طبيعي
            return View(checkoutViewModel);
        }



        // POST: PlaceOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(CheckoutViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Checkout", model);
                }

                var cart = HttpContext.Session.GetObjectFromJson<List<OrderItemViewModel>>("Cart");
                if (cart == null || !cart.Any())
                {
                    TempData["Error"] = "Your cart is empty";
                    return RedirectToAction("Index", "Cart");
                }
                string id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

               // For now, we will bypass user verification
               // Create a new order
               var order = new Order
                {
                    UserId = id,  // Assuming user is not authenticated
                    CreatedAt = DateTime.UtcNow,
                    TotalAmount = cart.Sum(c => c.Price * c.Quantity),
                    //ShippingAddress = model.Address,
                    //PhoneNumber = model.PhoneNumber,

                    OrderDetails = cart.Select(c => new OrderDetails
                    {
                        CarID = c.CarId,
                        UnitPrice = c.Price,
                        Quantity = c.Quantity
                    }).ToList()
                };

                // Add the order to the database and save changes
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                // Clear the cart session after the order is placed
                HttpContext.Session.Remove("Cart");

                // Redirect to the success page
                return RedirectToAction("OrderSuccess");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error placing order");
                TempData["Error"] = "An error occurred while processing your order";
                return View("Checkout", model);
            }
        }

        // OrderSuccess: Display order success page
        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}
