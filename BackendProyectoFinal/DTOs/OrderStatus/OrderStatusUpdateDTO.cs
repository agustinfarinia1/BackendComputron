﻿namespace BackendProyectoFinal.DTOs.OrderStatus
{
    public class OrderStatusUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? NextOrderStatusId { get; set; }
    }
}
