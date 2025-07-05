namespace BackendProyectoFinal.DTOs.ProductoDTO
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string CodigoML { get; set; }
        public string Titulo { get; set; }
        public int MarcaId { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string Imagen { get; set; }
        public DateOnly FechaCreacion { get; set; }
        public int CategoriaProductoID { get; set; }
        public bool Eliminado { get; set; }
    }
}
