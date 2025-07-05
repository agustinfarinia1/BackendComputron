using Microsoft.IdentityModel.Tokens;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;
using BackendProyectoFinal.DTOs.EstadoPedidoDTO;
using BackendProyectoFinal.Mappers;

namespace BackendProyectoFinal.Services
{
    public class EstadoPedidoService : ICommonService<EstadoPedidoDTO, EstadoPedidoInsertDTO, EstadoPedidoUpdateDTO>
    {
        private IRepository<EstadoPedido> _repository;
        public List<string> Errors { get; }
        public EstadoPedidoService(IRepository<EstadoPedido> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<EstadoPedidoDTO>> Get()
        {
            var estados = await _repository.Get();
            // CONVIERTE LOS Estados A DTO
            return estados.Select(e =>
                EstadoPedidoMapper.ConvertEstadoPedidoToDTO(e));
        }

        public async Task<EstadoPedidoDTO?> GetById(int id)
        {
            var estadoPedido = await _repository.GetById(id);
            if (estadoPedido != null)
            {
                return EstadoPedidoMapper.ConvertEstadoPedidoToDTO(estadoPedido);
            }
            return null;
        }

        public async Task<EstadoPedidoDTO?> GetByField(string field)
        {
            var estadoPedido = _repository.Search(r => r.Nombre == field).FirstOrDefault();
            if (estadoPedido != null)
            {
                return EstadoPedidoMapper.ConvertEstadoPedidoToDTO(estadoPedido);
            }
            return null;
        }

        public async Task<EstadoPedidoDTO> Add(EstadoPedidoInsertDTO estadoPedidoInsertDTO)
        {
            var estadoPedido = new EstadoPedido()
            {
                Nombre = estadoPedidoInsertDTO.Nombre
            };
            await _repository.Add(estadoPedido);
            await _repository.Save();

            return EstadoPedidoMapper.ConvertEstadoPedidoToDTO(estadoPedido);
        }

        public async Task<EstadoPedidoDTO?> Update(EstadoPedidoUpdateDTO estadoPedidoUpdateDTO)
        {
            var estadoPedido = await _repository.GetById(estadoPedidoUpdateDTO.Id);
            if (estadoPedido != null)
            {
                estadoPedido.Nombre = estadoPedidoUpdateDTO.Nombre;

                _repository.Update(estadoPedido);
                await _repository.Save();

                return EstadoPedidoMapper.ConvertEstadoPedidoToDTO(estadoPedido);
            }
            return null;
        }

        public async Task<EstadoPedidoDTO> Delete(int id)
        {
            var estadoPedido = await _repository.GetById(id);
            if (estadoPedido != null)
            {
                var estadoPedidoDTO = EstadoPedidoMapper.ConvertEstadoPedidoToDTO(estadoPedido);

                _repository.Delete(estadoPedido);
                await _repository.Save();
                return estadoPedidoDTO;
            }
            return null;
        }

        public bool Validate(EstadoPedidoInsertDTO estadoPedidoDTO)
        {
            if (_repository.Search(e => 
                e.Nombre.ToUpper() == estadoPedidoDTO.Nombre.ToUpper())
                .Count() > 0)
            {
                Errors.Add("No puede existir un Estado con un nombre ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(EstadoPedidoUpdateDTO estadoPedidoDTO)
        {
            if (_repository.Search(e => 
                e.Nombre.ToUpper() == estadoPedidoDTO.Nombre.ToUpper()
                && estadoPedidoDTO.Id != e.EstadoPedidoID)
                .Count() > 0)
            {
                Errors.Add("No puede existir un Estado con un nombre ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
