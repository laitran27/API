using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Models
{
    public static class DbContextExtensions
    {
        public static IQueryable<AR_ShopType> GetShopTypes(this WideWorldImportersDbContext dbContext, int pageSize = 10, int pageNumber = 1, string descr = null)
        {
            // Get query from DbSet
            var query = dbContext.AR_ShopTypes.AsQueryable();

            // Filter by: 'Descr'
            if (!string.IsNullOrEmpty(descr))
            {
                query = query.Where(item => item.Descr == descr);
            }

            return query;
        }
        public static async Task<AR_ShopType> GetShopTypesAsync(this WideWorldImportersDbContext dbContext, AR_ShopType entity)
            => await dbContext.AR_ShopTypes.FirstOrDefaultAsync(item => item.Code == entity.Code);
        public static async Task<AR_ShopType> GetShopTypesByNameAsync(this WideWorldImportersDbContext dbContext, AR_ShopType entity)
            => await dbContext.AR_ShopTypes.FirstOrDefaultAsync(item => item.Descr == entity.Descr);
        public static async Task<Batch> GetBatchAsync(this WideWorldImportersDbContext dbContext, Batch entity)
           => await dbContext.Batches.FirstOrDefaultAsync(item => item.BatNbr == entity.BatNbr && item.BranchID == entity.BranchID);


    }

    public static class IQueryableExtensions
    {
        public static IQueryable<TModel> Paging<TModel>(this IQueryable<TModel> query, int pageSize = 0, int pageNumber = 0) where TModel : class
            => pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
    }
}
