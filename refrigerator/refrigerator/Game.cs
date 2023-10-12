/*Handled by code*/
#pragma warning disable CS8600,CS8602 ,CS8603,CS8604
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refrigerator
{
    public class Game
    {
        /// <summary>
        /// Represents the main game logic for managing a collection of refrigerators.
        /// Allows the user to perform various operations on the refrigerators and the products within them.
        /// In case no fridges have been created yet, the functions will offer the user to create a fridge to make the game meaningful
        /// </summary>
        public List<Refrigerator> MyRefrigList { get; set; }
        public Dictionary<int, Action> actions;

        public Game() {
            MyRefrigList = new List<Refrigerator>();
            actions = new Dictionary<int, Action>
        {
            { 1, OneWasPressed },
            { 2, TwoWasPressed },
            { 3, ThreeWasPressed },
            { 4, FourWasPressed },
            { 5, FiveWasPressed },
            { 6, SixWasPressed },
            { 7, SevenWasPressed },
            { 8, EightWasPressed },
            { 9, NineWasPressed },
            { 10, TenWasPressed },
        };
        }
        public void HandleChoice(int choice)
        {
            if (actions.ContainsKey(choice))
            {
                actions[choice].Invoke();
            }
            else if (choice == 100)
            {
                Console.WriteLine("Exiting the system...");
            }
            else
            {
                Console.WriteLine("Invalid choice");
            }
        }
        private Refrigerator GetRefrigeratorById()
        {
            if (MyRefrigList.Count == 0)
            {
                Console.WriteLine("No refrigerators have been created yet. Would you like to create? Press yes or no.");
                string answer = Console.ReadLine();
                switch (answer)
                {
                    case "yes":
                        AddRefrigerator();
                        return MyRefrigList[0];
                    case "no":
                        return null;
                    default:
                        Console.WriteLine("Sorry your answer is invalid");
                        return null;
                }
            }
            int id;
            Console.WriteLine("Enter Refrigerator ID - Number between 1 to {0}", MyRefrigList.Count);
            while(!int.TryParse(Console.ReadLine(),out id))
            {
                Console.WriteLine("Enter valid ID");
            }
            var refrigerator = MyRefrigList.FirstOrDefault(r => r.RefrigeratorId == id);
            if (refrigerator == null)
            {
                Console.WriteLine("No refrigerator was found with the ID you entered");
            }
            return refrigerator;
        }

        public void OneWasPressed()
        {
            var refrigerator = GetRefrigeratorById();
            if (refrigerator != null)
            {
                Console.WriteLine(refrigerator.ToString());
            }
        }

        public void TwoWasPressed()
        {
            var refrigerator = GetRefrigeratorById();
            if (refrigerator != null)
            {
                Console.WriteLine(refrigerator.CalculationOfFreeSpaceInTheRefrigerator());
            }
        }
        public void ThreeWasPressed()
        {
            var refrigerator = GetRefrigeratorById();
            if (refrigerator != null)
            {
                Console.WriteLine("What would you like to put in the fridge?");
                var product=CreateProduct();
                refrigerator.PuttingProductInRefrigerator(product);
            }
        }
        public void FourWasPressed()
        {
            var refrigerator = GetRefrigeratorById();
            Product? product;
            if (refrigerator != null)
            {
                Console.WriteLine(refrigerator.ToString());
                Console.WriteLine("Enter an item code that you would like to take out of the fridge:");
                product=refrigerator.TakingProductOutOfRefrigerator(int.Parse(Console.ReadLine()));
                if(product != null)
                {
                    Console.WriteLine(product.ToString());
                }  
            }
        }
        public void FiveWasPressed()
        {
            var refrigerator = GetRefrigeratorById();
            if (refrigerator != null)
            {
                refrigerator.CleaningRefrigerator();
            }
        }
        public void SixWasPressed()
        {
            var listIWant = new List<Product>();
            var refrigerator = GetRefrigeratorById();
            if (refrigerator != null)
            {
                Console.WriteLine("What do you want to eat?");
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
                listIWant= refrigerator.WhatDoIWantToEat(kashrutType.ToString(),type.ToString());
                if(listIWant.Count > 0)
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
        }
        public void SevenWasPressed()
        {
            var refrigerator = GetRefrigeratorById();
            if (refrigerator != null)
            {
              var products= refrigerator.SortProductsByExpirationDate();
                if (products.Count > 0)
                {
                    Console.WriteLine("List of products by expiration date:");
                    foreach (var product in products)
                    {
                        Console.WriteLine(product.ToString());
                    }
                }
                else
                    Console.WriteLine("This fridge is empty");
            }
        }
        public void EightWasPressed()
        {
            var refrigerator = GetRefrigeratorById();
            if (refrigerator != null)
            {
                Console.WriteLine("All the shelves are arranged according to the free space left on them:");
                var shelves=refrigerator.SortShelvesByFreeSpace();
                foreach (var shelf in shelves)
                { 
                    Console.WriteLine(shelf.ToString()+"\n");
                }
            }
        }
        public void NineWasPressed()
        {
            if (MyRefrigList.Count > 1)
            {
                Console.WriteLine("All the refrigerators are arranged according to the free space left in them:");
                foreach (var frig in MyRefrigList)
                {
                    Console.WriteLine("Refrigerator id: {0} -> free space: {1}", frig.RefrigeratorId , frig.CalculationOfFreeSpaceInTheRefrigerator());
                }
            }
            else
            {
                Console.WriteLine("Refrigerators are sorted if there are at least 2 active refrigerators.\n Would you like to create a refrigerator?\n Press yes \\ no");
                string answer = Console.ReadLine();
                switch (answer)
                {
                    case "yes":
                        AddRefrigerator();
                        NineWasPressed();
                        break;
                    case "no":
                        break;
                    default:
                        Console.WriteLine("Sorry your answer is invalid");
                        break;
                }
            }
        }
        public void TenWasPressed()
        {
            var refrigerator = GetRefrigeratorById();
            if (refrigerator != null)
            { 
                refrigerator.GettingReadytoShoping();
            }
        }
        public void AddRefrigerator()
        {
            Console.WriteLine("Enter refrigerator details:");
            Console.WriteLine("model?");
            string model = Console.ReadLine();
            Console.WriteLine("Color?");
            string color = Console.ReadLine();
            Refrigerator newRef = new Refrigerator(model, color);
            MyRefrigList.Add(newRef);
        }
        private Product CreateProduct()
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
    }
}
