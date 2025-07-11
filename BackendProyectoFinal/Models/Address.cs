using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProyectoFinal.Models
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressID {  get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public int? Floor {  get; set; }
        public string? ApartmentNumber { get; set; }
    }
}
