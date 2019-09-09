using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WideWorldImporters.API.Models
{
    public static class WideWroldImportersDbContextExtensions
    {
        public static IQueryable<StockItem> GetStockItems(this WideWorldImportersDbContext dbContext,
                                                          int pageSize = 10, int pageNumber = 1,
                                                          int? lastEditedBy = null, int? colorID = null,
                                                          int? embalagemID = null, int? fornecedorID = null, int? precoUnidade = null)
        {
            //Get quey do DbSet
            var query = dbContext.StockItems.AsQueryable();

            //Filtro do: 'LastEditedBy'
            if (lastEditedBy.HasValue)
                query = query.Where(item => item.LastEditedBy == lastEditedBy);

            //Filtro do: 'ColorID'
            if (colorID.HasValue)
                query = query.Where(item => item.ColorID == colorID);

            //Filtro do: 'OuterPackageID
            if (embalagemID.HasValue)
                query = query.Where(item => item.EmbalagemID == embalagemID);

            //Filtro do: 'SupplierID
            if (fornecedorID.HasValue)
                query = query.Where(item => item.FornecedorID == fornecedorID);

            //Filtro do: 'UnitPackageID'
            if (precoUnidade.HasValue)
                query = query.Where(item => item.PrecoUnidade == precoUnidade);

            return query;
        }

        public static async Task<StockItem> GetStockItemAsync(this WideWorldImportersDbContext dbContext, StockItem entity)
            => await dbContext.StockItems.FirstOrDefaultAsync(item => item.StockItemID == entity.StockItemID);

        public static async Task<StockItem> GetStockItemsByStockItemNameAsync(this WideWorldImportersDbContext dbContext, StockItem entity)
            => await dbContext.StockItems.FirstOrDefaultAsync(item => item.StockItemName == entity.StockItemName);

    }

    public static class IQueryableExtensions
    {
        public static IQueryable<TModel> Paging<TModel>(this IQueryable<TModel> query, int pageSize = 0, int pageNumber = 0) where TModel : class
            => pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
    }

}
