﻿namespace BackendProyectoFinal.DTOs
{
    public class FacturaDTO
    {
        public int FacturaID {  get; set; }
        public DateTime Fecha {  get; set; }
        public int TipoFactura {  get; set; }
        public string RazonSocial {  get; set; }
        public decimal importe {  get; set; }
    }
}
