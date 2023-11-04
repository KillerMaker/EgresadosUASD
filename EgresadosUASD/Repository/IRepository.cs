namespace EgresadosUASD.Repository
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> Get(int id = 0);
        public Task<int> Create(T entity);
        public Task Delete(int id);
    }
}
