using BackendProyectoFinal.DTOs.OrderStatusDTO;
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
                NextStatusOrderId = orderStatus.NextStatusOrderID
            };
            return orderStatusDTO;
        }

        public static OrderStatus ConvertirDTOToModel(OrderStatusInsertDTO orderStatusDTO)
        {
            var orderStatus = new OrderStatus()
            {
                Name = orderStatusDTO.Name
            };
            if (orderStatusDTO.NextStatusOrderId.HasValue)
                orderStatus.NextStatusOrderID = orderStatusDTO.NextStatusOrderId;

            return orderStatus;
        }

        public static void UpdateOrderStatus(OrderStatus orderStatus, OrderStatusUpdateDTO orderStatusDTO)
        {
            if (!string.IsNullOrWhiteSpace(orderStatusDTO.Name))
                orderStatus.Name = orderStatusDTO.Name;

            if (orderStatusDTO.NextStatusOrderId.HasValue)
                orderStatus.NextStatusOrderID = orderStatusDTO.NextStatusOrderId;
        }

        public static OrderStatusUpdateDTO GenerateOrderStatus(int id, string name,int? nextStatusOrderID)
        {
            var estadoPedidoUpdateDTO = new OrderStatusUpdateDTO()
            {
                Id = id,
                Name = name
            };
            if(nextStatusOrderID.HasValue) estadoPedidoUpdateDTO.NextStatusOrderId = nextStatusOrderID;
            return estadoPedidoUpdateDTO;
        }
    }
}
