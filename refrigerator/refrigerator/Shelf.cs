/*Handled by code*/
#pragma warning disable CS8600,CS8602 ,CS8603,CS8604
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace refrigerator
{
    public class Shelf
    {
        private static int _idCounter = 0;
        public int ShelfId { get; private set; }
        public int FloorNumber { get; private set; }
        public double SpaceInScm { get; private set; }
        public List<Product> Products { get; set; }

        public Shelf(int floorNumber, double spaceInScm=20)
        {
            ShelfId = ++_idCounter;
            FloorNumber = floorNumber;
            SpaceInScm = spaceInScm;
            Products = new List<Product>();
        }
        public override string ToString()
        {
            var productsNames = string.Join(',', Products.Select(product => product.ProductName));
            return @$"Shelf details
ID: {ShelfId}
Floor Number: {FloorNumber}
Space: {SpaceInScm}scm
Space available: {CalculationOfFreeSpaceOnTheShelf()}scm
Products: {ProductsInShelfToString()}";
        }
        //A method that returns the products on the specific shelf
        private string ProductsInShelfToString()
        {
            string productsDetails = "";
            foreach (var product in Products)
            {
                productsDetails += product.ToString()+"\n";
            }
            return productsDetails;
        }
        //A method for calculating free space on the shelf
        public double CalculationOfFreeSpaceOnTheShelf()
        {
            double total = 0;
            foreach (var product in Products)
            {
                total += product.SizeInScm;
            }
            return SpaceInScm - total;
        }
        /*A method of putting a product on the shelf.
         * The method will return true if successful otherwise false*/
        public bool PuttingProductInShelf(Product toAdd)
        {
            try
            {
                Products.Add(toAdd);
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("error in PuttingProductInShelf()");
                return false;
            }
           
        }
        //A method for removing a product from a shelf
        public void TakingProductOutOfShelf(int productID)
        {
            try
            {
                this.Products.Remove(Products.Find(produdt => produdt.ProductId == productID));
                Console.WriteLine("Product removed from shelf {0} ",this.ShelfId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
            
        }
    }
}
