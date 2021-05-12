using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;

using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class ProductService
    {
        public readonly IProductDao productDao;
        public readonly IProductCategoryDao productCategoryDao;
        public readonly ISupplierDao supplierDao;


        public ProductService(IProductDao productDao, IProductCategoryDao productCategoryDao, ISupplierDao supplierDao)
        {
            this.productDao = productDao;
            this.productCategoryDao = productCategoryDao;
            this.supplierDao = supplierDao;
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
        public IEnumerable<Product> GetProductsForSupplier(int supplierId)
        {
            Supplier supplier = this.supplierDao.Get(supplierId);
            return this.productDao.GetBy(supplier);
        }

        public Product GetProduct(int id)
        {
           return this.productDao.Get(id);
        }


        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return this.supplierDao.GetAll();
        }
        /*      public List<Supplier> GetListOfSuplliers()
              {
                  List<Supplier> listOfSuppliers = new List<Supplier>();
                  var listOfProduct = Daos.Implementations.ProductDaoMemory.GetInstance().GetAll();
                  foreach (var product in listOfProduct)
                  { 
                      listOfSuppliers.Add(product.Supplier);
                  }
                  //List<string> listOfSuppliersString = new List<string>();
                  //foreach (var supplier in listOfSuppliers)
                  //{
                  //    if (!listOfSuppliersString.Contains(supplier.Name))
                  //    {
                  //        listOfSuppliersString.Add(supplier.Name);
                  //    }
                  //}
                  return listOfSuppliers;
              }*/

        public void AddProduct()
        {
        

            Supplier amazon = new Supplier { Name = "Amazon", Description = "Digital content and services" };
            supplierDao.Add(amazon);
            Supplier lenovo = new Supplier { Name = "Lenovo", Description = "Computers" };
            supplierDao.Add(lenovo);
            ProductCategory tablet = new ProductCategory { Name = "Tablet", Department = "Hardware", Description = "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display." };
            productCategoryDao.Add(tablet);
            ProductCategory phone = new ProductCategory { Name = "Tablet", Department = "phone", Description = "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display." };
            productCategoryDao.Add(phone);
            productDao.Add(new Product { Name = "Amazon Fire", DefaultPrice = 49.9m, Currency = "USD", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = tablet, Supplier = amazon });
            productDao.Add(new Product { Name = "Lenovo IdeaPad Miix 700", DefaultPrice = 479.0m, Currency = "USD", Description = "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.", ProductCategory = tablet, Supplier = lenovo });
            productDao.Add(new Product { Name = "Amazon Fire HD 8", DefaultPrice = 89.0m, Currency = "USD", Description = "Amazon's latest Fire HD 8 tablet is a great value for media consumption.", ProductCategory = tablet, Supplier = amazon });
            productDao.Add(new Product { Name = "Amazon Fire1", DefaultPrice = 89.0m, Currency = "USD", Description = "Amazon's latest Fire HD 8 tablet is a great value for media consumption.", ProductCategory = phone, Supplier = amazon });
            productDao.Add(new Product { Name = "Lenovo IdeaPad Miix 7001", DefaultPrice = 479.0m, Currency = "USD", Description = "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.", ProductCategory = tablet, Supplier = lenovo });
        }

    }



}
