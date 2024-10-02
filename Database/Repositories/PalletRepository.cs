using Domain;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class PalletRepository(AppDbContext context) : IRepository<Pallet>
{
    public async Task AddAsync(Pallet pallet)
    {
        await context.AddAsync(pallet);
    }
    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public async Task<List<Pallet>> GetByGtin(string gtin)
    {
        return await context.Pallets.Where(p => p.CodeWithoutId.Contains(gtin)).OrderBy(p => p.Id).ToListAsync();
    }
}
