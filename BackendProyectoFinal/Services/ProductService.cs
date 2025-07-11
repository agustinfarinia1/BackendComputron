using BackendProyectoFinal.Repositories;
using Microsoft.IdentityModel.Tokens;
using BackendProyectoFinal.Mappers;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.ProductDTO;

namespace BackendProyectoFinal.Services
{
    public class ProductService : ICommonService<ProductDTO, ProductInsertDTO, ProductUpdateDTO>
    {
        private IRepository<Product> _repository;
        public List<string> Errors { get; }
        public ProductService(
            IRepository<Product> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<ProductDTO>> Get()
        {
            var products = await _repository.Get();
            // CONVIERTE LOS Usuarios A DTO
            return products.Where(p => p.Eliminated == false) // 🔍 Filtra los que no están eliminados
                .Select(p =>
                    ProductMapper.ConvertProductToDTO(p));
        }

        public async Task<ProductDTO?> GetById(int id)
        {
            var product = await _repository.GetById(id);
            if (product != null)
            {
                return ProductMapper.ConvertProductToDTO(product);
            }
            return null;
        }

        // Por ahora la busqueda es por titulo, podria ser por CategoriaProducto
        public async Task<ProductDTO?> GetByField(string field)
        {
            var products = _repository.Search(p => p.Title == field).FirstOrDefault(); ;
            if (products != null)
            {
                return ProductMapper.ConvertProductToDTO(products);
            }
            return null;
        }

        public async Task<ProductDTO> Add(ProductInsertDTO productoInsertDTO)
        {
            var product = ProductMapper.ConvertDTOToProduct(productoInsertDTO);
            await _repository.Add(product);
            await _repository.Save();

            return ProductMapper.ConvertProductToDTO(product);
        }

        public async Task<ProductDTO> Update(ProductUpdateDTO productoUpdateDTO)
        {
            var producto = await _repository.GetById(productoUpdateDTO.Id);
            if (producto != null)
            {
                ProductMapper.UpdateProduct(producto, productoUpdateDTO);

                _repository.Update(producto);
                await _repository.Save();

                return ProductMapper.ConvertProductToDTO(producto);
            }
            return null;
        }

        public async Task<ProductDTO> Delete(int id)
        {
            var producto = await _repository.GetById(id);
            if (producto != null)
            {
                producto.Eliminated = true;
                var productoDTO = ProductMapper.ConvertProductToDTO(producto);

                _repository.Delete(producto);
                await _repository.Save();
                return productoDTO;
            }
            return null;
        }

        public bool Validate(ProductInsertDTO productoDTO)
        {
            if (_repository.Search(p => p.Title.ToUpper() == productoDTO.Title.ToUpper()).Count() > 0)
            {
                Errors.Add("No puede existir un producto con un titulo ya existente");
            }
            if (_repository.Search(p => p.MLCode.ToUpper() == productoDTO.MLCode.ToUpper()).Count() > 0)
            {
                Errors.Add("No puede existir un producto con un codigoML ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(ProductUpdateDTO productoDTO)
        {
            if (_repository.Search(p => p.Title.ToUpper() == productoDTO.Title.ToUpper()
                && productoDTO.Id != p.ProductID).Count() > 0)
            {
                Errors.Add("No puede existir un producto con un titulo ya existente");
            }
            if (_repository.Search(p => p.MLCode.ToUpper() == productoDTO.MLCode.ToUpper()
                && productoDTO.Id != p.ProductID).Count() > 0)
            {
                Errors.Add("No puede existir un producto con un codigoML ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
