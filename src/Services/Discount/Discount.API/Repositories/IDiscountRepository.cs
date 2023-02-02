using Discount.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    interface IDiscountRepository
    {

        Task<Coupen> GetDiscount(string ProductName);

        Task<bool> CreateDiscount(Coupen coupen);

        Task<bool> UpdateDiscount(Coupen coupen);

        Task<bool> DeleteDiscount(string  ProductName);




    }
}
