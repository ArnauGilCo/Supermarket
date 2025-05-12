using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supermercat
{
    public class SuperMarket
    {
        private string name;
        private string address;
        public static int MAXLINES;
        private int activeLines;
        CheckOutLine[] lines = new CheckOutLine[MAXLINES];
        Dictionary<string,Person> staff;
        Dictionary<string,Person> customers;
        SortedDictionary<int,Item> warehouse;

        /// <summary>
        /// Genera el supermercat i afegeix els caixers, clients i productes a les estructures de dades corresponents.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="fileCashiers"></param>
        /// <param name="fileCustomers"></param>
        /// <param name="fileItems"></param>
        /// <param name="activeLines"></param>
        public SuperMarket(string name, string address, string fileCashiers, string fileCustomers, string fileItems, int activeLines)
        { 
            Random r = new Random();
            this.name = name;
            this.address = address;
            this.customers = LoadCustomers(fileCustomers);
            this.staff = LoadCashiers(fileCashiers);
            this.warehouse = LoadWarehouse(fileItems);
            MAXLINES = 5;
            activeLines = r.Next(1,MAXLINES);
        }

        /// <summary>
        /// Retorna un diccionari amb tots els clients del supermercat.
        /// </summary>
        /// <param name="fileName">Path del fitxer d'on treiem els clients</param>
        /// <returns>Diccionari customers</returns>
        private Dictionary<string, Person> LoadCustomers(string fileName)
        {
            Dictionary<string, Person> customers = new Dictionary<string, Person>();
            StreamReader sr = new StreamReader(fileName);
            string linia;
            string[] persona;
            linia = sr.ReadLine();
            while (linia != null)
            {
                persona = linia.Split(';');
                Person customer;
                if (persona[2] != "") { customer = new Customer(persona[0], persona[1], Convert.ToInt32(persona[2])); }
                else { customer = new Customer(persona[0], persona[1], null); }
                customers.Add(persona[0], customer);
                linia = sr.ReadLine();
            }
            sr.Close();
            return customers;
        }

        /// <summary>
        /// Retorna un diccionari amb tots els caixers del supermercat.
        /// </summary>
        /// <param name="fileName">Path del fitxer d'on treiem els caixers</param>
        /// <returns>Diccionari staff</returns>
        private Dictionary<string, Person> LoadCashiers(string fileName)
        {
            Dictionary<string, Person> staff = new Dictionary<string, Person>();
            StreamReader sr = new StreamReader(fileName);
            string linia;
            string[] persona;
            linia = sr.ReadLine();
            while (linia != null)
            {
                persona = linia.Split(';');
                Person cashier = new Cashier(persona[0], persona[1], Convert.ToDateTime(persona[3]));
                staff.Add(persona[0], cashier);
                linia = sr.ReadLine();
            }
            sr.Close();
            return staff;
        }

        /// <summary>
        /// Retorna un diccionari amb tots els items del supermercat.
        /// </summary>
        /// <param name="fileName">Path del fitxer d'items</param>
        /// <returns>SortedDiccionary amb tots els items</returns>
        private SortedDictionary<int, Item> LoadWarehouse(string fileName)
        {
            SortedDictionary<int, Item> warehouse = new SortedDictionary<int, Item>();
            StreamReader sr = new StreamReader(fileName);
            Random r = new Random();
            string linia;
            string[] items;
            linia = sr.ReadLine();
            while (linia != null)
            {
                items = linia.Split(';');
                int stock = r.Next(50, 550);
                bool onSale = r.Next(1, 100) <= 15;
                Item item = new Item('€', Item.nextCode, items[0], onSale, Convert.ToDouble(items[3]), (Item.Category)Convert.ToInt32(items[1]), Item.charToPackage(Convert.ToChar(items[2])), stock, r.Next(stock - 35,stock - 3));
                warehouse.Add(Item.nextCode, item);
                Item.nextCode += 1;
                linia = sr.ReadLine();
            }
            sr.Close();
            return warehouse;
        }

        public SortedSet<Item> GetItemsByStock()
        { 
            SortedSet<Item> warehouse = new SortedSet<Item>();
            //SortedSet<Item> warehouse = new SortedSet<Item>(this.warehouse.Value);
            foreach (Item item in this.warehouse.Values)
            {
                warehouse.Add(item);
            }
            return warehouse;
        }

        public class CheckOutLine
        {
            private int number;
            private Queue<ShoppingCart> queque;
            private Person cashier;
            private bool active;
        }
    }
}