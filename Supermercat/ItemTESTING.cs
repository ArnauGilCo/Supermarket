namespace Supermercat
{
    internal class itemTESTING
    {
        static void Main(string[] args)
        {
            // Crear diversos ítems
            Item item1 = new Item('€', 101, "Llet sencera", true, 1.50, Item.Category.MILK_AND_DERIVATIVES, Item.Packaging.Unit, 20, 5);
            Item item2 = new Item('€', 102, "Tomàquets", false, 2.00, Item.Category.VEGETABLES, Item.Packaging.Kg, 15.5, 3);
            Item item3 = new Item('€', 103, "Galletes", true, 3.25, Item.Category.SWEETS, Item.Packaging.Package, 10, 2);

            // Mostrar informació de cada item
            Console.WriteLine("=== INFORMACIÓ DELS PRODUCTES ===");
            Console.WriteLine(item1);
            Console.WriteLine(item2);
            Console.WriteLine(item3);

            // Comprovar el descompte del 10%
            Console.WriteLine("\n=== COMPROVACIÓ DE DESCOMPTE SI ON SALE ===");
            Console.WriteLine($"Preu original de llet: 1.50€, preu amb descompte: {item1.Price}€");
            Console.WriteLine($"Preu original de tomàquet: 2.00€, preu amb descompte (no aplica): {item2.Price}€");

            // Comprovar l'stock abans/després d'actualitzar
            Console.WriteLine("\n=== ACTUALITZAR STOCK ===");
            Console.WriteLine($"Stock original item2 (Tomàquet): {item2.Stock}");
            Item.UpdateStock(item2, 4.5);
            Console.WriteLine($"Stock després d'afegir 4.5 unitats: {item2.Stock}");

            // Comparació
            Console.WriteLine("\n=== COMPARACIÓ D'ITEMS ===");
            Console.WriteLine($"item1 vs item2: {item1.CompareTo(item2)}"); // -1 si item1.code < item2.code
            Console.WriteLine($"item2 vs item1: {item2.CompareTo(item1)}"); // 1 si item2.code > item1.code
            Console.WriteLine($"item1 vs item1: {item1.CompareTo(item1)}"); // 0

            // Llistar i ordenar per codi
            Console.WriteLine("\n=== LLISTA ORDENADA PER CODI ===");
            List<Item> items = new List<Item> { item3, item1, item2 };
            items.Sort();  // Usa CompareTo
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }
    }
}
