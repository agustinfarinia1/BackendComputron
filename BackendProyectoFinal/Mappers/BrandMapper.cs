using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.Product.Brand;

namespace BackendProyectoFinal.Mappers
{
    public static class BrandMapper
    {
        public static BrandDTO ConvertBrandToDTO(Brand brand)
        {
            var brandDTO = new BrandDTO()
            {
                Id = brand.BrandID,
                Name = brand.Name
            };
            return brandDTO;
        }
    }
}
