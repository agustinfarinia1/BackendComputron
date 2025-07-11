using BackendProyectoFinal.Repositories;
using Microsoft.IdentityModel.Tokens;
using BackendProyectoFinal.Mappers;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.OrderStatusDTO;

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
            // CONVIERTE LOS Estados A DTO
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

        public async Task<OrderStatusDTO?> GetFirst()
        {
            var statuses = await Get();
            var lastStatus = await GetLast();
            var firstStatus = lastStatus;
            if (statuses.Any() && lastStatus != null)
            {
                var search = false;
                for(int i = 0; i> statuses.Count(); i++)
                {
                    var previousStatus = statuses.FirstOrDefault(e => e.NextStatusOrderId == lastStatus.Id);
                    if(previousStatus == null)
                    {
                        firstStatus = lastStatus;
                        search = true;
                        break;
                    }
                    lastStatus = previousStatus;
                }
                if (search)
                {
                    return firstStatus;
                }
            }
            return null;
        }

        public async Task<OrderStatusDTO?> GetLast()
        {
            var statuses = await Get();
            if (statuses.Any())
            {
                var lastStatus = statuses.FirstOrDefault(e => e.NextStatusOrderId == null);
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
                orderStatusInsertDTO.NextStatusOrderId = null;
            }
            var newStatus = await AddSimple(orderStatusInsertDTO);
            statuses = await Get();
            if (statuses.Count() >= 2)
            {
                // Conecta cuando tiene un EstadoSiguienteID
                if (orderStatusInsertDTO.NextStatusOrderId != null)
                {
                    var selectedStatus = statuses.FirstOrDefault(e => e.Id == orderStatusInsertDTO.NextStatusOrderId);
                    if (selectedStatus != null)
                    {
                        var previousStatus = statuses.FirstOrDefault(e2 => e2.NextStatusOrderId == selectedStatus.Id);
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
                    var lastStatus = await GetLast();
                    if (lastStatus != null)
                    {
                        lastStatus.NextStatusOrderId = newStatus.Id;
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
