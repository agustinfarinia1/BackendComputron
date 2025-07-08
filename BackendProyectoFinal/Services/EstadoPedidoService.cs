using BackendProyectoFinal.DTOs.EstadoPedidoDTO;
using BackendProyectoFinal.Mappers;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BackendProyectoFinal.Services
{
    public class EstadoPedidoService : IEstadoPedidoService
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
                EstadoPedidoMapper.ConvertirModelToDTO(e));
        }

        public async Task<EstadoPedidoDTO?> GetById(int id)
        {
            var estadoPedido = await _repository.GetById(id);
            if (estadoPedido != null)
            {
                return EstadoPedidoMapper.ConvertirModelToDTO(estadoPedido);
            }
            return null;
        }

        public async Task<EstadoPedidoDTO?> GetByField(string field)
        {
            var estadoPedido = _repository.Search(r => r.Nombre == field).FirstOrDefault();
            if (estadoPedido != null)
            {
                return EstadoPedidoMapper.ConvertirModelToDTO(estadoPedido);
            }
            return null;
        }

        public async Task<EstadoPedidoDTO?> GetPrimero()
        {
            var estados = await Get();
            var ultimoEstado = await GetUltimo();
            var estadoPedido = ultimoEstado;
            if (estados.Any() && ultimoEstado != null)
            {
                var busqueda = false;
                for(int i = 0; i>estados.Count(); i++)
                {
                    var anteriorEstado = estados.FirstOrDefault(e => e.EstadoSiguienteId == ultimoEstado.Id);
                    if(anteriorEstado == null)
                    {
                        estadoPedido = ultimoEstado;
                        busqueda = true;
                        break;
                    }
                    ultimoEstado = anteriorEstado;
                }
                if (busqueda)
                {
                    return estadoPedido;
                }
            }
            return null;
        }

        public async Task<EstadoPedidoDTO?> GetUltimo()
        {
            var estados = await Get();
            if (estados.Any())
            {
                var estadoPedido = estados.FirstOrDefault(e => e.EstadoSiguienteId == null);
                return estadoPedido;
            }
            return null;
        }

        public async Task<EstadoPedidoDTO> AddSimple(EstadoPedidoInsertDTO estadoPedidoInsertDTO)
        {
            var estadoPedido = EstadoPedidoMapper.ConvertirDTOToModel(estadoPedidoInsertDTO);
            await _repository.Add(estadoPedido);
            await _repository.Save();

            return EstadoPedidoMapper.ConvertirModelToDTO(estadoPedido);
        }

        public async Task<EstadoPedidoDTO> Add(EstadoPedidoInsertDTO estadoPedidoInsertDTO)
        {
            var estados = await Get();
            // Si no hay estados, no existe un siguiente
            if (!estados.Any())
            {
                estadoPedidoInsertDTO.EstadoSiguienteId = null;
            }
            var nuevoEstado = await AddSimple(estadoPedidoInsertDTO);
            estados = await Get();
            if (estados.Count() >= 2)
            {
                // Conecta cuando tiene un EstadoSiguienteID
                if (estadoPedidoInsertDTO.EstadoSiguienteId != null)
                {
                    var estadoSeleccionado = estados.FirstOrDefault(e => e.Id == estadoPedidoInsertDTO.EstadoSiguienteId);
                    if (estadoSeleccionado != null)
                    {
                        var anteriorEstado = estados.FirstOrDefault(e2 => e2.EstadoSiguienteId == estadoSeleccionado.Id);
                        if (anteriorEstado != null && anteriorEstado.Id != nuevoEstado.Id)
                        {
                            var anteriorEstadoDTO = EstadoPedidoMapper.GenerarEstadoPedidoUpdateDTO(anteriorEstado.Id,anteriorEstado.Nombre,nuevoEstado.Id);
                            await Update(anteriorEstadoDTO);
                        }
                    }
                }
                // Conecta cuando el estado es el ultimo
                else
                {
                    var ultimoEstado = await GetUltimo();
                    if (ultimoEstado != null)
                    {
                        ultimoEstado.EstadoSiguienteId = nuevoEstado.Id;
                        var ultimoEstadoDTO = EstadoPedidoMapper.GenerarEstadoPedidoUpdateDTO(ultimoEstado.Id,ultimoEstado.Nombre,nuevoEstado.Id);
                        await Update(ultimoEstadoDTO);
                    }
                }
            }
            return nuevoEstado;
        }

        public async Task<EstadoPedidoDTO?> Update(EstadoPedidoUpdateDTO estadoPedidoUpdateDTO)
        {
            var estadoPedido = await _repository.GetById(estadoPedidoUpdateDTO.Id);
            if (estadoPedido != null)
            {
                EstadoPedidoMapper.ActualizarEstadoPedido(estadoPedido,estadoPedidoUpdateDTO);
                _repository.Update(estadoPedido);
                await _repository.Save();

                return EstadoPedidoMapper.ConvertirModelToDTO(estadoPedido);
            }
            return null;
        }

        public async Task<EstadoPedidoDTO?> Delete(int id)
        {
            var estadoPedido = await _repository.GetById(id);
            if (estadoPedido != null)
            {
                var estadoPedidoDTO = EstadoPedidoMapper.ConvertirModelToDTO(estadoPedido);
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
