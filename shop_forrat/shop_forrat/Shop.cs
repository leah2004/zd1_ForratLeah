using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop_forrat
{
    internal class Shop
    {
        private Dictionary<Product, int> products;
        private decimal revenue; // Поле для хранения выручки

        public Shop()
        {
            products = new Dictionary<Product, int>();
            revenue = 0m; // Инициализация выручки
        }

        public void AddProduct(Product product, int count)
        {
            products.Add(product, count);
        }

        //Выводит список всех продуктов
        public Dictionary<Product, int> ProductsDictionary()
        {
            return products;
        }

        public void CreateProduct(string name, decimal price, int count)
        {
            products.Add(new Product(name, price), count);
        }

        public void WriteAllProducts()
        {
            Console.WriteLine("Список продуктов: ");
            foreach (var product in products)
            {
                Console.WriteLine(product.Key.GetInfo() + "; Количество: " + product.Value);
            }
        }

        public void Sell(Product product, int count)
        {
            if (products.ContainsKey(product))
            {
                if (products[product] < count)
                {
                    Console.WriteLine("Недостаточно товара в наличии!");
                }
                else
                {
                    products[product] -= count;
                    revenue += product.Price * count; // Увеличиваем выручку
                }
            }
            else
            {
                Console.WriteLine("Товар не найден!");
            }
        }

        public void Sell(string ProductName, int count)
        {
            Product ToSell = FindByName(ProductName);
            if (ToSell != null)
            {
                this.Sell(ToSell, count);
            }
            else
            {
                Console.WriteLine("Товар не найден!");
            }
        }

        public decimal GetRevenue()
        {
            return revenue;
        }

        public Product FindByName(string name)
        {
            foreach (var product in products.Keys)
            {
                if (product.Name == name)
                {
                    return product;
                }
            }
            return null;
        }
    }
}
