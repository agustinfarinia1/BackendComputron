﻿using BackendProyectoFinal.DTOs.ItemCart;

namespace BackendProyectoFinal.DTOs.Cart
{
    public class CartDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<ItemCartDTO> ListCarts { get; set; }
    }
}
