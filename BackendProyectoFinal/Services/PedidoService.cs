using BackendProyectoFinal.DTOs.PedidoDTO;
using BackendProyectoFinal.Mappers;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace BackendProyectoFinal.Services
{
    public class PedidoService : ICommonService<PedidoDTO, PedidoInsertDTO, PedidoUpdateDTO>
    {
        private IRepository<Pedido> _repository;
        public List<string> Errors { get; }
        public PedidoService(IRepository<Pedido> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<PedidoDTO>> Get()
        {
            var pedidos = await _repository.Get();
            // CONVIERTE LOS Pedidos A DTO
            return pedidos.Select(p
                => PedidoMapper.ConvertPedidoToDTO(p));
        }

        public async Task<PedidoDTO?> GetById(int id)
        {
            var pedido = await _repository.GetById(id);
            if (pedido != null)
            {
                return PedidoMapper.ConvertPedidoToDTO(pedido);
            }
            return null;
        }

        public async Task<PedidoDTO?> GetByField(string field)
        {
            var pedido = _repository.Search(p => p.UsuarioID == int.Parse(field)).FirstOrDefault();
            if (pedido != null)
            {
                return PedidoMapper.ConvertPedidoToDTO(pedido);
            }
            return null;
        }

        public async Task<PedidoDTO> Add(PedidoInsertDTO pedidoInsertDTO)
        {
            var pedido = PedidoMapper.ConvertDTOToModel(pedidoInsertDTO);
            await _repository.Add(pedido);
            await _repository.Save();

            return PedidoMapper.ConvertPedidoToDTO(pedido);
        }

        public async Task<PedidoDTO> Update(PedidoUpdateDTO pedidoUpdateDTO)
        {
            var pedido = await _repository.GetById(pedidoUpdateDTO.Id);
            if (pedido != null)
            {
                PedidoMapper.ActualizarPedido(pedido, pedidoUpdateDTO);

                _repository.Update(pedido);
                await _repository.Save();

                return PedidoMapper.ConvertPedidoToDTO(pedido);
            }
            return null;
        }

        public async Task<PedidoDTO> Delete(int id)
        {
            var pedido = await _repository.GetById(id);
            if (pedido != null)
            {
                var pedidoDTO = PedidoMapper.ConvertPedidoToDTO(pedido);

                _repository.Delete(pedido);
                await _repository.Save();
                return pedidoDTO;
            }
            return null;
        }

        public bool Validate(PedidoInsertDTO pedidoDTO)
        {
            if (_repository.Search(p
                => p.UsuarioID == pedidoDTO.UsuarioId)
                .Count() > 0)
            {
                Errors.Add("No puede existir un pedido con un usuario ya existente");
            }
            if (_repository.Search(p
                => p.ListaPedido.Count() > 0)
                .Count() > 0)
            {
                Errors.Add("La lista de Pedidos tiene que contener productos");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(PedidoUpdateDTO pedidoDTO)
        {
            if (_repository.Search(p
                => p.UsuarioID == pedidoDTO.UsuarioId
                && pedidoDTO.Id != p.PedidoID)
                .Count() > 0)
            {
                Errors.Add("No puede existir un pedido con un usuario ya existente");
            }
            if (_repository.Search(p
                => p.ListaPedido.Count() > 0
                && pedidoDTO.Id != p.PedidoID)
                .Count() > 0)
            {
                Errors.Add("La lista de Pedidos tiene que contener productos");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
