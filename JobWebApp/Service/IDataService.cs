namespace JobWebApp.Service;

public interface IDataService<T>
{
    Task Add(T model);
    Task Update(T model);
    Task Delete(int id);
    Task<IEnumerable<T>> Get();
    Task<T> Get(int id);
}