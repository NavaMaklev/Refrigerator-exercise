/*Handled by code*/
#pragma warning disable CS8600,CS8602 ,CS8603,CS8604
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
            { 1, PrintRefrigeratorItemsAndContents },
            { 2, PrinFreeSpaceInTheRefrigerator },
            { 3, InsertAnItem },
            { 4, RemoveAnItem },
            { 5, CleaningTheRefrigeratorAndPrintingCheckedItems },
            { 6, WhatWouldYouLikeToEat },
            { 7, SortByExpirationDate },
            { 8, SortingShelvesAccordingAvailableSpace },
            { 9, SortRefrigeratorAccordingAvailableSpace },
            { 10, PreparationForShopping },
        };
        }
        public int? GetKeyByActionName(string actionName)
        {
            foreach (var pair in actions)
            {
                if (pair.Value.Method.Name == actionName)
                {
                    return pair.Key;
                }
            }
            return null;  
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
        public void PrintRefrigeratorItemsAndContents()
        {
            var refrigerator = User.ChooseRefrigerator(this);
            if (refrigerator != null)
            {
                Console.WriteLine(refrigerator.ToString());
            }
        }

        public void PrinFreeSpaceInTheRefrigerator()
        {
            var refrigerator = User.ChooseRefrigerator(this);
            if (refrigerator != null)
            {
                Console.WriteLine(refrigerator.CalculationOfFreeSpaceInTheRefrigerator());
            }
        }
        public void InsertAnItem()
        {
            var refrigerator = User.ChooseRefrigerator(this);
            if (refrigerator != null)
            {
                Console.WriteLine("What would you like to put in the fridge?");
                var product=User.TapProductDetails();
                refrigerator.PuttingProductInRefrigerator(product);
            }
        }
        public void RemoveAnItem()
        {
            var refrigerator = User.ChooseRefrigerator(this);
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
        public void CleaningTheRefrigeratorAndPrintingCheckedItems()
        {
            var refrigerator = User.ChooseRefrigerator(this);
            if (refrigerator != null)
            {
                refrigerator.CleaningRefrigerator();
            }
        }
        public void WhatWouldYouLikeToEat()
        {
            var listIWant = new List<Product>();
            var refrigerator = User.ChooseRefrigerator(this);
            if (refrigerator != null)
            {
                Console.WriteLine("What do you want to eat?");
                ProductType type=User.TapType();
                Kashrut kashrutType=User.TapKashrut();
                listIWant= refrigerator.WhatDoIWantToEat(kashrutType.ToString(),type.ToString());
                User.PrintProductIMayWant(listIWant);
            }
        }
        public void SortByExpirationDate()
        {
            var refrigerator = User.ChooseRefrigerator(this);
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
        public void SortingShelvesAccordingAvailableSpace()
        {
            var refrigerator = User.ChooseRefrigerator(this);
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
        public void SortRefrigeratorAccordingAvailableSpace()
        {
            if (MyRefrigList.Count > 1)
            {
                Console.WriteLine("All the refrigerators are arranged according to the free space left in them:");
                var fridgs = MyRefrigList[0].SortRefrigeratorsByFreeSpace(MyRefrigList);
                foreach (var frig in fridgs)
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
                        SortRefrigeratorAccordingAvailableSpace();
                        break;
                    case "no":
                        break;
                    default:
                        Console.WriteLine("Sorry your answer is invalid");
                        break;
                }
            }
        }
        public void PreparationForShopping()
        {
            var refrigerator = User.ChooseRefrigerator(this);
            if (refrigerator != null)
            { 
                refrigerator.GettingReadytoShoping();
            }
        }
        public void AddRefrigerator()
        {
            Refrigerator newRef = User.TapRefDetails();
            MyRefrigList.Add(newRef);
        }
    }
}
