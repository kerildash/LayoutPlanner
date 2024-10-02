using Domain;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class BoxRepository(AppDbContext context) : IRepository<Box>
{
    public async Task AddAsync(Box box)
    {
        await context.AddAsync(box);
    }
    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public async Task<List<Box>> GetByGtin(string gtin)
    {
        return await context.Boxes.Where(b => b.CodeWithoutId.Contains(gtin)).OrderBy(b => b.Id).ToListAsync();
    }
}
