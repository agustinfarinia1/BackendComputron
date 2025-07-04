using BackendProyectoFinal.DTOs;
using BackendProyectoFinal.Mappers;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace BackendProyectoFinal.Services
{
    public class MarcaService : ICommonService<MarcaDTO, MarcaInsertDTO, MarcaUpdateDTO>
    {
        private IRepository<Marca> _repository;
        public List<string> Errors { get; }
        public MarcaService(IRepository<Marca> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<MarcaDTO>> Get()
        {
            var marcas = await _repository.Get();
            // CONVIERTE LOS ROLES A DTO
            return marcas.Select(marca 
                => MarcaMapper.ConvertMarcaToDTO(marca)
            );
        }

        public async Task<MarcaDTO> GetById(int id)
        {
            var marca = await _repository.GetById(id);
            if (marca != null)
            {
                return MarcaMapper.ConvertMarcaToDTO(marca);
            }
            return null;
        }

        public async Task<MarcaDTO> GetByField(string field)
        {
            var marca = _repository.Search(r => r.Nombre.ToUpper() == field.ToUpper()).FirstOrDefault();
            if (marca != null)
            {
                return MarcaMapper.ConvertMarcaToDTO(marca);
            }
            return null;
        }

        public async Task<MarcaDTO> Add(MarcaInsertDTO marcaInsertDTO)
        {
            var marca = new Marca()
            {
                Nombre = marcaInsertDTO.Nombre
            };
            await _repository.Add(marca);
            await _repository.Save();

            return MarcaMapper.ConvertMarcaToDTO(marca);
        }

        public async Task<MarcaDTO> Update(MarcaUpdateDTO marcaUpdateDTO)
        {
            var marca = await _repository.GetById(marcaUpdateDTO.Id);
            if (marca != null)
            {
                marca.Nombre = marcaUpdateDTO.Nombre;

                _repository.Update(marca);
                await _repository.Save();

                return MarcaMapper.ConvertMarcaToDTO(marca);
            }
            return null;
        }

        public async Task<MarcaDTO> Delete(int id)
        {
            var marca = await _repository.GetById(id);
            if (marca != null)
            {
                var marcaDTO = MarcaMapper.ConvertMarcaToDTO(marca);

                _repository.Delete(marca);
                await _repository.Save();
                return marcaDTO;
            }
            return null;
        }

        public bool Validate(MarcaInsertDTO marcaDTO)
        {
            if (_repository.Search(m 
                    => m.Nombre.ToUpper() == marcaDTO.Nombre.ToUpper()).Count() > 0)
            {
                Errors.Add("No puede existir una marca con un nombre ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(MarcaUpdateDTO marcaDTO)
        {
            if (_repository.Search(m 
                    => m.Nombre.ToUpper() == marcaDTO.Nombre.ToUpper()
                    && marcaDTO.Id != m.MarcaID).Count() > 0)
            {
                Errors.Add("No puede existir una marca con un nombre ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
