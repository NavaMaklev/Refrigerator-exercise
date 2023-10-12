#pragma warning disable CS8604,CS8600

namespace refrigerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            int choice=0;
            string input;
            do
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1: Display all items in the refrigerator");
                Console.WriteLine("2: Display available space in the refrigerator");
                Console.WriteLine("3: Add an item to the refrigerator");
                Console.WriteLine("4: Remove an item from the refrigerator");
                Console.WriteLine("5: Clean the refrigerator");
                Console.WriteLine("6: What do you want to eat?");
                Console.WriteLine("7: Display items by expiration date");
                Console.WriteLine("8: Display shelves by available space");
                Console.WriteLine("9: Display refrigerators by available space");
                Console.WriteLine("10: Prepare the refrigerator for shopping");
                Console.WriteLine("100: Exit the system");
                input = Console.ReadLine();
                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }
                game.HandleChoice(choice);
            } while (choice != 100);
        }
    }  
}