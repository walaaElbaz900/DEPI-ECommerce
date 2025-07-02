using ECommerce.Data;
using EcommerceProject.DAL.IdentityApplication;
using EcommerceProject.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EcommerceProject.DAL.UnitOfWork;
namespace EcommerceProject.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            // session for cart
            builder.Services.AddSession();

            var con = builder.Configuration.GetConnectionString("con");
            builder.Services.AddDbContext<EcommerceContext>(options => options.UseSqlServer(con));

            // Review & Wishlist(Mohamed)
            builder.Services.AddScoped<EcommerceProject.Presentation.Services.Interfaces.IWishlistService, EcommerceProject.Presentation.Services.WishlistService>();
            builder.Services.AddScoped<EcommerceProject.Presentation.Services.Interfaces.IReviewService, EcommerceProject.Presentation.Services.ReviewService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register Identity
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<EcommerceContext>(); // to make it register with my DbContext


            // to change password criteria as i need 
            //builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            //{
            //    options.Password.RequiredLength = 3;
            //    options.Password.RequireNonAlphanumeric = false; // there is alot of properties to controll the password if i need to
            //}).AddEntityFrameworkStores<EcommerceContext>(); // to make it register with my DbContext

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            // to make Authorize filter work
            app.UseAuthentication(); // Authentication => if i have cookie to enter or not

            app.UseAuthorization(); // Authorization => what is my role inside the website

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
