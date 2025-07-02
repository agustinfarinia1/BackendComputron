using BackendProyectoFinal.DTOs;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;
using BackendProyectoFinal.Utils.Mappers;

namespace BackendProyectoFinal.Services
{
    public class DomicilioService : ICommonService<DomicilioDTO, DomicilioInsertDTO, DomicilioUpdateDTO>
    {
        private IRepository<Domicilio> _repository;
        public List<string> Errors { get; }
        public DomicilioService(IRepository<Domicilio> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<DomicilioDTO>> Get() 
        {
            var roles = await _repository.Get();
            // CONVIERTE LOS ROLES A DTO
            return roles.Select(domicilio => 
            DomicilioMapper.ConvertDomicilioToDTO(domicilio)
            );
        }

        public async Task<DomicilioDTO> GetById(int id)
        {
            var domicilio = await _repository.GetById(id);
            if (domicilio != null)
            {
                return DomicilioMapper.ConvertDomicilioToDTO(domicilio);
            }
            return null;
        }

        public async Task<DomicilioDTO> GetByField(string field)
        {
            var domicilio = _repository.Search(d => d.DomicilioID == int.Parse(field)).FirstOrDefault();
            if (domicilio != null)
            {
                return DomicilioMapper.ConvertDomicilioToDTO(domicilio);
            }
            return null;
        }

        public async Task<DomicilioDTO> Add(DomicilioInsertDTO domicilioInsertDTO)
        {
            var domicilio = DomicilioMapper.ConvertDTOToDomicilio(domicilioInsertDTO);
            await _repository.Add(domicilio);
            await _repository.Save();

            return DomicilioMapper.ConvertDomicilioToDTO(domicilio) ;
        }

        public async Task<DomicilioDTO> Update(DomicilioUpdateDTO domicilioUpdateDTO)
        {
            var domicilio = await _repository.GetById(domicilioUpdateDTO.Id);
            if (domicilio != null)
            {
                domicilio.Nombre = domicilioUpdateDTO.Nombre;
                domicilio.Numero = domicilioUpdateDTO.Numero;
                domicilio.Piso = domicilioUpdateDTO.Piso;
                domicilio.Departamento = domicilioUpdateDTO.Departamento;

                _repository.Update(domicilio);
                await _repository.Save();

                return DomicilioMapper.ConvertDomicilioToDTO(domicilio);
            }
            return null;
        }

        public async Task<DomicilioDTO> Delete(int id)
        {
            var domicilio = await _repository.GetById(id);
            if (domicilio != null)
            {
                var domicilioDTO = DomicilioMapper.ConvertDomicilioToDTO(domicilio);

                _repository.Delete(domicilio);
                await _repository.Save();
                return domicilioDTO;
            }
            return null;
        }

        // Con las validaciones de entrada, ya alcanzan
        // No tienen condicion de Unique
        public bool Validate(DomicilioInsertDTO domicilioDTO)
        {
            return true;
        }

        public bool Validate(DomicilioUpdateDTO domicilioDTO)
        {
            return true;
        }
    }
}
