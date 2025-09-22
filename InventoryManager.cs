using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP422_Mozgunova
{
    public class InventoryManager
    {
        public List<Product> Products { get; set; }
        private int currentId;

        public InventoryManager()
        {
            Products = new List<Product>();
            currentId = 1000;
            AddTestData();
        }

        private void AddTestData()
        {
            AddProduct("Мышь беспроводная", 2500.00m, 35, "Электроника");
            AddProduct("Телефон", 50000.00m, 30, "Электроника");
            AddProduct("Телевизор", 95000.00m, 50, "Электроника");
            AddProduct("Бритва", 1200.00m, 100, "Электроника");
            AddProduct("Наушники", 4000.00m, 70, "Электроника");
        }

        public void AddProduct(string name, decimal price, int quantity, string category)
        {
            currentId++;
            Products.Add(new Product
            {
                Code = "1" + currentId.ToString("D5"),
                Name = name,
                Price = price,
                Quantity = quantity,
                IsAvailable = quantity > 0,
                Category = category
            });
        }

        public void DeleteProduct(string code)
        {
            var product = Products.FirstOrDefault(p => p.Code == code);
            if (product != null)
            {
                Products.Remove(product);
            }
        }

        public void OrderSupply(string code, int quantity)
        {
            var product = Products.FirstOrDefault(p => p.Code == code);
            if (product != null)
            {
                product.Quantity += quantity;
                product.IsAvailable = product.Quantity > 0;
            }
        }

        public bool SellProduct(string code, int quantity)
        {
            var product = Products.FirstOrDefault(p => p.Code == code);
            if (product != null && product.Quantity >= quantity)
            {
                product.Quantity -= quantity;
                product.IsAvailable = product.Quantity > 0;
                return true;
            }
            return false;
        }

        public List<Product> SearchProducts(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return Products;

            return Products.Where(p =>
                p.Code.Contains(searchText) ||
                p.Name.ToLower().Contains(searchText.ToLower()) ||
                p.Category.ToLower().Contains(searchText.ToLower())
            ).ToList();
        }
    }
}
