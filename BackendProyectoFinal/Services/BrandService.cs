using BackendProyectoFinal.Repositories;
using Microsoft.IdentityModel.Tokens;
using BackendProyectoFinal.Mappers;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.Product.Brand;

namespace BackendProyectoFinal.Services
{
    public class BrandService : ICommonService<BrandDTO, BrandInsertDTO, BrandUpdateDTO>
    {
        private IRepository<Brand> _repository;
        public List<string> Errors { get; }
        public BrandService(IRepository<Brand> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<BrandDTO>> Get()
        {
            var marcas = await _repository.Get();
            // Convierte las Brands A DTO
            return marcas.Select(brand 
                => BrandMapper.ConvertBrandToDTO(brand)
            );
        }

        public async Task<BrandDTO?> GetById(int id)
        {
            var brand = await _repository.GetById(id);
            if (brand != null)
            {
                return BrandMapper.ConvertBrandToDTO(brand);
            }
            return null;
        }

        public async Task<BrandDTO?> GetByField(string field)
        {
            var brand = _repository.Search(r => r.Name.ToUpper() == field.ToUpper()).FirstOrDefault();
            if (brand != null)
            {
                return BrandMapper.ConvertBrandToDTO(brand);
            }
            return null;
        }

        public async Task<BrandDTO> Add(BrandInsertDTO brandInsertDTO)
        {
            var brand = new Brand()
            {
                Name = brandInsertDTO.Name
            };
            await _repository.Add(brand);
            await _repository.Save();

            return BrandMapper.ConvertBrandToDTO(brand);
        }

        public async Task<BrandDTO?> Update(BrandUpdateDTO brandUpdateDTO)
        {
            var brand = await _repository.GetById(brandUpdateDTO.Id);
            if (brand != null)
            {
                brand.Name = brandUpdateDTO.Name;

                _repository.Update(brand);
                await _repository.Save();

                return BrandMapper.ConvertBrandToDTO(brand);
            }
            return null;
        }

        public async Task<BrandDTO?> Delete(int id)
        {
            var brand = await _repository.GetById(id);
            if (brand != null)
            {
                var brandDTO = BrandMapper.ConvertBrandToDTO(brand);

                _repository.Delete(brand);
                await _repository.Save();
                return brandDTO;
            }
            return null;
        }

        public bool Validate(BrandInsertDTO brandDTO)
        {
            if (_repository.Search(b => 
                b.Name.ToUpper() == brandDTO.Name.ToUpper()).Count() > 0)
            {
                Errors.Add("No puede existir una marca con un nombre ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(BrandUpdateDTO brandDTO)
        {
            if (_repository.Search(b => 
                b.Name.ToUpper() == brandDTO.Name.ToUpper()
                    && brandDTO.Id != b.BrandID).Count() > 0)
            {
                Errors.Add("No puede existir una marca con un nombre ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
