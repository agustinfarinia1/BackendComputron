using BackendProyectoFinal.Mappers;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;
using Microsoft.IdentityModel.Tokens;
using BackendProyectoFinal.DTOs.Order;
using BackendProyectoFinal.DTOs.Order.ItemOrder;

namespace BackendProyectoFinal.Services
{
    public class OrderService : IListService<OrderDTO, OrderInsertDTO, OrderUpdateDTO>
    {
        private IItemListService<ItemOrderDTO, ItemOrderInsertDTO, ItemOrderUpdateDTO> _itemOrderService;
        private IOrderStatusService _orderStatusService;
        private IRepository<Order> _repository;
        public List<string> Errors { get; }
        public OrderService(
            [FromKeyedServices("OrderStatusService")] IOrderStatusService orderStatusService,
            [FromKeyedServices("ItemOrderService")] IItemListService<ItemOrderDTO, ItemOrderInsertDTO, ItemOrderUpdateDTO> itemOrderService,
            IRepository<Order> repository)
        {
            _itemOrderService = itemOrderService;
            _orderStatusService = orderStatusService;
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<OrderDTO>> Get()
        {
            var orders = await _repository.Get();
            // Convierte los Carts A DTO
            var ordersDTO = orders.Select(c
                => OrderMapper.ConvertOrderToDTO(c)).ToList();
            foreach (var orderDTO in ordersDTO)
            {
                // Lista de ItemCart ligada a Cart
                var listOrderDTO = await _itemOrderService.GetItemByListId(orderDTO.Id);
                if (listOrderDTO != null && listOrderDTO.Count() > 0)
                {
                    orderDTO.ListOrders = listOrderDTO.ToList();
                }
            }
            return ordersDTO;
        }

        public async Task<OrderDTO?> GetById(int orderId)
        {
            var order = await _repository.GetById(orderId);
            if (order != null)
            {
                var orderDTO = OrderMapper.ConvertOrderToDTO(order);
                var listOrderDTO = await _itemOrderService.GetItemByListId(orderDTO.Id);
                if (listOrderDTO != null)
                    orderDTO.ListOrders = listOrderDTO.ToList();
                return orderDTO;
            }
            return null;
        }

        // TODO: Esto es una lista de Orders, porque puede existir mas de Order por User
        // Se podria cambiar el ICommonService general y hacer que este metodo devuelva una Lista
        public async Task<OrderDTO?> GetByField(string field)
        {
            var order = _repository.Search(o => o.UserID == int.Parse(field)).FirstOrDefault();
            if (order != null)
            {
                var orderDTO = OrderMapper.ConvertOrderToDTO(order);

                var listOrderDTO = await _itemOrderService.GetItemByListId(orderDTO.Id);
                if (listOrderDTO != null)
                    orderDTO.ListOrders = listOrderDTO.ToList();
                return orderDTO;
            }
            return null;
        }

        // Inicializacion del Order, todas las operaciones con la listOrder se hacen dentro de ItemOrder
        public async Task<OrderDTO> Add(OrderInsertDTO orderInsertDTO)
        {
            // Obtiene el estado inicial y el CreationDate
            var orderStatus = await _orderStatusService.GetFirstOrderStatus();
            if(orderStatus != null)
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
                var orderStatus = await _orderStatusService.GetById(order.OrderStatusID);
                if (orderStatus != null && orderStatus.NextOrderStatusId.HasValue)
                {
                    order.OrderStatusID = orderStatus.NextOrderStatusId.Value;
                    _repository.Update(order);
                    await _repository.Save();   
                    return OrderMapper.ConvertOrderToDTO(order);
                }
            }
            return null;
        }

        // Modifica el campo Canceled y cuenta como su "eliminacion"
        public async Task<OrderDTO?> Delete(int id)
        {
            var order = await _repository.GetById(id);
            if (order != null)
            {
                order.Canceled = true;
                var orderDTO = OrderMapper.ConvertOrderToDTO(order);

                _repository.Update(order);
                await _repository.Save();
                return orderDTO;
            }
            return null;
        }

        public bool Validate(OrderInsertDTO orderDTO)
        {
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
                => p.ListOrders.Count() > 0
                && orderDTO.Id != p.OrderID)
                .Count() > 0)
            {
                Errors.Add("La lista de Pedidos tiene que contener productos");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public void EmptyList(int listID, int userID)
        {
            throw new NotImplementedException();
        }
    }
}
