﻿using G20.Core;
using G20.Core.Domain;
using System.Threading.Tasks;

namespace G20.Service.Products
{
    public interface IProductService
    {
        Task<IPagedList<Product>> GetProductsAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<Product> GetByIdAsync(int id);
        Task InsertAsync(Product entity);
        Task UpdateAsync(Product entity);
        Task DeleteAsync(Product entity);
    }
}