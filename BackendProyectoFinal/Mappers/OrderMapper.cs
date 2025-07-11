using BackendProyectoFinal.DTOs.OrderDTO;
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
                ListOrders = order.ListOrders,
                AddressId = order.AddressID
            };
            return orderDTO;
        }

        public static Order ConvertDTOToModel(OrderInsertDTO orderDTO)
        {
            var order = new Order()
            {
                UserID = orderDTO.UserId,
                ListOrders = orderDTO.ListOrders,
                AddressID = orderDTO.AddressId,
            };
            return order;
        }

        public static void UpdateOrder(Order order, OrderUpdateDTO orderDTO)
        {
            if (orderDTO.Id > 0)
                order.OrderID = orderDTO.Id;

            if (orderDTO.UserId > 0)
                order.UserID = orderDTO.UserId;

            if (orderDTO.ListOrders.Count() > 0)
            {
                order.ListOrders = orderDTO.ListOrders;
            }
            if (orderDTO.AddressId > 0)
            {
                order.AddressID = orderDTO.AddressId;
            }
        }
    }
}
