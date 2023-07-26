namespace Ltj.Domain.Interface.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<bool> UpdateAsync(T obj);        
        Task<bool> InsertAsync(T obj);        
        Task<T> Get(string id);
        Task<List<T>> GetAll();
    }
}
