namespace Database.Repositories;

public interface IRepository<T>
{
    Task AddAsync(T entity);
    void SaveChanges();
    Task<List<T>> GetByGtin(string gtin);
}
