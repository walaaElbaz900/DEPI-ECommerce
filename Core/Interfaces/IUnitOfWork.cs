using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //IRepository<> User {  get; }
        //IRepository<> Role {  get; }
        public IRepository<Car> Car {  get; }
        public IRepository<Order> Order {  get; }
        public IRepository<OrderDetails> OrderDetails {  get; }
        public IRepository<Payment> Payment {  get; }
        public IRepository<Promotion> Promotion {  get; }
        public IRepository<Review> Review {  get; }
        public IRepository<Shipping> Shipping {  get; }
        public IRepository<Wishlist> Wishlist {  get; }

        public int Complete();
    }
}
