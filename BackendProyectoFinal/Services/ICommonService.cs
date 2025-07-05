namespace BackendProyectoFinal.Services
{
    public interface ICommonService<T, TI, TU>
    {
        public List<string> Errors { get; }
        public Task<IEnumerable<T>> Get();
        public Task<T?> GetById(int id);
        public Task<T?> GetByField(string field);
        public Task<T> Add(TI modelInsertDTO);
        public Task<T?> Update(TU modelUpdateDTO);
        public Task<T?> Delete(int id);
        public bool Validate(TI modelInsertDTO);
        public bool Validate(TU modelUpdateDTO);
    }
}
