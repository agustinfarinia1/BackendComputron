using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.ItemOrder;

namespace BackendProyectoFinal.Mappers
{
    public static class ItemOrderMapper
    {
        public static ItemOrderDTO ConvertItemOrderToDTO(ItemOrder itemOrder)
        {
            var itemOrderDTO = new ItemOrderDTO()
            {
                Id = itemOrder.ItemOrderID,
                Quantity = itemOrder.Quantity,
                OrderId = itemOrder.OrderID,
                ProductId = itemOrder.ProductID
            };
            return itemOrderDTO;
        }

        public static ItemOrder ConvertDTOToModel(ItemOrderInsertDTO itemOrderDTO)
        {
            var itemOrder = new ItemOrder()
            {
                OrderID = itemOrderDTO.OrderId,
                ProductID = itemOrderDTO.ProductId,
                Quantity = itemOrderDTO.Quantity
            };
            return itemOrder;
        }

        public static void UpdateItemOrder(ItemOrder itemOrder, ItemOrderUpdateDTO itemOrderDTO)
        {
            if (itemOrderDTO.ProductId > 0)
                itemOrder.ProductID = itemOrderDTO.ProductId;

            if (itemOrderDTO.OrderId > 0)
            {
                itemOrder.OrderID = itemOrderDTO.OrderId;
            }

            if (itemOrderDTO.Quantity > 0)
            {
                itemOrder.Quantity = itemOrderDTO.Quantity;
            }
        }
    }
}
