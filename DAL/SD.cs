using DAL.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL;

public class SD
{
    public static async Task Init(IServiceProvider scopeServiceProvider)
    {
        var context = scopeServiceProvider.GetRequiredService<DefaultDbContext>();
        await context.Database.MigrateAsync();
        if (!context.Tests.Any())
        {
            for (int i = 0; i < 100000; i++)
            {
                var rnd = new Random();
                var model = new TestModel()
                {
                    Id = Guid.NewGuid(),
                    RandomInt = rnd.Next(),
                };
                context.Tests.Add(model);
            }

            await context.SaveChangesAsync();
        }

       
    }
}