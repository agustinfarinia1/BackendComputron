using BackendProyectoFinal.DTOs.CategoriaProductoDTO;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;
using BackendProyectoFinal.Utils.Mappers;
using Microsoft.IdentityModel.Tokens;

namespace BackendProyectoFinal.Services
{
    public class CategoriaProductoService : ICommonService<CategoriaProductoDTO, CategoriaProductoInsertDTO, CategoriaProductoUpdateDTO>
    {
        private IRepository<CategoriaProducto> _repository;
        public List<string> Errors { get; }
        public CategoriaProductoService(
            IRepository<CategoriaProducto> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<CategoriaProductoDTO>> Get() 
        {
            var categorias = await _repository.Get();
            // CONVIERTE LOS TIPOS DE CATEGORIA A DTO
            return categorias.Select(c => 
            CategoriaProductoMapper.ConvertCategoriaProductoToDTO(c)
            );
        }
        
        public async Task<CategoriaProductoDTO?> GetById(int id)
        {
            var categoria = await _repository.GetById(id);
            if (categoria != null)
            {
                return CategoriaProductoMapper.ConvertCategoriaProductoToDTO(categoria);
            }
            return null;
        }

        public async Task<CategoriaProductoDTO?> GetByField(string field)
        {
            var categoria = _repository.Search(u => u.Nombre == field).FirstOrDefault(); ;
            if (categoria != null)
            {
                return CategoriaProductoMapper.ConvertCategoriaProductoToDTO(categoria);
            }
            return null;
        }

        public async Task<CategoriaProductoDTO> Add(CategoriaProductoInsertDTO categoriaInsertDTO)
        {
            var categoria = new CategoriaProducto()
            {
                Nombre = categoriaInsertDTO.Nombre
            };
            await _repository.Add(categoria);
            await _repository.Save();

            return CategoriaProductoMapper.ConvertCategoriaProductoToDTO(categoria) ;
        }

        public async Task<CategoriaProductoDTO> Update(CategoriaProductoUpdateDTO categoriaUpdateDTO)
        {
            var categoria = await _repository.GetById(categoriaUpdateDTO.Id);
            if (categoria != null)
            {
                categoria.Nombre = categoriaUpdateDTO.Nombre;

                _repository.Update(categoria);
                await _repository.Save();

                return CategoriaProductoMapper.ConvertCategoriaProductoToDTO(categoria);
            }
            return null;
        }

        public async Task<CategoriaProductoDTO> Delete(int id)
        {
            var categoria = await _repository.GetById(id);
            if (categoria != null)
            {
                var categoriaDTO =  CategoriaProductoMapper.ConvertCategoriaProductoToDTO(categoria);

                _repository.Delete(categoria);
                await _repository.Save();
                return categoriaDTO;
            }
            return null;
        }

        public bool Validate(CategoriaProductoInsertDTO categoriaDTO)
        {
            if (_repository.Search(c => c.Nombre.ToUpper() == categoriaDTO.Nombre.ToUpper()).Count() > 0)
            {
                Errors.Add("No puede existir una categoria con un nombre ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(CategoriaProductoUpdateDTO categoriaDTO)
        {
            if (_repository.Search(
                c => c.Nombre.ToUpper() == categoriaDTO.Nombre.ToUpper()
                && categoriaDTO.Id != c.CategoriaProductoID).Count() > 0)
            {
                Errors.Add("No puede existir una categoria con un nombre ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
