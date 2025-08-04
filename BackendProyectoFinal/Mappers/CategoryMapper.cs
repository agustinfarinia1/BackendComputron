using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.Product.Category;

namespace BackendProyectoFinal.Utils.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDTO ConvertCategoryToDTO(Category category)
        {
            var categoryDTO = new CategoryDTO()
            {
                Id = category.CategoryID,
                Name = category.Name
            };
            return categoryDTO;
        }
    }
}
