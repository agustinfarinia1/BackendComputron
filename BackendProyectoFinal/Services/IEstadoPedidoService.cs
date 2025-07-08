using BackendProyectoFinal.DTOs.EstadoPedidoDTO;

namespace BackendProyectoFinal.Services
{
    public interface IEstadoPedidoService : ICommonService<EstadoPedidoDTO,EstadoPedidoInsertDTO,EstadoPedidoUpdateDTO>
    {
        public Task<EstadoPedidoDTO?> GetPrimero();
        public Task<EstadoPedidoDTO?> GetUltimo();
    }
}
