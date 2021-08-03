using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EAuction.Products.Api.Entities;

namespace EAuction.Products.Api.Repositories.Abstractions
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<IEnumerable<Product>> GetProductsByCategory(string categoryName);

        Task Create(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(string id);
    }
}