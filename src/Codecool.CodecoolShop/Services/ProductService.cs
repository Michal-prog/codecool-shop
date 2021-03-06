using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;

using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class ProductService
    {
        private readonly IProductDao productDao;
        private readonly IProductCategoryDao productCategoryDao;

        public ProductService(IProductDao productDao, IProductCategoryDao productCategoryDao)
        {
            this.productDao = productDao;
            this.productCategoryDao = productCategoryDao;
        }

        public ProductCategory GetProductCategory(int categoryId)
        {
            return this.productCategoryDao.Get(categoryId);
        }

        public IEnumerable<Product> GetProductsForCategory(int categoryId)
        {
            ProductCategory category = this.productCategoryDao.Get(categoryId);
            return this.productDao.GetBy(category);
        }
        public IEnumerable<ProductCategory> GetAllProductCategory()
        {
            return this.productCategoryDao.GetAll();
        }
        
        public List<string> GetListOfSuplliers()
        {
            List<Supplier> listOfSuppliers = new List<Supplier>();
            var listOfProduct = Daos.Implementations.ProductDaoMemory.GetInstance().GetAll();
            foreach (var product in listOfProduct)
            { 
                listOfSuppliers.Add(product.Supplier);
            }
            List<string> listOfSuppliersString = new List<string>();
            foreach (var supplier in listOfSuppliers)
            {
                if (!listOfSuppliersString.Contains(supplier.Name))
                {
                    listOfSuppliersString.Add(supplier.Name);
                }
            }
            
            return listOfSuppliersString;
        }
    }
}
