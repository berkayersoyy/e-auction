using EAuction.Products.Api.Entities;
using MongoDB.Driver;

namespace EAuction.Products.Api.Data.Abstractions
{
    public interface IProductContext
    {
        IMongoCollection<Product> Products { get; set; }
    }
}