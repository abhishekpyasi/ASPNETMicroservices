﻿using Catalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public interface IProductRepository

    {
        public Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetProduct(string Id);

        Task<IEnumerable<Product>> GetProductByName(string Name);

        Task<IEnumerable<Product>> GetProductByCatagory(string CategoryName);

        Task CreateProduct(Product product);


        Task<bool> UpdateProduct(Product product);

        Task<bool> DeleteProduct(String Id);


    }
}
