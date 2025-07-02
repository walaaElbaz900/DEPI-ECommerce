using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerce.Data;
using ECommerce.Models;
using Microsoft.Extensions.Hosting;
using EcommerceProject.Presentation.Services.Interfaces;

namespace EcommerceProject.Presentation.Controllers
{

    public class CarController : Controller
    {
        private readonly EcommerceContext _context;
        private readonly IReviewService _reviewService;
        private readonly IWebHostEnvironment _environment;
        public CarController(EcommerceContext context, IWebHostEnvironment environment,IReviewService reviewService)
        {
            _context = context;
            _environment = environment;
            _reviewService = reviewService;
        }

        // GET: Car
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cars.ToListAsync());
        }

        // GET: Car/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.CarID == id);
            if (car == null)
            {
                return NotFound();
            }
            ViewBag.Cars = _context.Cars.ToList();
            ViewBag.Reviews = await _reviewService.GetReviewsByCarAsync(id);
            return View(car);
        }

        // GET: Car/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("CarID,Title,Brand,Model,Year,Category,Price,RentalPricePerDay,Mileage,FuelType,TransmissionType,AvailabilityStatus,Location,CreatedAt,ImagesUrl,CarImage")] Car car)
        public async Task<IActionResult> Create(Car car)
        {
            // First validate the model without ImagesUrl
            ModelState.Remove("ImagesUrl"); // Remove ImagesUrl from validation
            car.ImagesUrl = ImagePath(car.CarImage);
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Car/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Car car)
        {
            // First validate the model without ImagesUrl
            ModelState.Remove("ImagesUrl"); // Remove ImagesUrl from validation
            ModelState.Remove("CarImage"); // Remove ImagesUrl from validation
            car.ImagesUrl = ImagePath(car.CarImage);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Car/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.CarID == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.CarID == id);
        }

        public string ImagePath(IFormFile image)
        {
            if (image != null)
            {
                string Path = _environment.WebRootPath + "/CarPics/"; // _environment.WebRootPath == wwwroot
                string FileName = image.FileName;
                // if path doesn't exist create it 
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
                FileStream fileStream = System.IO.File.Create(Path + FileName);

                image.CopyTo(fileStream);
                fileStream.Flush(); // to clear the buffer
                return FileName;
            }
            return "Error";
        }
    }
}
