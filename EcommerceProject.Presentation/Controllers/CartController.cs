using Microsoft.AspNetCore.Mvc;
using Orders_part.Extensions; // مهم علشان تقدر تستخدم SessionExtensions
using Microsoft.AspNetCore.Http;
using ECommerce.Data; // علشان HttpContext.Session

public class CartController : Controller
{
    private readonly EcommerceContext _context;

    public CartController(EcommerceContext context)
    {
        _context = context;
    }

    // عرض محتويات الكارت
    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("Cart")
                   ?? new List<CartItemViewModel>();
        return View("~/Views/Order/Index.cshtml", cart);  // تأكد من تمرير الكارت للـ View
    }

    public IActionResult AddToCart(int carId)
    {
        // استرجاع الكارت من الـ Session أو إنشاء واحد جديد إذا مش موجود
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("Cart")
                   ?? new List<CartItemViewModel>();

        // البحث عن السيارة في قاعدة البيانات
        var car = _context.Cars.FirstOrDefault(c => c.CarID == carId);

        if (car != null)
        {
            // التحقق إذا كان العنصر موجود بالفعل في الكارت
            var cartItem = cart.FirstOrDefault(c => c.CarId == carId);
            if (cartItem != null)
            {
                // زيادة الكمية إذا العنصر موجود
                cartItem.Quantity++;
            }
            else
            {
                // إضافة العنصر الجديد إلى الكارت
                cart.Add(new CartItemViewModel
                {
                    CarId = car.CarID,
                    CarName = car.Brand,
                    Price = car.Price,
                    Quantity = 1
                });
            }

            // حفظ الكارت المحدث في الـ Session
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            // يمكن إضافة رسالة تأكيد للمستخدم هنا
            TempData["Success"] = "Item added to cart successfully!";
        }
        else
        {
            // في حالة عدم العثور على السيارة في قاعدة البيانات
            TempData["Error"] = "Car not found!";
        }

        // إعادة التوجيه إلى صفحة الكارت أو صفحة المنتجات حسب الحاجة
        return RedirectToAction("Index", "Cart");
    }

    // حذف عنصر من الكارت
    public IActionResult RemoveFromCart(int carId)
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("Cart")
                   ?? new List<CartItemViewModel>();

        var cartItem = cart.FirstOrDefault(c => c.CarId == carId);
        if (cartItem != null)
        {
            cart.Remove(cartItem);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
        }

        return RedirectToAction("Index");
    }

    // تعديل الكمية
    public IActionResult UpdateQuantity(int carId, int quantity)
    {
        if (quantity <= 0)
        {
            return RedirectToAction("Index"); // يمكن تعديل الرسالة أو منعه من الوصول لهذه الصفحة
        }

        var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("Cart")
                   ?? new List<CartItemViewModel>();

        var cartItem = cart.FirstOrDefault(c => c.CarId == carId);
        if (cartItem != null)
        {
            cartItem.Quantity = quantity;
            HttpContext.Session.SetObjectAsJson("Cart", cart);
        }

        return RedirectToAction("Index");
    }

    // مسح الكارت
    public IActionResult ClearCart()
    {
        HttpContext.Session.Remove("Cart");
        return RedirectToAction("Index");
    }
}
