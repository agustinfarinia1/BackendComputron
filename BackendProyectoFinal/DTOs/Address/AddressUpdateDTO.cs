﻿namespace BackendProyectoFinal.DTOs.Address
{
    public class AddressUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public int? Floor { get; set; }
        public string? ApartmentNumber { get; set; }
    }
}
