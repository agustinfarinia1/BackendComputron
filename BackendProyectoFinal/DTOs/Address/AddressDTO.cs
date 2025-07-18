namespace BackendProyectoFinal.DTOs.Address
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public int? Floor { get; set; }
        public string? ApartmentNumber { get; set; }
    }
}
