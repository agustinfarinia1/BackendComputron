namespace BackendProyectoFinal.DTOs
{
    public class ProductoInsertDTO
    {
        public string CodigoML { get; set; }
        public string Titulo {  get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string Imagen { get; set; }
        public int CategoriaProductoID { get; set; }
        public bool Eliminado { get; set; }
    }
}
