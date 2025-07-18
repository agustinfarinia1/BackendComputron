using BackendProyectoFinal.DTOs.OrderStatus;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Mappers
{
    public static class OrderStatusMapper
    {
        public static OrderStatusDTO ConvertirModelToDTO(OrderStatus orderStatus)
        {
            var orderStatusDTO = new OrderStatusDTO()
            {
                Id = orderStatus.OrderStatusID,
                Name = orderStatus.Name,
                NextOrderStatusId = orderStatus.NextOrderStatusID
            };
            return orderStatusDTO;
        }

        public static OrderStatus ConvertirDTOToModel(OrderStatusInsertDTO orderStatusDTO)
        {
            var orderStatus = new OrderStatus()
            {
                Name = orderStatusDTO.Name
            };
            if (orderStatusDTO.NextOrderStatusId.HasValue)
                orderStatus.NextOrderStatusID = orderStatusDTO.NextOrderStatusId;

            return orderStatus;
        }

        public static void UpdateOrderStatus(OrderStatus orderStatus, OrderStatusUpdateDTO orderStatusDTO)
        {
            if (!string.IsNullOrWhiteSpace(orderStatusDTO.Name))
                orderStatus.Name = orderStatusDTO.Name;

            if (orderStatusDTO.NextOrderStatusId.HasValue)
                orderStatus.NextOrderStatusID = orderStatusDTO.NextOrderStatusId;
        }

        public static OrderStatusUpdateDTO GenerateOrderStatus(int id, string name,int? nextOrderStatusID)
        {
            var estadoPedidoUpdateDTO = new OrderStatusUpdateDTO()
            {
                Id = id,
                Name = name
            };
            if(nextOrderStatusID.HasValue) estadoPedidoUpdateDTO.NextOrderStatusId = nextOrderStatusID;
            return estadoPedidoUpdateDTO;
        }
    }
}
