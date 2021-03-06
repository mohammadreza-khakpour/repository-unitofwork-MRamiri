using Microsoft.EntityFrameworkCore;

namespace ApiProject.Models
{
    public interface IApiDbContext
    {
        DbSet<GoodCategory> GoodCategories { get; set; }
        DbSet<GoodEntry> GoodEntries { get; set; }
        DbSet<Good> Goods { get; set; }
        DbSet<SalesFactor> SalesFactors { get; set; }
    }
}