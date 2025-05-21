using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supermercat
{
    public class CheckOutLine
    {
        //Atributs
        private int number;
        private Queue<ShoppingCart> queue;
        private Person cashier;
        private bool active;

        /// <summary>
        /// Crea una nova instància buida de System.Collectins.Generic.Queue amb el caixer i el número associat amb els dos paràmetres del constructor
        /// </summary>
        /// <param name="responisble"></param>
        /// <param name="number"></param>
        public CheckOutLine(Person responisble, int number)
        {
            this.cashier = responisble;
            this.number = number;
            this.queue = new Queue<ShoppingCart>();
            this.active = true;
        }

        /// <summary>
        /// Retorna true en cas de que la cua tingui 0 carros
        /// </summary>
        public bool Empty 
        {
            get 
            {
                bool empty = false;
                if (this.queue.Count == 0) empty = true;
                return empty;
            } 
        }

        public Person Cashier { get { return cashier; } }

        public bool Active
        {
            get { return this.active; }
            set { this.active = value; }
        }

        /// <summary>
        /// El metode intenta fer enqueue el shoppingCart.
        /// Si el CheckOutLine no esta actiu el shopingCart no es podra fer enqueue i el metode retorna false
        /// Si el CheckOutLine esta actiu retorna true i fa enqueue
        /// </summary>
        /// <param name="oneShoppingCart"></param>
        /// <returns></returns>
        public bool CheckIn(ShoppingCart oneShoppingCart)
        {
            bool resultat = false;

            if (this.active)
            {
                this.queue.Enqueue(oneShoppingCart);
                resultat = true;
            }

            return resultat;

        }
        /// <summary>
        /// El metode fa dequeue el ShopingCart que es troba en la primera posicio (Si la linea esta activa i hi ha un ShoppingCart)
        /// </summary>
        /// <returns></returns>
        public bool CheckOut()
        {
            bool resultat = false;

            if (this.active && this.queue.Count > 0)
            {
                ShoppingCart shoppingCart = this.queue.Dequeue();

                //a) Processem els items del ShoppinCart i obtenim el total de la compra
                double total = ShoppingCart.ProcessItems(shoppingCart);

                //b) Obtenim els points associat al ShoppingCart
                int rawPoints = shoppingCart.RawPointsObtainedAtCheckout(total);

                //c) Afegim el total de la factura al cashier de la linea i al costumer que fa checkOut
                this.cashier.AddInvoiceAmount(total);
                shoppingCart.Customer.AddInvoiceAmount(total);

                //d)Afegim els punts al cashier de la linea i al costumer que esta fent CheckOut
                shoppingCart.Customer.AddPoints(rawPoints);
                this.cashier.AddPoints(rawPoints);

                //e)Posem la propietat Active del csotumer com false(ja no es troba actiu dintre el supermarket)
                shoppingCart.Customer.Active = false;

                resultat = true;
            }
            return resultat;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"NUMERO DE CAIXA --> {this.number}");
            sb.AppendLine($"CAIXER/A AL CÀREC --> {this.cashier.FullName}");
            if (this.queue.Count == 0)
            {
                sb.AppendLine("CUA BUIDA");
            }
            else
            {
                foreach (ShoppingCart carrito in this.queue)
                {
                    sb.AppendLine(carrito.ToString());
                }
            }

            return sb.ToString();
        }
    }
}