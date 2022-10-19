using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository

    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context               )
        {
            _context = context; 

        }

        async Task IProductRepository.CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

       async Task<bool> IProductRepository.DeleteProduct(string Id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, Id);

            DeleteResult deleteResult = await _context
                                                .Products
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        async Task<Product> IProductRepository.GetProduct(string Id)


        {
            return await  _context.Products.Find(p => p.Id == Id).FirstOrDefaultAsync();

        }

        async Task<IEnumerable<Product>> IProductRepository.GetProductByCatagory(string CategoryName)
        {

            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, CategoryName);

            return await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }

        async Task<IEnumerable<Product>> IProductRepository.GetProductByName(string Name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, Name);

            return await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();

        }

        async  Task<IEnumerable<Product>> IProductRepository.GetProducts()
        {

            return await _context.Products.Find(p => true).ToListAsync();
        }

       async  Task<bool> IProductRepository.UpdateProduct(Product product)
        {
            var updateResult = await _context
                                                   .Products
                                                   .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}
