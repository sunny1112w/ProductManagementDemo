using BusinessObjects;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        private static List<Product> listProducts = GetProducts();

        public ProductDAO()
        {
            Product chai = new Product(1, "Chai", 3, 12, 18);
            Product chang = new Product(2, "Chang", 1, 23, 19);
            Product aniseed = new Product(3, "Aniseed Syrup", 2, 23, 10);
            listProducts = new List<Product> { chai, chang, aniseed };
        }

        public List<Product> GetProduct()
        {
            return listProducts;
        }
        public static List<Product> GetProducts()
        {
            var listProducts = new List<Product>();
            try
            {
                using var db = new MyStoreContext();
                listProducts = db.Products.ToList();
            }
            catch (Exception e) { }
            return listProducts;
        }

        public static void SaveProduct(Product p)
        {
            try
            {
                using var db = new MyStoreContext();
                db.Products.Add(p);
                db.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }


        public static void UpdateProduct(Product product)
        {
            try
            {
                using var db = new MyStoreContext();
                var existingProduct = db.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (existingProduct != null)
                {
                    existingProduct.ProductName = product.ProductName;
                    existingProduct.UnitPrice = product.UnitPrice;
                    existingProduct.UnitsInStock = product.UnitsInStock;
                    existingProduct.CategoryId = product.CategoryId;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void DeleteProduct(Product product)
        {
            try
            {
                using var db = new MyStoreContext();
                var productToDelete = db.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (productToDelete != null)
                {
                    db.Products.Remove(productToDelete);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
        }

        public static Product GetProductById(int id)
        {
            try
            {
                using var db = new MyStoreContext();
                return db.Products.FirstOrDefault(p => p.ProductId == id);
            }
            catch (Exception e)
            {

                return null;
            }
        }

    }
}
