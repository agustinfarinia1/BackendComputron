using BackendProyectoFinal.Repositories;
using BackendProyectoFinal.Utils.Mappers;
using Microsoft.IdentityModel.Tokens;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.Product.Category;

namespace BackendProyectoFinal.Services
{
    public class CategoryService : ICommonService<CategoryDTO, CategoryInsertDTO, CategoryUpdateDTO>
    {
        private IRepository<Category> _repository;
        public List<string> Errors { get; }
        public CategoryService(
            IRepository<Category> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<CategoryDTO>> Get() 
        {
            var categorias = await _repository.Get();
            // CONVIERTE Las Categories A DTO
            return categorias.Select(c => 
            CategoryMapper.ConvertCategoryToDTO(c)
            );
        }
        
        public async Task<CategoryDTO?> GetById(int id)
        {
            var category = await _repository.GetById(id);
            if (category != null)
            {
                return CategoryMapper.ConvertCategoryToDTO(category);
            }
            return null;
        }

        public async Task<CategoryDTO?> GetByField(string field)
        {
            var category = _repository.Search(u => 
                u.Name == field).FirstOrDefault(); ;
            if (category != null)
            {
                return CategoryMapper.ConvertCategoryToDTO(category);
            }
            return null;
        }

        public async Task<CategoryDTO> Add(CategoryInsertDTO categoryInsertDTO)
        {
            var category = new Category()
            {
                Name = categoryInsertDTO.Name
            };
            await _repository.Add(category);
            await _repository.Save();

            return CategoryMapper.ConvertCategoryToDTO(category) ;
        }

        public async Task<CategoryDTO?> Update(CategoryUpdateDTO categoryUpdateDTO)
        {
            var category = await _repository.GetById(categoryUpdateDTO.Id);
            if (category != null)
            {
                category.Name = categoryUpdateDTO.Name;

                _repository.Update(category);
                await _repository.Save();

                return CategoryMapper.ConvertCategoryToDTO(category);
            }
            return null;
        }

        public async Task<CategoryDTO?> Delete(int id)
        {
            var category = await _repository.GetById(id);
            if (category != null)
            {
                var categoryDTO =  CategoryMapper.ConvertCategoryToDTO(category);

                _repository.Delete(category);
                await _repository.Save();
                return categoryDTO;
            }
            return null;
        }

        public bool Validate(CategoryInsertDTO categoryDTO)
        {
            if (_repository.Search(c => c.Name.ToUpper() == categoryDTO.Name.ToUpper()).Count() > 0)
            {
                Errors.Add("No puede existir una categoria con un nombre ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(CategoryUpdateDTO categoryDTO)
        {
            if (_repository.Search(
                c => c.Name.ToUpper() == categoryDTO.Name.ToUpper()
                && categoryDTO.Id != c.CategoryID).Count() > 0)
            {
                Errors.Add("No puede existir una categoria con un nombre ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
