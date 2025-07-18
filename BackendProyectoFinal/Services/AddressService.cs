using BackendProyectoFinal.Repositories;
using BackendProyectoFinal.Utils.Mappers;
using BackendProyectoFinal.DTOs.Address;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Services
{
    public class AddressService : ICommonService<AddressDTO, AddressInsertDTO, AddressUpdateDTO>
    {
        private IRepository<Address> _repository;
        public List<string> Errors { get; }
        public AddressService(IRepository<Address> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<AddressDTO>> Get() 
        {
            var adresses = await _repository.Get();
            // Convierte los Address A DTO
            return adresses.Select(address => 
            AddressMapper.ConvertAddressToDTO(address)
            );
        }

        public async Task<AddressDTO?> GetById(int id)
        {
            var address = await _repository.GetById(id);
            if (address != null)
            {
                return AddressMapper.ConvertAddressToDTO(address);
            }
            return null;
        }

        public async Task<AddressDTO?> GetByField(string field)
        {
            var address = _repository.Search(a => a.AddressID == int.Parse(field)).FirstOrDefault();
            if (address != null)
            {
                return AddressMapper.ConvertAddressToDTO(address);
            }
            return null;
        }

        public async Task<AddressDTO> Add(AddressInsertDTO addressInsertDTO)
        {
            var address = AddressMapper.ConvertDTOToAddress(addressInsertDTO);
            await _repository.Add(address);
            await _repository.Save();

            return AddressMapper.ConvertAddressToDTO(address) ;
        }

        public async Task<AddressDTO> Update(AddressUpdateDTO addressUpdateDTO)
        {
            var address = await _repository.GetById(addressUpdateDTO.Id);
            if (address != null)
            {
                AddressMapper.UpdateAddress(address,addressUpdateDTO);

                _repository.Update(address);
                await _repository.Save();

                return AddressMapper.ConvertAddressToDTO(address);
            }
            return null;
        }

        public async Task<AddressDTO> Delete(int id)
        {
            var address = await _repository.GetById(id);
            if (address != null)
            {
                var addressDTO = AddressMapper.ConvertAddressToDTO(address);

                _repository.Delete(address);
                await _repository.Save();
                return addressDTO;
            }
            return null;
        }

        // Con las validaciones de entrada, ya alcanzan
        // No tienen condicion de Unique
        public bool Validate(AddressInsertDTO addressDTO)
        {
            return true;
        }

        public bool Validate(AddressUpdateDTO addressDTO)
        {
            return true;
        }
    }
}
