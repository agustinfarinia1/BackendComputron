using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.Product;

namespace BackendProyectoFinal.Mappers
{
    public static class ProductMapper
    {
        public static Product ConvertDTOToProduct(ProductInsertDTO productDTO)
        {
            // Al crearse un Product, no puede estar eliminado
            var product = new Product()
            {
                Title = productDTO.Title,
                MLCode = productDTO.MLCode,
                BrandID = productDTO.BrandId,
                Price = productDTO.Price,
                Quantity = productDTO.Quantity,
                Image = productDTO.Image,
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                CategoryID = productDTO.CategoryId,
                Eliminated = false
            };
            return product;
        }

        public static ProductDTO ConvertProductToDTO(Product producto)
        {
            var productoDTO = new ProductDTO()
            {
                Id = producto.ProductID,
                Title = producto.Title,
                MLCode = producto.MLCode,
                BrandId = producto.BrandID,
                Price = producto.Price,
                Quantity = producto.Quantity,
                Image = producto.Image,
                CreationDate = producto.CreationDate,
                CategoryId = producto.CategoryID,
                Eliminated = producto.Eliminated
            };
            return productoDTO;
        }

        public static void UpdateProduct(Product product, ProductUpdateDTO productDTO)
        {
            if (!string.IsNullOrWhiteSpace(productDTO.Title))
                product.Title = productDTO.Title;

            if (!string.IsNullOrWhiteSpace(productDTO.MLCode))
                product.MLCode = productDTO.MLCode;
            
            if (productDTO.BrandId > 0)
                product.BrandID = productDTO.BrandId;

            if (productDTO.Price > 0)
                product.Price = productDTO.Price;

            if (productDTO.Quantity > 0)
                product.Quantity = productDTO.Quantity;

            if (!string.IsNullOrWhiteSpace(productDTO.Image))
                product.Image = productDTO.Image;

            if (productDTO.CreationDate > DateOnly.MinValue)
                product.CreationDate = productDTO.CreationDate;
            
            if (productDTO.CategoryId > 0)
                product.CategoryID = productDTO.CategoryId;

            if (productDTO.Eliminated.HasValue) {
                product.Eliminated = (bool)productDTO.Eliminated;
            }
        }
    }
}
