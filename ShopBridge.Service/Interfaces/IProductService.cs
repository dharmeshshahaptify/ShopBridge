using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Service.Interfaces
{
    public interface IProductService
    {
        IQueryable<Product> GetProducts();

        Product GetProduct(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);

        IQueryable<Product> GetByCondition(Expression<Func<Product, bool>> expression);
    }
}
