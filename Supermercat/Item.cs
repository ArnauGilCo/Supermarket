using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Supermercat
{
    public class Item : IComparable<Item>
    {
        public enum Category
        { BEVERAGE = 1, FRUITS, VEGETABLES, BREAD, MILK_AND_DERIVATIVES, GARDEN, MEAT, SWEETS, SAUCES, FROZEN, CLEANING, FISH, OTHER };

        public enum Packaging 
        { Unit, Kg, Package };

        public static int nextCode = 1;
        
        private char currency = '\u20AC';
        private int code;
        private string description;
        private bool onSale;
        private double price;
        private Category category;
        private Packaging packaging;
        private double stock;
        private int minStock;

        /// <summary>
        /// Construcció d'un item a partir de tots els paràmetres.
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="code"></param>
        /// <param name="description"></param>
        /// <param name="onSale"></param>
        /// <param name="price"></param>
        /// <param name="category"></param>
        /// <param name="packaging"></param>
        /// <param name="stock"></param>
        /// <param name="minStock"></param>
        public Item(char currency, int code, string description, bool onSale, double price, Category category, Packaging packaging, double stock, int minStock)
        {
            this.currency = currency;
            this.code = code;
            this.description = description;
            this.onSale = onSale;
            this.price = price;
            this.category = category;
            this.packaging = packaging;
            this.stock = stock;
            this.minStock = minStock;
        }

        public double Stock { get { return this.stock; } }
        public int MinStock { get { return this.minStock; } }

        public Packaging PackagingType { get { return this.packaging; } }

        public Category GetCategory { get { return this.category; } }

        public string Description { get { return this.description; } }

        public bool OnSale {  get { return this.onSale; } }

        /// <summary>
        /// Retorna el preu tinguent en compte si el producte està o no està de rebaixes.
        /// </summary>
        public double Price 
        { 
            get 
            { 
                double finalPrice;
                if (this.OnSale) { finalPrice = price * 0.9; }
                else { finalPrice = price; }
                return finalPrice;
            } 
        }

        /// <summary>
        /// Afegeix la qüantitat d'estoc passada com a paràmetre.
        /// </summary>
        /// <param name="item">item al que afegir-hi stock</param>
        /// <param name="qty">qty a afegir al item</param>
        public static void UpdateStock(Item item, double qty)
        {
            item.stock += qty;
        }

        /// <summary>
        /// Genera un string amb l'informació necessària del item
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string sale;
            if (this.OnSale) { sale = $"Y({this.Price})"; } else { sale = "N"; }
            return $"CODE->{this.code} DESCRIPTION->{this.description}     CATEGORY->{this.GetCategory}    STOCK->{this.Stock} MIN_STOCK->{this.MinStock}  PRICE->{this.price}{currency} ONSALE->{sale}{currency}";
        }

        /// <summary>
        /// Compara 2 items a partir del seu stock.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Item? other)
        {
            return this.stock.CompareTo(other.stock);
        }

        /// <summary>
        /// Transforma un char a un valor de package.
        /// </summary>
        /// <param name="codiPack">char passat</param>
        /// <returns></returns>
        /// <exception cref="Exception">si no coincideix amb cap de les 3 opcions</exception>
        public static Packaging charToPackage(char codiPack)
        {
            Packaging packaging;
            if (codiPack == 'U') { packaging = Packaging.Unit; }
            else if (codiPack == 'K') { packaging = Packaging.Kg; }
            else if (codiPack == 'P') { packaging = Packaging.Package; }
            else throw new Exception("Format incorrecte");
            return packaging;
        }
    }
}