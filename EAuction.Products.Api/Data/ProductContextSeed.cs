using System.Collections.Generic;
using EAuction.Products.Api.Entities;
using MongoDB.Driver;

namespace EAuction.Products.Api.Data
{
    public class ProductContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetConfigureProducts());
            }
        }

        private static IEnumerable<Product> GetConfigureProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Category = "Phone",
                    Description = "Phone",
                    ImageFile = "image1",
                    Name = "IPhone",
                    Price = 1200M,
                    Summary = "iphone"
                },
                new Product
                {
                    Category = "Phone",
                    Description = "Phone",
                    ImageFile = "image1",
                    Name = "Xiaomi",
                    Price = 1200M,
                    Summary = "iphone"
                },
                new Product
                {
                    Category = "Phone",
                    Description = "Phone",
                    ImageFile = "image1",
                    Name = "Samsung",
                    Price = 1200M,
                    Summary = "iphone"
                },
                new Product
                {
                    Category = "Phone",
                    Description = "Phone",
                    ImageFile = "image1",
                    Name = "Huawei",
                    Price = 1200M,
                    Summary = "iphone"
                },
            };
        }
    }
}