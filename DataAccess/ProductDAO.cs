using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        private FstoreContext dbContext;

        public ProductDAO(FstoreContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ProductDAO()
        : this(new FstoreContext())
        {
        }

        public IEnumerable<Product> GetProductList()
        {
            return dbContext.Products.ToList();
        }

        public Product GetProductByID(int productId)
        {
            return dbContext.Products.SingleOrDefault(p => p.ProductId == productId);
        }

        public void AddNew(Product product)
        {
            if (GetProductByID(product.ProductId) == null)
            {
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("The product is already exist.");
            }
        }

        public void Update(Product product)
        {
            var existingProduct = GetProductByID(product.ProductId);
            if (existingProduct != null)
            {
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.ProductName = product.ProductName;
                existingProduct.Weight = product.Weight;
                existingProduct.UnitPrice = product.UnitPrice;
                existingProduct.UnitsInStock = product.UnitsInStock;

                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("The product does not already exist.");
            }
        }

        public void Remove(int productId)
        {
            var product = GetProductByID(productId);
            if (product != null)
            {
                dbContext.Products.Remove(product);
                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("The product does not already exist.");
            }
        }
    }
}
