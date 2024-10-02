using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Database;

public class BloggingContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(ConfigurationManager.AppSettings["DefaultConnection"],
                options => options.EnableRetryOnFailure());

        return new AppDbContext(optionsBuilder.Options);
    }
}