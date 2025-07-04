using BackendProyectoFinal.DTOs;
using BackendProyectoFinal.Mappers;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace BackendProyectoFinal.Services
{
    public class ProductoService : ICommonService<ProductoDTO, ProductoInsertDTO, ProductoUpdateDTO>
    {
        private IRepository<Producto> _repository;
        public List<string> Errors { get; }
        public ProductoService(
            IRepository<Producto> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<ProductoDTO>> Get()
        {
            var productos = await _repository.Get();
            // CONVIERTE LOS Usuarios A DTO
            return productos.Where(p => p.Eliminado == false) // 🔍 Filtra los que no están eliminados
                .Select(p =>
                    ProductoMapper.ConvertProductoToDTO(p));
        }

        public async Task<ProductoDTO> GetById(int id)
        {
            var producto = await _repository.GetById(id);
            if (producto != null)
            {
                return ProductoMapper.ConvertProductoToDTO(producto);
            }
            return null;
        }

        // Por ahora la busqueda es por titulo, podria ser por CategoriaProducto
        public async Task<ProductoDTO> GetByField(string field)
        {
            var productos = _repository.Search(p => p.Titulo == field).FirstOrDefault(); ;
            if (productos != null)
            {
                return ProductoMapper.ConvertProductoToDTO(productos);
            }
            return null;
        }

        public async Task<ProductoDTO> Add(ProductoInsertDTO productoInsertDTO)
        {
            var producto = ProductoMapper.ConvertDTOToProducto(productoInsertDTO);
            await _repository.Add(producto);
            await _repository.Save();

            return ProductoMapper.ConvertProductoToDTO(producto);
        }

        public async Task<ProductoDTO> Update(ProductoUpdateDTO productoUpdateDTO)
        {
            var producto = await _repository.GetById(productoUpdateDTO.Id);
            if (producto != null)
            {
                ProductoMapper.ActualizarProducto(producto, productoUpdateDTO);

                _repository.Update(producto);
                await _repository.Save();

                return ProductoMapper.ConvertProductoToDTO(producto);
            }
            return null;
        }

        public async Task<ProductoDTO> Delete(int id)
        {
            var producto = await _repository.GetById(id);
            if (producto != null)
            {
                producto.Eliminado = true;
                var productoDTO = ProductoMapper.ConvertProductoToDTO(producto);

                _repository.Delete(producto);
                await _repository.Save();
                return productoDTO;
            }
            return null;
        }

        public bool Validate(ProductoInsertDTO productoDTO)
        {
            if (_repository.Search(p => p.Titulo == productoDTO.Titulo).Count() > 0)
            {
                Errors.Add("No puede existir un producto con un titulo ya existente");
            }
            if (_repository.Search(p => p.CodigoML == productoDTO.CodigoML).Count() > 0)
            {
                Errors.Add("No puede existir un producto con un codigoML ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(ProductoUpdateDTO productoDTO)
        {
            if (_repository.Search(p => p.Titulo == productoDTO.Titulo
                && productoDTO.Id != p.ProductoID).Count() > 0)
            {
                Errors.Add("No puede existir un producto con un titulo ya existente");
            }
            if (_repository.Search(p => p.CodigoML == productoDTO.CodigoML 
                && productoDTO.Id != p.ProductoID).Count() > 0)
            {
                Errors.Add("No puede existir un producto con un codigoML ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
