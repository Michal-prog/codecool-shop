using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Daos.Conte;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    class ProductCategoryDaoMemory : IProductCategoryDao
    {
        //private List<ProductCategory> data = new List<ProductCategory>();
        //private static ProductCategoryDaoMemory instance = null;
        private Context context { get; set;}

        public ProductCategoryDaoMemory(Context context)
        {
            this.context = context;
        }

        //public static ProductCategoryDaoMemory GetInstance()
        //{
        //    if (instance == null)
        //    {
        //        instance = new ProductCategoryDaoMemory();
        //    }

        //    return instance;
        //}

        public void Add(ProductCategory item)
        {
            item.Id = context.Products.FirstOrDefault(x =>)
            data.Add(item);
        }

        public void Remove(int id)
        {
            data.Remove(this.Get(id));
        }

        public ProductCategory Get(int id)
        {
            return data.Find(x => x.Id == id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return data;
        }
        //public List<ProductCategory> GetAllList()
        //{
        //    return data;
        //}

    }
}
