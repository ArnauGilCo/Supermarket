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

        public const int nextCode = 1;
        
        private char currency = '\u20AC';
        private int code;
        private string description;
        private bool onSale;
        private double price;
        private Category category;
        private Packaging packaging;
        private double stock;
        private int minStock;

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

        public static void UpdateStock(Item item, double qty)
        {
            item.stock += qty;
        }

        public override string ToString()
        {
            string sale;
            if (this.OnSale) { sale = $"Y({this.Price})"; } else { sale = "N"; }
            return $"CODE->{this.code} DESCRIPTION->{this.description}     CATEGORY->{this.GetCategory}    STOCK->{this.Stock} MIN_STOCK->{this.MinStock}  PRICE->{this.price}{currency} ONSALE->{sale}{currency}";
        }

        public int CompareTo(Item? other)
        {
            return this.code.CompareTo(other.code);
        }
    }
}