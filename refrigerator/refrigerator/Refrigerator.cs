/*Handled by code*/
#pragma warning disable CS8600,CS8602 ,CS8603,CS8604 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refrigerator
{
    public class Refrigerator
    {
        private static int _idCounter = 0;
        public int RefrigeratorId { get; private set; }
        public string Model { get;private set; }
        public string Color { get; private set; }
        public int NumberOfShelves { get; set; }
        public List<Shelf> Shelves { get; set; }

        public Refrigerator(string model, string color)
        {
            RefrigeratorId = ++_idCounter;
            Model = model;
            Color = color;
            NumberOfShelves ++;
            Shelves = new List<Shelf>
            {
                new Shelf(NumberOfShelves)
            };
        }
        public override string ToString()
        {
            return @$"Refrigerator details-
ID: {RefrigeratorId}
Model: {Model}
Color: {Color}
Number of Shelves: {NumberOfShelves}
Shelves: {ShelvesInRefrigeratorToString()}";
        }
        //A method that returns a list describing all the shelves of the refrigerator
        private string ShelvesInRefrigeratorToString()
        {
            string shelvesDetails = "";
            foreach (var shelve in Shelves)
            {
                shelvesDetails += shelve.ToString() + "\n";
            }
            return shelvesDetails;
        }
        //A method for calculating the available space in the refrigerator
        public double CalculationOfFreeSpaceInTheRefrigerator()
        {
            double result = 0;
            foreach (var shelve in Shelves)
            {
                result += shelve.CalculationOfFreeSpaceOnTheShelf();
            }
            return result;
        }
        //A method of putting a product in the refrigerator
        public string PuttingProductInRefrigerator(Product toAdd)
        {
            Shelf? newShelf, freeShelf = null;
            bool isAdded = false; ;
            freeShelf = Shelves.FirstOrDefault(shelf => shelf.CalculationOfFreeSpaceOnTheShelf() >= toAdd.SizeInScm);
            if (freeShelf != null)
            {
                //Found a shelf with free space for the product
                toAdd.ShelfID = freeShelf.ShelfId;
                isAdded = freeShelf.PuttingProductInShelf(toAdd);
                if (isAdded)
                    return @$"{toAdd.ProductName} added to the refrigerator";
                return "Error - the product does not go into the refrigerator";
            }
            //All the shelves are full - we will create a new shelf
            newShelf = new Shelf(++NumberOfShelves, Math.Max(40, toAdd.SizeInScm + 20));
            toAdd.ShelfID = newShelf.ShelfId;
            isAdded = newShelf.PuttingProductInShelf(toAdd);
            Shelves.Add(newShelf);
            if (isAdded)
                return @$"{toAdd.ProductName} added to the refrigerator";
            return "Error - the product does not go into the refrigerator";
        }
        /*A method of removing a product from the refrigerator. 
         * The method may return empty if the product was not in the refrigerator 
         * or if an error occurred*/
        public Product? TakingProductOutOfRefrigerator(int productID)
        {
            Product? takeProduct = null;
            Shelf? shelfTakeProduct = null;
            foreach (var shelf in Shelves)
            {
                takeProduct = shelf.Products.FirstOrDefault(product => product.ProductId == productID);
                if (takeProduct != null)
                {
                    shelfTakeProduct = shelf;
                    break;
                }
            }
            if (takeProduct == null)
            {
                Console.WriteLine("There is no product with an ID {0} in the refrigerator", productID);
                return takeProduct;
            }
            shelfTakeProduct?.TakingProductOutOfShelf(productID);
            return takeProduct;
        }
        //A method for cleaning the refrigerator
        public void CleaningRefrigerator()
        {
            DateTime currentDate = DateTime.Now;
            foreach (var shelf in Shelves)
            {
                var expiredProducts = shelf.Products.Where(product => product.ExpiryDate < currentDate).ToList();
                foreach (var product in expiredProducts)
                {
                    Console.WriteLine($"Product: {product.ProductName}, expiration date: {product.ExpiryDate:dd/MM/yyyy} thrown in the trash");
                }
                shelf.Products.RemoveAll(product => product.ExpiryDate < currentDate);
            }
        }
        /*A method that accepts filter parameters and returns products that match the parameters*/
        public List<Product> WhatDoIWantToEat(string kashrut, string foodType)
        {
            var listIWant = new List<Product>();
            CleaningRefrigerator();
            foreach (var shelf in Shelves)
            {
                listIWant.AddRange(shelf.Products.Where(product => product.KashrutType.ToString() == kashrut && product.Type.ToString() == foodType));
            }
            return listIWant;
        }
        /*sorties
        *The function will sort and return all the products according to their expiration date (in ascending order)*/
        public List<Product> SortProductsByExpirationDate()
        {
            return this.Shelves.SelectMany(shelf => shelf.Products)
                          .OrderBy(product => product.ExpiryDate)
                          .ToList();
        }
        /*The function will sort and return all the shelves according to the free
        space left on them (from the largest to the smallest)*/
        public List<Shelf> SortShelvesByFreeSpace()
        {
            return Shelves.OrderByDescending(shelf => shelf.CalculationOfFreeSpaceOnTheShelf())
                          .ToList();
        }
        /*The function will sort and return all the refrigerators according to the free space 
         left in them (from the largest to the smallest)*/
        public List<Refrigerator> SortRefrigeratorsByFreeSpace(List<Refrigerator> refs)
        {
            return refs.OrderByDescending(refrigerator =>
                refrigerator.Shelves.Sum(shelf => shelf.CalculationOfFreeSpaceOnTheShelf()))
                .ToList();
        }
        //A method that prepares the refrigerator for shoping
        public void GettingReadytoShoping()
        {
            List<Product> toDelete = new List<Product>();
            Shelf? shelff;
            DateTime someDaysFromToday;
            double sum = CalculationOfFreeSpaceInTheRefrigerator();
            CleaningRefrigerator();
            if (sum < 20)//There is not enough space in the fridge
            {
                someDaysFromToday = DateTime.Today.AddDays(3);
                foreach (var shelf in Shelves)
                {
                    //The dairy products expire within 3 days
                    toDelete.AddRange(shelf.Products.Where(product => product.KashrutType.ToString() == "Dairy" && product.ExpiryDate < someDaysFromToday));
                }
                if (sum - toDelete.Sum(product => product.SizeInScm) < 20)
                {//After the dairy products have been cleared and there is still not enough space
                    someDaysFromToday = DateTime.Today.AddDays(7);
                    foreach (var shelf in Shelves)
                    {
                        //Meat products expire within a week
                        toDelete.AddRange(shelf.Products.Where(product => product.KashrutType.ToString() == "Meat" && product.ExpiryDate < someDaysFromToday));
                    }
                    if (sum - toDelete.Sum(product => product.SizeInScm) < 20)
                    {//still not enough space
                        someDaysFromToday = DateTime.Today.AddDays(1);
                        foreach (var shelf in Shelves)
                        {
                            toDelete.AddRange(shelf.Products.Where(product => product.KashrutType.ToString() == "Parve" && product.ExpiryDate < someDaysFromToday));
                        }
                        if (sum - toDelete.Sum(product => product.SizeInScm) < 20)
                        {
                            Console.WriteLine("This is not the time to shoping.");
                            return;
                        }
                    }
                }
                Console.WriteLine("Unexpired products are thrown away");
                foreach (var product in toDelete)
                {
                    shelff = Shelves.Find(shelf => shelf.ShelfId == product.ShelfID);
                    shelff.TakingProductOutOfShelf(product.ProductId);
                    Console.WriteLine(@$"{product.ProductName} from shelf {shelff.ShelfId} thrown away");
                }
            }
            Console.WriteLine("This is the time to shoping.");
        }
    }
}
