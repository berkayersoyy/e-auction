using System.Collections.Generic;
using System.Threading.Tasks;
using EAuction.Products.Api.Data.Abstractions;
using EAuction.Products.Api.Entities;
using EAuction.Products.Api.Repositories.Abstractions;
using MongoDB.Driver;

namespace EAuction.Products.Api.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private readonly IProductContext _context;

        public ProductRepository(IProductContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var result = await _context.Products.Find(p => true).ToListAsync();
            return result;
        }

        public async Task<Product> GetProduct(string id)
        {
            var result = await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            var filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
            var result = await _context.Products.Find(filter).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            var result = await _context.Products.Find(filter).ToListAsync();
            return result;
        }

        public async Task Create(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> Update(Product product)
        {
            var updatedResult =
                await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            var result = updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
            return result;
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<Product>.Filter.Eq(m => m.Id,id);
            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);
            var result = deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            return result;
        }
    }
}