﻿namespace BackendProyectoFinal.DTOs.ItemCart
{
    public class ItemCartUpdateDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
    }
}
