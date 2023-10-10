using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface ICart
    {
        public Cart Add(Cart cart, int id);//adds a cart
        public Cart QuantityUpdate(Cart cart, int id, int newAmount);//updates the ouantity
        public void OrderConfirmation(Cart cart);//saves the cart
    }
}
