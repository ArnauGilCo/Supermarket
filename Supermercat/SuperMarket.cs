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
        Dictionary<Customer, ShoppingCart> carrosPassejant;

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
            activeLines = r.Next(1,MAXLINES+1);
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
                int stock = r.Next(50, 551);
                bool onSale = r.Next(1, 101) <= 15;
                Item item = new Item('€', Item.nextCode, items[0], onSale, Convert.ToDouble(items[3]), (Item.Category)Convert.ToInt32(items[1]), Item.charToPackage(Convert.ToChar(items[2])), stock, r.Next(stock - 35,stock - 2));
                warehouse.Add(Item.nextCode, item);
                Item.nextCode += 1;
                linia = sr.ReadLine();
            }
            sr.Close();
            return warehouse;
        }

        /// <summary>
        /// Retorna el warehouse de forma ordenada seguint el criteri d'ordenació d'items.
        /// </summary>
        /// <returns>Warehouse ordenat</returns>
        public SortedSet<Item> GetItemsByStock()
        { 
            SortedSet<Item> warehouse = new SortedSet<Item>();
            foreach (Item item in this.warehouse.Values)
            {
                warehouse.Add(item);
            }
            return warehouse;
        }

        /// <summary>
        /// Et dona un caixer disponible de tot l'staff i el deixa Active
        /// </summary>
        /// <returns>Qüalsevol caixer dels disponibles</returns>
        public Person GetAvailableCashier()
        {
            bool trobat = false;
            Random r = new Random();
            Person availableCashier = null;
            int i = r.Next(0, staff.Count);
            while (!trobat)
            {
                KeyValuePair<string, Person> entry = staff.ElementAt(i);
                if (entry.Value.Active = false)
                {
                    trobat = true;
                    availableCashier = entry.Value;
                    entry.Value.Active = true;  
                }
                else
                {
                    i = r.Next(0, staff.Count);
                }
            }
            return availableCashier;
        }

        /// <summary>
        /// Et dona un customer disponible de tot el llistat i el deixa Active
        /// </summary>
        /// <returns>Qüalsevol customer dels disponibles</returns>
        public Person GetAvailableCustomer()
        {
            bool trobat = false;
            Random r = new Random();
            Person availableCustomer = null;
            int i = r.Next(0, customers.Count);
            while(!trobat)
            {
                KeyValuePair<string, Person> entry = customers.ElementAt(i);
                if (entry.Value.Active = false)
                {
                    trobat = true;
                    availableCustomer = entry.Value;
                    entry.Value.Active = true;
                }
                else
                { 
                    i = r.Next(0, customers.Count);
                }
            }
            return availableCustomer;
        }

        public Dictionary<string, Person> Customers { get { return this.customers; } }

        public Dictionary<string, Person> Staff { get { return this.staff; } }

        public SortedDictionary<int, Item> Warehouse { get { return this.warehouse; } }

        public int ActiveLines { get { return this.ActiveLines; } }
    }
}