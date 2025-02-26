namespace JobWebApp.Repository;

public interface IRepository<T>
{
    void Add(T company);
    void Update(T company);
    void Delete(T company);
    Task<IEnumerable<T>> Get();
    Task<T> Get(int id);

    Task Save();
}