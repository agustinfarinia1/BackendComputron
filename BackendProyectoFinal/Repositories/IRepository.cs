using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Repositories
{
    public interface IRepository <TEntity>
    {
        public Task<IEnumerable<TEntity>> Get();
        public Task<TEntity?> GetById(int id);
        public Task<TEntity?> GetByField(string field);
        public Task Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task Save();
        IEnumerable<TEntity> Search(Func<TEntity, bool> filter);
    }
}
