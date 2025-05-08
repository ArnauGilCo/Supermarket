namespace Supermercat
{
    internal class SupermarketTESTING
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicialitzant supermercat de prova...\n");

            string pathClients = "CUSTOMERS.TXT";
            string pathCashiers = "CASHIERS.TXT";
            string pathItems = "GROCERIES.TXT";

            SuperMarket super = new SuperMarket("SuperTest", "Carrer Prova, 123", pathCashiers, pathClients, pathItems, 3);

            Console.WriteLine("Supermercat inicialitzat correctament.");
            Console.WriteLine($"Nom: SuperTest");
            Console.WriteLine($"Adreça: Carrer Prova, 123");
        }
    }
}
