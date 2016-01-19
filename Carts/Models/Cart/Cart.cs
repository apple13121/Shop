using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carts.Models.Cart //Cart為購物車主要類別，來放一群CartItem
{
    [Serializable]//可序列化
    public class Cart : IEnumerable<CartItem>//購物車類別
    {
        //建構值
        public Cart()
        {
            this.cartItems = new List<CartItem>();
        }

        //儲存所有商品
        private List<CartItem> cartItems;

        /// <summary>
        /// 取得購物車內商品的總數量
        /// </summary>
        public int Count
        {
            get
            {
                return this.cartItems.Count;
            }
        }

        //取得商品總價
        public decimal TotalAmout
        {
            get
            {
                decimal totalAmout = 0.0m;
                foreach(var cartItem in this.cartItems)
                {
                    totalAmout = totalAmout + cartItem.Amout;
                }
                return totalAmout;
            }
        }

        //新增一筆product 使用productId新增
        public bool AddProduct(int ProductId)
        {
            var findItem = this.cartItems
                            .Where(s => s.Id == ProductId)
                            .Select(s => s)
                            .FirstOrDefault();

            //判斷相同Id的CartItem是否已經存在購物車內
            if(findItem == default(Models.Cart.CartItem))
            {   //不存在購物車內，則新增一筆
                using (Models.Cart.CartDBContext db = new Models.Cart.CartDBContext())
                {
                    var product = (from s in db.Products
                                   where s.Id == ProductId
                                   select s).FirstOrDefault();

                    if(product != default(Models.Cart.Product))
                    {
                        this.AddProduct(product);
                    }
                }
            }
            else
            {
                findItem.Quantity += 1;
            }
            return true;
        }

        //新增一筆Proudct，使用Proudct物件
        private bool AddProduct(Product product)
        {
            //將Product轉為CartItem
            var cartItem = new Models.Cart.CartItem()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = 1,
                DefaultImageURL = product.DefaultImageURL
            };

            //加入CartITem至購物車
            this.cartItems.Add(cartItem);
            return true;
        }

        //移除一筆Product，使用ProductId
        public bool RemoveProduct(int ProductId)
        {
            var findItem = this.cartItems
                                .Where(s => s.Id == ProductId)
                                .Select(s => s)
                                .FirstOrDefault();

            //判斷相同Id的CartItem是否已經存在購物車內
            if(findItem == default(Models.Cart.CartItem))
            {
                //不存在購物車內，不需做任何動作
            }
            else
            {
                //存在購物車內，將商品移除
                this.cartItems.Remove(findItem);
            }
            return true;
        }

        //將購物車商品轉成OrderDetail的List
        public List<Models.Cart.OrderDetail> ToOrderDetailList(int orderId)
        {
            var result = new List<Models.Cart.OrderDetail>();
            foreach (var cartItem in this.cartItems)
            {
                result.Add(new Models.Cart.OrderDetail()
                {
                    Name = cartItem.Name,
                    Price = cartItem.Price,
                    Quantity = cartItem.Quantity,
                    OrderId = orderId,
                
                });
            }
            return result;
        }

        #region IEnumerator

        IEnumerator<CartItem> IEnumerable<CartItem>.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }

        #endregion
    }
}