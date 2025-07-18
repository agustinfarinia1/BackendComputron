using Microsoft.IdentityModel.Tokens;
using BackendProyectoFinal.DTOs.Order;
using BackendProyectoFinal.Mappers;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;

namespace BackendProyectoFinal.Services
{
    public class OrderService : IOrderService
    {
        private IOrderStatusService _orderStatusService;
        private IRepository<Order> _repository;
        public List<string> Errors { get; }
        public OrderService(
            [FromKeyedServices("OrderStatusService")] IOrderStatusService orderStatusService,
            IRepository<Order> repository)
        {
            _orderStatusService = orderStatusService;
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<OrderDTO>> Get()
        {
            var orders = await _repository.Get();
            // Convierte los Orders A DTO
            return orders.Select(o
                => OrderMapper.ConvertOrderToDTO(o));
        }

        public async Task<OrderDTO?> GetById(int id)
        {
            var order = await _repository.GetById(id);
            if (order != null)
            {
                return OrderMapper.ConvertOrderToDTO(order);
            }
            return null;
        }

        public async Task<OrderDTO?> GetByField(string field)
        {
            var order = _repository.Search(p => p.UserID == int.Parse(field)).FirstOrDefault();
            if (order != null)
            {
                return OrderMapper.ConvertOrderToDTO(order);
            }
            return null;
        }

        // Inicializacion del Order, todas las operaciones con la listOrder se hacen dentro de ItemOrder
        public async Task<OrderDTO> Add(OrderInsertDTO orderInsertDTO)
        {
            // Obtiene el estado inicial del OrderStatusService
            var orderStatus = _orderStatusService.GetFirst();
            orderInsertDTO.OrderStatusId = orderStatus.Id;
            var order = OrderMapper.ConvertDTOToModel(orderInsertDTO);
            await _repository.Add(order);
            await _repository.Save();

            return OrderMapper.ConvertOrderToDTO(order);
        }

        public async Task<OrderDTO?> Update(OrderUpdateDTO orderUpdateDTO)
        {
            var order = await _repository.GetById(orderUpdateDTO.Id);
            if (order != null)
            {
                OrderMapper.UpdateOrder(order, orderUpdateDTO);

                _repository.Update(order);
                await _repository.Save();

                return OrderMapper.ConvertOrderToDTO(order);
            }
            return null;
        }

        public void AddItemOrder(int orderID, int userID)
        {
            throw new NotImplementedException();
        }

        public void UpdateItemOrder(int orderID, int userID)
        {
            throw new NotImplementedException();
        }

        public void RemoveItemOrder(int orderID, int userID)
        {
            throw new NotImplementedException();
        }


        public async Task<OrderDTO?> Delete(int id)
        {
            var order = await _repository.GetById(id);
            if (order != null)
            {
                var orderDTO = OrderMapper.ConvertOrderToDTO(order);

                _repository.Delete(order);
                await _repository.Save();
                return orderDTO;
            }
            return null;
        }

        public bool Validate(OrderInsertDTO orderDTO)
        {
            if (_repository.Search(p
                => p.UserID == orderDTO.UserId)
                .Count() > 0)
            {
                Errors.Add("No puede existir un pedido con un usuario ya existente");
            }
            if (_repository.Search(p
                => p.ListOrders.Count() > 0)
                .Count() > 0)
            {
                Errors.Add("La lista de Pedidos tiene que contener productos");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(OrderUpdateDTO orderDTO)
        {
            if (_repository.Search(p
                => p.UserID == orderDTO.UserId
                && orderDTO.Id != p.OrderID)
                .Count() > 0)
            {
                Errors.Add("No puede existir un pedido con un usuario ya existente");
            }
            if (_repository.Search(p
                => p.ListOrders.Count() > 0
                && orderDTO.Id != p.OrderID)
                .Count() > 0)
            {
                Errors.Add("La lista de Pedidos tiene que contener productos");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
