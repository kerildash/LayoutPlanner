using Domain;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class ItemRepository(AppDbContext context) : IItemRepository
{
    public bool Exists(string code)
    {
        return context.Items.Any(i => i.Code == code);
    }
    public async Task AddAsync(Item item)
    {
        await context.AddAsync(item);
    }
    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public async Task<List<Item>> GetByGtin(string gtin)
    {
        return await context.Items.Where(i => i.Code.Contains(gtin)).OrderBy(p => p.Id).ToListAsync();
    }
}
