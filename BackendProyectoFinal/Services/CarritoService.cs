using BackendProyectoFinal.DTOs.CarritoDTO;
using BackendProyectoFinal.Mappers;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace BackendProyectoFinal.Services
{
    public class CarritoService : ICommonService<CarritoDTO, CarritoInsertDTO, CarritoUpdateDTO>
    {
        private IRepository<Carrito> _repository;
        public List<string> Errors { get; }
        public CarritoService(IRepository<Carrito> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<CarritoDTO>> Get()
        {
            var carritos = await _repository.Get();
            // CONVIERTE LOS Carritos A DTO
            return carritos.Select(c 
                => CarritoMapper.ConvertCarritoToDTO(c));
        }

        public async Task<CarritoDTO?> GetById(int id)
        {
            var carrito = await _repository.GetById(id);
            if (carrito != null)
            {
                return CarritoMapper.ConvertCarritoToDTO(carrito);
            }
            return null;
        }

        public async Task<CarritoDTO?> GetByField(string field)
        {
            var carrito = _repository.Search(c => c.UsuarioID == int.Parse(field)).FirstOrDefault();
            if (carrito != null)
            {
                return CarritoMapper.ConvertCarritoToDTO(carrito);
            }
            return null;
        }

        public async Task<CarritoDTO> Add(CarritoInsertDTO carritoInsertDTO)
        {
            var carrito = CarritoMapper.ConvertDTOToModel(carritoInsertDTO);
            await _repository.Add(carrito);
            await _repository.Save();

            return CarritoMapper.ConvertCarritoToDTO(carrito);
        }

        public async Task<CarritoDTO> Update(CarritoUpdateDTO carritoUpdateDTO)
        {
            var carrito = await _repository.GetById(carritoUpdateDTO.Id);
            if (carrito != null)
            {
                CarritoMapper.ActualizarCarrito(carrito,carritoUpdateDTO);

                _repository.Update(carrito);
                await _repository.Save();

                return CarritoMapper.ConvertCarritoToDTO(carrito);
            }
            return null;
        }

        public async Task<CarritoDTO> Delete(int id)
        {
            var carrito = await _repository.GetById(id);
            if (carrito != null)
            {
                var carritoDTO = CarritoMapper.ConvertCarritoToDTO(carrito);

                _repository.Delete(carrito);
                await _repository.Save();
                return carritoDTO;
            }
            return null;
        }

        public bool Validate(CarritoInsertDTO carritoDTO)
        {
            if (_repository.Search(c 
                => c.UsuarioID == carritoDTO.UsuarioId)
                .Count() > 0)
            {
                Errors.Add("No puede existir un carrito con un usuario ya existente");
            }
            if (_repository.Search(c 
                => c.ListaCarrito.Count() > 0)
                .Count() > 0)
            {
                Errors.Add("El carrito tiene que contener productos");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(CarritoUpdateDTO carritoDTO)
        {
            if (_repository.Search(c 
                => c.UsuarioID == carritoDTO.UsuarioId
                && carritoDTO.Id != c.CarritoID)
                .Count() > 0)
            {
                Errors.Add("No puede existir un carrito con un usuario ya existente");
            }
            if (_repository.Search(c
                => c.ListaCarrito.Count() > 0
                && carritoDTO.Id != c.CarritoID)
                .Count() > 0)
            {
                Errors.Add("El carrito tiene que contener productos");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
