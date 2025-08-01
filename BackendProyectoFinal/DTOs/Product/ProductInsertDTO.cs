﻿namespace BackendProyectoFinal.DTOs.Product
{
    public class ProductInsertDTO
    {
        public string MLCode { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public bool Eliminated { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
    }
}
