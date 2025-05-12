using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supermercat
{
    public class CheckOutLine
    {
        private int number;
        private Queue<ShoppingCart> queque;
        private Person cashier;
        private bool active;
    }
}