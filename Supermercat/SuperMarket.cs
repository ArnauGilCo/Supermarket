using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace Supermercat
{
    public class SuperMarket
    {
        private string name;
        private string address;
        private int activeLines;
        public const int MAXLINES = 5;
        private CheckOutLine[] lines = new CheckOutLine[MAXLINES];
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
            this.activeLines = activeLines;
            for (int i = 0; i < activeLines; i++)
            {
                lines[i] = new CheckOutLine(GetAvailableCashier(), i+1);
            }
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
                if (entry.Value.Active == false)
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
                if (entry.Value.Active == false)
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

        public CheckOutLine[] Lines { get { return this.lines; } }

        public int ActiveLines 
        { get { return this.activeLines; }                         
          set { this.activeLines = value; }
        }

        /// <summary>
        /// Retorna la CheckOutLine en la posicio passada com a paràmetre. Si el número donat no és compatible  retorna null.
        /// </summary>
        /// <param name="lineNumber">posició de la línia a bsucar</param>
        /// <returns>Linia en la posicio del pàrametre o bé null si el paràmetre no és correcte</returns>
        public CheckOutLine GetCheckOutLine(int lineNumber)
        { 
            CheckOutLine checkOutLine = null;
            if (lineNumber >= 1 && lineNumber <= lines.Length) { checkOutLine = lines[lineNumber - 1]; }
            return checkOutLine;
        }

        /// <summary>
        /// Obre la línia del número passat per paràmetre en el cas que no estigui oberta.
        /// </summary>
        /// <param name="line2Open">Línia a obrir</param>
        public bool OpenCheckOutLine(int line2Open)
        {
            bool fet = false;
            if (line2Open > activeLines && line2Open <= MAXLINES) 
            {
                lines[line2Open-1] = new CheckOutLine(GetAvailableCashier(), line2Open);
                activeLines++;
                fet = true;
            }
            return fet;
        }

        /// <summary>
        /// Afegeix un carro a una línia tots dos especificats als pàrametres
        /// </summary>
        /// <param name="theCart">carro a afegir a línia</param>
        /// <param name="line">linia a la qual afegir</param>
        /// <returns>true si s'ha completat, false si no s'ha completat</returns>
        public bool JoinTheQueque(ShoppingCart theCart, int line)
        {
            bool completat = false;
            if (line >= 1 && line <= activeLines)
            {
                lines[line-1].CheckIn(theCart);
                completat = true;
            }
            return completat;
        }

        /// <summary>
        /// Fa el checkout del carro que li toqui de la línia passada com a pàrametre
        /// </summary>
        /// <param name="line">linia on fer el dequeque</param>
        /// <returns>true si s'ha fet, false si no s'ha fet</returns>
        public bool Checkout(int line)
        {
            bool completat = false;
            if (line >= 1 && line <= activeLines)
            {
                completat = lines[line - 1].CheckOut();
            }
            return completat;
        }

        public void RemoveQueque(SuperMarket super, int lineToRemove)
        {
            Person caixer = super.lines[lineToRemove].Cashier;
            super.lines[lineToRemove].Active = false;
            caixer.Active = false;
            super.ActiveLines--;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(this.name);
            sb.AppendLine(this.address);
            foreach (CheckOutLine line in this.lines)
            {
                sb.Append(line.ToString());
            }
            return sb.ToString();
        }
    }
}