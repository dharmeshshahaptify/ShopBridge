using DAL;
using DAL.Interfaces;
using ShopBridge.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Service.Services
{
    class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepo;

        public ProductService(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
           
        }

        public void AddProduct(Product product)
        {
            _productRepo.Add(product);
            _productRepo.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            _productRepo.Delete(id);
            _productRepo.SaveChanges();
        }

        public Product GetProduct(int id)
        {
            return _productRepo.Find(id);
        }

        public IQueryable<Product> GetProducts()
        {
            return _productRepo.GetAll().OrderBy(item => item.Name);
        }

        public IQueryable<Product> GetByCondition(Expression<Func<Product, bool>> expression)
        {
            return _productRepo.GetByCondition(expression);

        }

        public void UpdateProduct(Product product)
        {
            _productRepo.Update(product);
                     
            _productRepo.SaveChanges();
        }




    }
}
