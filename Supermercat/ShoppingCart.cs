using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supermercat
{
    public class ShoppingCart
    {
        private Dictionary<Item, double> shoppingList;
        private Customer customer;
        private DateTime dateOfPurchase;

        public ShoppingCart(Dictionary<Item, double> shoppingList, Customer customer, DateTime dateOfPurchase, )
        {
            this.shoppingList = shoppingList;
            this.customer = customer;
            this.dateOfPurchase = dateOfPurchase;
        }
    }
}