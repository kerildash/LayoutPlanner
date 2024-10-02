using System.Configuration;
using System.Xml;
using Domain;
using Microsoft.EntityFrameworkCore;
namespace Database;

public class AppDbContext : DbContext
{
    public DbSet<Pallet> Pallets { get; set; }
    public DbSet<Box> Boxes { get; set; }
    public DbSet<Item> Items { get; set; }

    public DbContextOptions<AppDbContext> Options { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Options = options;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConfigurationManager.AppSettings["DefaultConnection"]);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Box>()
            .Ignore(b => b.Code)
            .Property<string>("Code")
            .HasComputedColumnSql("CASE WHEN \"CodeWithoutId\" IS NULL THEN \"Id\"::varchar(10) WHEN \"Id\"::varchar(10)  IS NULL THEN \"CodeWithoutId\" ELSE \"CodeWithoutId\" || \"Id\"::varchar(10) END", stored: true);
            

        modelBuilder.Entity<Pallet>()
            .Ignore(b => b.Code)
            .Property<string>("Code")
            .HasComputedColumnSql("CASE WHEN \"CodeWithoutId\" IS NULL THEN \"Id\"::varchar(10) WHEN \"Id\"::varchar(10)  IS NULL THEN \"CodeWithoutId\" ELSE \"CodeWithoutId\" || \"Id\"::varchar(10) END", stored: true);
    }
}
