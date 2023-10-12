using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refrigerator
{
    public enum ProductType
    {
        Food,
        Drink
    }
    public enum Kashrut
    {
        Meat,
        Dairy,
        Parve
    }
    public class Product
    {
        private static int _idCounter = 0;
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int ShelfID { get; set; }
        public ProductType Type { get; private set; }
        public Kashrut KashrutType { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public double SizeInScm { get; private set; }

        public Product(string protucrName, ProductType type, Kashrut kashrutType, DateTime expiryDate, double sizeInScm=5)
        {
            ProductId = ++_idCounter;
            ProductName = protucrName;
            ShelfID = 0;//When you put the product in the refrigerator, the shelf ID will be updated
            Type = type;
            KashrutType = kashrutType;
            ExpiryDate = expiryDate;
            SizeInScm = sizeInScm;
        }
        public override string ToString()
        {
            return @$"Product Details-
ID: {ProductId}
Product Name: {ProductName}
Shelf: {ShelfID}
Type: {Type}
Kashrut: {KashrutType}
Expiry Date: {ExpiryDate.ToShortDateString()}
Size: {SizeInScm}Scm";
        }
    }   
}

