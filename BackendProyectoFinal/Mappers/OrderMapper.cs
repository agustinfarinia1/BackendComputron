using BackendProyectoFinal.DTOs.Order;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Mappers
{
    public static class OrderMapper
    {
        public static OrderDTO ConvertOrderToDTO(Order order)
        {
            var orderDTO = new OrderDTO()
            {
                Id = order.OrderID,
                CreationDate = order.CreationDate,
                UserId = order.UserID,
                AddressId = order.AddressID,
                OrderStatusId = order.OrderStatusID,
                Canceled = order.Canceled
            };
            return orderDTO;
        }

        public static Order ConvertDTOToModel(OrderInsertDTO orderDTO)
        {
            var order = new Order()
            {
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                UserID = orderDTO.UserId,
                ListOrders = new List<ItemOrder>(),
                AddressID = orderDTO.AddressId,
                OrderStatusID = orderDTO.OrderStatusId,
                Canceled = false
            };
            return order;
        }

        public static void UpdateOrder(OrderDTO orderDTO, OrderUpdateDTO orderUpdateDTO)
        {
            if (orderUpdateDTO.OrderStatusId > 0)
                orderDTO.OrderStatusId = orderUpdateDTO.OrderStatusId;

            if (orderUpdateDTO.Canceled != orderDTO.Canceled)
                orderDTO.Canceled = orderUpdateDTO.Canceled;
        }
    }
}
