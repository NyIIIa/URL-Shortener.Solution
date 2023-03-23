namespace URL_Shortener.Client.Interfaces.Repository;

public interface IRepository<T> where T : class
{
    void Add(T entity);
}