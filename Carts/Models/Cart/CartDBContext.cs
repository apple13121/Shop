using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Carts.Models.Cart
{
    public class CartDBContext : DbContext
    {
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}