using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Supermercat
{
    public class ShoppingCart
    {
        private Dictionary<Item, double> shoppingList;
        private Customer customer;
        private DateTime dateOfPurchase;

        /// <summary>
        /// Crea un carro de la compra associat al client que el porta i amb la data d'obtenció d'aquest.
        /// </summary>
        /// <param name="customer">client conductor</param>
        /// <param name="dateOfPurchase">data de obtenció</param>
        public ShoppingCart(Customer customer, DateTime dateOfPurchase)
        {
            this.shoppingList = new Dictionary<Item, double>();
            customer.Active = true;
            this.customer = customer;
            this.dateOfPurchase = dateOfPurchase;
        }

        public Dictionary<Item, double> ShoppingList { get{ return this.shoppingList; } }

        public Customer Customer { get { return this.customer; } }

        public DateTime DateOfPurchase { get { return this.dateOfPurchase; } }

        /// <summary>
        /// Afegeix o modifica la qüantitat del producte específicat dintre del ShoppingCart, no permet qty amb decimals
        /// si el producte no és de tipus Kg
        /// </summary>
        /// <param name="item">item al qüal afegir-hi</param>
        /// <param name="qty">qüantitat a afegir-hi</param>
        public void AddOne(Item item, double qty)
        {
            if (item.PackagingType != Item.Packaging.Kg & qty != Math.Floor(qty))
            {
                throw new ArgumentException("Qüantitat incompatible amb el tipus de empaquetatge del item");
            }
            else
            {
                if (ShoppingList.ContainsKey(item)) { ShoppingList[item] += qty; }
                else { this.ShoppingList.Add(item, qty); }
            }
        }

        /// <summary>
        /// Afegeix una qüantitat random d'items a la llista de la compra de forma aleatoria tant els items com la qüantitat
        /// en el cas de que l'item sigui de PackagingType kg pot posar qüantitats double.
        /// </summary>
        /// <param name="warehouse">Items disponibles</param>
        public void AddAllRandomly(SortedDictionary<int, Item> warehouse)
        { 
            Random r = new Random();
            int qtyItems = r.Next(1,11);
            for (int i = 1; i <= qtyItems; i++)
            {
                Item itemAAfegir = warehouse[r.Next(1, warehouse.Count+1)];
                while (ShoppingList.ContainsKey(itemAAfegir) && ShoppingList != null)
                {
                    itemAAfegir = warehouse[r.Next(1, warehouse.Count+1)];
                }
                if (itemAAfegir.PackagingType == Item.Packaging.Kg) 
                { 
                    double qtyAAfegir = 1.0 + r.NextDouble() * (5.0 - 1.0);
                    this.shoppingList.Add(itemAAfegir, qtyAAfegir);
                }
                else 
                {
                    this.ShoppingList.Add(itemAAfegir, r.Next(1, 6));
                }
            }
        }

        /// <summary>
        /// Genera la qüantitat de pts que s'obté a partir del que t'has gastat a la compra
        /// </summary>
        /// <param name="totalInvoiced">total € de la compra</param>
        /// <returns>punts obtinguts</returns>
        public int RawPointsObtainedAtCheckout(double totalInvoiced)
        {
            return Convert.ToInt32(Math.Truncate(totalInvoiced * 0.01));
        }

        /// <summary>
        /// Processa la qüantaitat final d'items comprats i retorna el preu
        /// </summary>
        /// <param name="cart">carrito a processar</param>
        /// <returns>preu final de la compra</returns>
        public static double ProcessItems(ShoppingCart cart)
        {
            double totalPrice = 0;
            foreach (KeyValuePair<Item, double> entrada in cart.ShoppingList)
            { 
                Item item = entrada.Key;
                double qty = entrada.Value;
                if (qty > item.Stock) { qty = item.Stock; }
                Item.UpdateStock(item, -qty);
                totalPrice += qty * item.Price;
            }
            return totalPrice;
        }

        public override string ToString()
        {
            string marcaOferta;
            StringBuilder sb = new StringBuilder("*********");
            sb.AppendLine($"INFO CARRITO DE LA COMPRA CLIENT->{this.Customer.FullName}");
            foreach (KeyValuePair<Item, double> entrada in this.ShoppingList)
            {
                if (entrada.Key.OnSale) { marcaOferta = "(*)"; }
                else { marcaOferta = ""; }
                sb.AppendLine($"{entrada.Key.Description}    - CAT-->{entrada.Key.GetCategory}      - QTY-->{entrada.Value}    - UNIT PRICE-->{entrada.Key.Price}  €{marcaOferta}");
            }
            sb.AppendLine("*****FI CARRITO COMPRA*****");
            return sb.ToString();
        }
    }
}