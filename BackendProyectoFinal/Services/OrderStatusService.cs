using BackendProyectoFinal.Repositories;
using Microsoft.IdentityModel.Tokens;
using BackendProyectoFinal.Mappers;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.Order.OrderStatus;

namespace BackendProyectoFinal.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private IRepository<OrderStatus> _repository;
        public List<string> Errors { get; }
        public OrderStatusService(IRepository<OrderStatus> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<OrderStatusDTO>> Get()
        {
            var statuses = await _repository.Get();
            // Convierte los OrderStatus A DTO
            return statuses.Select(e =>
                OrderStatusMapper.ConvertirModelToDTO(e));
        }

        public async Task<OrderStatusDTO?> GetById(int id)
        {
            var orderStatus = await _repository.GetById(id);
            if (orderStatus != null)
            {
                return OrderStatusMapper.ConvertirModelToDTO(orderStatus);
            }
            return null;
        }

        public async Task<OrderStatusDTO?> GetByField(string field)
        {
            var orderStatus = _repository.Search(r => r.Name == field).FirstOrDefault();
            if (orderStatus != null)
            {
                return OrderStatusMapper.ConvertirModelToDTO(orderStatus);
            }
            return null;
        }

        public async Task<OrderStatusDTO?> GetFirstOrderStatus()
        {
            var statuses = await Get();
            var current = await GetLastOrderStatus();

            if (!statuses.Any() || current == null)
                return null;

            while (true)
            {
                var previous = statuses.FirstOrDefault(s => s.NextOrderStatusId == current.Id);
                if (previous == null)
                    return current;
                current = previous;
            }
        }

        public async Task<OrderStatusDTO?> GetNextOrderStatus(int orderStatusID)
        {
            var status = await GetById(orderStatusID);
            if (status != null && status.NextOrderStatusId.HasValue)
            {
                var nextStatus = await GetById(status.NextOrderStatusId.Value);
                return nextStatus;
            }
            return null;
        }

        public async Task<OrderStatusDTO?> GetLastOrderStatus()
        {
            var statuses = await Get();
            if (statuses.Any())
            {
                var lastStatus = statuses.FirstOrDefault(e => e.NextOrderStatusId == null);
                return lastStatus;
            }
            return null;
        }

        public async Task<OrderStatusDTO> AddSimple(OrderStatusInsertDTO orderStatusInsertDTO)
        {
            var orderStatus = OrderStatusMapper.ConvertirDTOToModel(orderStatusInsertDTO);
            await _repository.Add(orderStatus);
            await _repository.Save();

            return OrderStatusMapper.ConvertirModelToDTO(orderStatus);
        }

        public async Task<OrderStatusDTO> Add(OrderStatusInsertDTO orderStatusInsertDTO)
        {
            var statuses = await Get();
            // Si no hay estados, no existe un siguiente
            if (!statuses.Any())
            {
                orderStatusInsertDTO.NextOrderStatusId = null;
            }
            var newStatus = await AddSimple(orderStatusInsertDTO);
            statuses = await Get();
            if (statuses.Count() >= 2)
            {
                // Conecta cuando tiene un EstadoSiguienteID
                if (orderStatusInsertDTO.NextOrderStatusId != null)
                {
                    var selectedStatus = statuses.FirstOrDefault(e => e.Id == orderStatusInsertDTO.NextOrderStatusId);
                    if (selectedStatus != null)
                    {
                        var previousStatus = statuses.FirstOrDefault(e2 => e2.NextOrderStatusId == selectedStatus.Id);
                        if (previousStatus != null && previousStatus.Id != newStatus.Id)
                        {
                            var previousStatusDTO = OrderStatusMapper.GenerateOrderStatus(previousStatus.Id, previousStatus.Name, newStatus.Id);
                            await Update(previousStatusDTO);
                        }
                    }
                }
                // Conecta cuando el estado es el ultimo
                else
                {
                    var lastStatus = await GetLastOrderStatus();
                    if (lastStatus != null)
                    {
                        lastStatus.NextOrderStatusId = newStatus.Id;
                        var lastStatusDTO = OrderStatusMapper.GenerateOrderStatus(lastStatus.Id, lastStatus.Name, newStatus.Id);
                        await Update(lastStatusDTO);
                    }
                }
            }
            return newStatus;
        }

        public async Task<OrderStatusDTO?> Update(OrderStatusUpdateDTO orderStatusUpdateDTO)
        {
            var orderStatus = await _repository.GetById(orderStatusUpdateDTO.Id);
            if (orderStatus != null)
            {
                OrderStatusMapper.UpdateOrderStatus(orderStatus, orderStatusUpdateDTO);
                _repository.Update(orderStatus);
                await _repository.Save();

                return OrderStatusMapper.ConvertirModelToDTO(orderStatus);
            }
            return null;
        }

        public async Task<OrderStatusDTO?> Delete(int id)
        {
            var orderStatus = await _repository.GetById(id);
            if (orderStatus != null)
            {
                var estadoPedidoDTO = OrderStatusMapper.ConvertirModelToDTO(orderStatus);
                _repository.Delete(orderStatus);
                await _repository.Save();
                return estadoPedidoDTO;
            }
            return null;
        }

        public bool Validate(OrderStatusInsertDTO orderStatusDTO)
        {
            if (_repository.Search(e => 
                e.Name.ToUpper() == orderStatusDTO.Name.ToUpper())
                .Count() > 0)
            {
                Errors.Add("No puede existir un Estado con un nombre ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(OrderStatusUpdateDTO orderStatusDTO)
        {
            if (_repository.Search(e => 
                e.Name.ToUpper() == orderStatusDTO.Name.ToUpper()
                && orderStatusDTO.Id != e.OrderStatusID)
                .Count() > 0)
            {
                Errors.Add("No puede existir un Estado con un nombre ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
