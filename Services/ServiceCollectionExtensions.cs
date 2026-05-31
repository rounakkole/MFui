
using Microsoft.Extensions.Options;
using TG.Blazor.IndexedDB;


namespace MFui.Services;

public static class ServiceCollectionExtensions
{
    public static void AddIndexedDBServices(this IServiceCollection services)
    {
        services.AddIndexedDB(dbStore =>
        {
            dbStore.DbName = "MFuiCacheDB";
            dbStore.Version = 1;

            dbStore.Stores.Add(new StoreSchema
            {
                Name = "InvestmentTable",
                PrimaryKey = new IndexSpec { Name = "schemeCode", KeyPath = "schemeCode", Auto = false } // SchemeCode as the unique primary key
      

            });

            dbStore.Stores.Add(new StoreSchema
            {
                Name = "OrderTable",
                PrimaryKey = new IndexSpec { Name = "id", KeyPath = "id", Auto = true },
                Indexes = new List<IndexSpec>
                {
                    // Create indexes if you want to search by specific fields later
                    new IndexSpec { Name = "schemeCode", KeyPath = "schemeCode", Auto = false }
                }
            });

            dbStore.Stores.Add(new StoreSchema
            {
                Name = "TotalsTable",
                PrimaryKey = new IndexSpec { Name = "id", KeyPath = "id", Auto = false }
            });
        });

    }
}

