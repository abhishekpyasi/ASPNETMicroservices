using Dapper;
using Discount.API.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository

    {
        private readonly IConfiguration _confuguration;

        public DiscountRepository(IConfiguration config)
        {

            _confuguration = config;
        }


        public async Task<Coupen> GetDiscount(string ProductName)
        {
            using var connection = new NpgsqlConnection
                (_confuguration.GetValue<string>("DatabaseSettings:ConnactionString"));

            var coupen = await connection.QueryFirstOrDefaultAsync<Coupen>
                ("Select * from Coupen where ProductName =@ProductName", new { ProductName = ProductName });

            if (coupen == null)
            {

                return new Coupen { ProductName = "No Discount", Amount = 0, Description = "No Discount Description" };
            }

            return coupen;

        }

        public async Task<bool> CreateDiscount(Coupen coupon)
        {
            using var connection = new NpgsqlConnection(_confuguration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =
                await connection.ExecuteAsync
                    ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                            new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> UpdateDiscount(Coupen coupon)
        {
            using var connection = new NpgsqlConnection(_confuguration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                    ("UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                            new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

            if (affected == 0)
                return false;

            return true;
        }

        

        async Task<bool> IDiscountRepository.DeleteDiscount(string  ProductName)
        {
            using var connection = new NpgsqlConnection(_confuguration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = ProductName });

            if (affected == 0)
                return false;

            return true;
        }
    }
}
