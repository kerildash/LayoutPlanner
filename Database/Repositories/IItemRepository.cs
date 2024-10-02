using Domain;

namespace Database.Repositories;

public interface IItemRepository : IRepository<Item>
{
    bool Exists(string code);
}
