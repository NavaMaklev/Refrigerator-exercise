#pragma warning disable CS8600,CS8602 ,CS8603,CS8604

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refrigerator
{
    public static class User
    {
      public  static int SelectAnAction(Game game)
        {
            string input;
            int choice = 0;
            Console.WriteLine("Choose an option:");
            Console.WriteLine($@"{game.GetKeyByActionName("PrintRefrigeratorItemsAndContents")}: Display all items in the refrigerator");
            Console.WriteLine($@"{game.GetKeyByActionName("PrinFreeSpaceInTheRefrigerator")}: Display available space in the refrigerator");
            Console.WriteLine($@"{game.GetKeyByActionName("InsertAnItem")}: Add an item to the refrigerator");
            Console.WriteLine($@"{game.GetKeyByActionName("RemoveAnItem")}: Remove an item from the refrigerator");
            Console.WriteLine($@"{game.GetKeyByActionName("CleaningTheRefrigeratorAndPrintingCheckedItems")}: Clean the refrigerator");
            Console.WriteLine($@"{game.GetKeyByActionName("WhatWouldYouLikeToEat")}: What do you want to eat?");
            Console.WriteLine($@"{game.GetKeyByActionName("SortByExpirationDate")}: Display items by expiration date");
            Console.WriteLine($@"{game.GetKeyByActionName("SortingShelvesAccordingAvailableSpace")}: Display shelves by available space");
            Console.WriteLine($@"{game.GetKeyByActionName("SortRefrigeratorAccordingAvailableSpace")}: Display refrigerators by available space");
            Console.WriteLine($@"{game.GetKeyByActionName("PreparationForShopping")}: Prepare the refrigerator for shopping");
            Console.WriteLine("100: Exit the system");
            input = Console.ReadLine();
            if (!int.TryParse(input, out choice))
            {
                Console.WriteLine("Please enter a valid number.");
                return -1;
            }
            return choice;
        }
        public static Refrigerator ChooseRefrigerator(Game game)
        {
            if (game.MyRefrigList.Count == 0)
            {
                Console.WriteLine("No refrigerators have been created yet. Would you like to create? Press yes or no.");
                string answer = Console.ReadLine();
                switch (answer)
                {
                    case "yes":
                        game.AddRefrigerator();
                        return game.MyRefrigList[0];
                    case "no":
                        return null;
                    default:
                        Console.WriteLine("Sorry your answer is invalid");
                        return null;
                }
            }
            int id;
            Console.WriteLine("Enter Refrigerator ID - Number between 1 to {0}", game.MyRefrigList.Count);
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Enter valid ID");
            }
            var refrigerator = game.MyRefrigList.FirstOrDefault(r => r.RefrigeratorId == id);
            if (refrigerator == null)
            {
                Console.WriteLine("No refrigerator was found with the ID you entered");
            }
            return refrigerator;
        } 
       public static Product TapProductDetails()
        {
            Console.WriteLine("Enter product details:");
            Console.Write("Product Name: ");
            string productName = Console.ReadLine();
            ProductType type;
            do
            {
                Console.Write("Enter product type (");
                foreach (var item in Enum.GetNames(typeof(ProductType)))
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine("):");
            } while (!Enum.TryParse<ProductType>(Console.ReadLine(), out type));
            Kashrut kashrutType;
            do
            {
                Console.Write("Enter Kashrut (");
                foreach (var item in Enum.GetNames(typeof(Kashrut)))
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine("):");
            } while (!Enum.TryParse<Kashrut>(Console.ReadLine(), out kashrutType));
            DateTime expiryDate;
            do
            {
                Console.Write("Enter Expiry Date (format: YYYY-MM-DD): ");
            } while (!DateTime.TryParse(Console.ReadLine(), out expiryDate));
            Console.Write("Enter Size in Scm (default is 5): ");
            string sizeInput = Console.ReadLine();
            double sizeInScm = string.IsNullOrEmpty(sizeInput) ? 5 : double.Parse(sizeInput);
            return new Product(productName, type, kashrutType, expiryDate, sizeInScm);
        }
        public static ProductType TapType()
        {
            ProductType type;
            do
            {
                Console.Write("Enter product type (");
                foreach (var item in Enum.GetNames(typeof(ProductType)))
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine("):");
            } while (!Enum.TryParse<ProductType>(Console.ReadLine(), out type));
            return type;
        }
        public static Kashrut TapKashrut()
        {
            Kashrut kashrutType;
            do
            {
                Console.Write("Enter Kashrut (");
                foreach (var item in Enum.GetNames(typeof(Kashrut)))
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine("):");
            } while (!Enum.TryParse<Kashrut>(Console.ReadLine(), out kashrutType));
            return kashrutType;
        }
        public static void PrintProductIMayWant(List <Product> listIWant)
        {
            if (listIWant.Count > 0)
            {
                Console.WriteLine("The products according to the filter you applied:");
                foreach (var product in listIWant)
                {
                    Console.WriteLine(product.ToString());
                }
            }
            else
            {
                Console.WriteLine("Sorry, but no products were found according to the filter you selected");
            }
        }
        public static Refrigerator TapRefDetails()
        {
            Console.WriteLine("Enter refrigerator details:");
            Console.WriteLine("model?");
            string model = Console.ReadLine();
            Console.WriteLine("Color?");
            string color = Console.ReadLine();
            return new Refrigerator(model, color);
        }
    }
    
}
