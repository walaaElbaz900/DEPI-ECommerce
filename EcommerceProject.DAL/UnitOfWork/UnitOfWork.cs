using ECommerce.Data;
using ECommerce.Models;
using EcommerceProject.DAL.Interfaces;
using EcommerceProject.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EcommerceContext _context;
        public IRepository<Car> Car { get; private set; }
        public IRepository<Order> Order { get; private set; }
        public IRepository<OrderDetails> OrderDetails { get; private set; }
        public IRepository<Payment> Payment { get; private set; }
        public IRepository<Promotion> Promotion { get; private set; }
        public IRepository<Review> Review { get; private set; }
        public IRepository<Shipping> Shipping { get; private set; }
        public IRepository<Wishlist> Wishlist { get; private set; }

        public UnitOfWork(EcommerceContext context)
        {
            _context = context;
            Car = new Repository<Car>(_context);
            Order = new Repository<Order>(_context);
            OrderDetails = new Repository<OrderDetails>(_context);
            Payment = new Repository<Payment>(_context);
            Promotion = new Repository<Promotion>(_context);
            Review = new Repository<Review>(_context);
            Shipping = new Repository<Shipping>(_context);
            Wishlist = new Repository<Wishlist>(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
