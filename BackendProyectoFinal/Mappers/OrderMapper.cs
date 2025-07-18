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
                UserId = order.UserID,
                AddressId = order.AddressID,
                OrderStatusId = order.OrderStatusID
            };
            return orderDTO;
        }

        public static Order ConvertDTOToModel(OrderInsertDTO orderDTO)
        {
            var order = new Order()
            {
                UserID = orderDTO.UserId,
                ListOrders = new List<ItemOrder>(),
                AddressID = orderDTO.AddressId,
                OrderID = orderDTO.OrderStatusId
            };
            return order;
        }

        public static void UpdateOrder(Order order, OrderUpdateDTO orderDTO)
        {
            if (orderDTO.Id > 0)
                order.OrderID = orderDTO.Id;

            if (orderDTO.UserId > 0)
                order.UserID = orderDTO.UserId;
            
            if (orderDTO.AddressId > 0)
                order.AddressID = orderDTO.AddressId;
            
            if (orderDTO.OrderStatusId > 0)
                order.OrderID = orderDTO.OrderStatusId;
        }
    }
}
