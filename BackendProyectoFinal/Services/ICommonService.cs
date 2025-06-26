using BackendProyectoFinal.DTOs;

namespace BackendProyectoFinal.Services
{
    public interface ICommonService<T, TI, TU>
    {
        public List<string> Errors { get; }
        public Task<IEnumerable<T>> Get();
        public Task<CategoriaProductoDTO> GetById(int id);
        public Task<T> Add(TI beerInsertDTO);
        public Task<T> Update(TU beerUpdateDTO);
        public Task<T> Delete(int id);
        public bool Validate(TI beerInsertDTO);
        public bool Validate(TU beerUpdateDTO);
    }
}
