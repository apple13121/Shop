using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carts.Models.Cart
{
    public partial class Order
    {
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string RecieverName { get; set; }
        public string RecieverPhone { get; set; }
        public string RecieverAddress { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}