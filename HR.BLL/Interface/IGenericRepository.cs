namespace HR.BLL.Interface
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T item);
        void Update(T item);
        void Delete(T item);

    }
}
