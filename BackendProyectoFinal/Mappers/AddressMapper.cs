using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.AddressDTO;

namespace BackendProyectoFinal.Utils.Mappers
{
    public static class AddressMapper
    {
        public static Address ConvertDTOToAddress(AddressInsertDTO addressDTO)
        {
            var adrress = new Address()
            {
                Name = addressDTO.Name,
                Number = addressDTO.Number,
                Floor = addressDTO.Floor,
                ApartmentNumber = addressDTO.ApartmentNumber
            };
            return adrress;
        }
        
        public static AddressDTO ConvertAddressToDTO(Address address)
        {
            var addressDTO = new AddressDTO()
            {
                Id = address.AddressID,
                Name = address.Name,
                Number = address.Number,
                Floor = address.Floor,
                ApartmentNumber = address.ApartmentNumber
            };
            return addressDTO;
        }

        public static void UpdateAddress(Address address,AddressUpdateDTO adressDTO)
        {
            if (!string.IsNullOrWhiteSpace(adressDTO.Name))
                address.Name = adressDTO.Name;

            if (adressDTO.Number > 0)
                address.Number = adressDTO.Number;

            if (adressDTO.Floor.HasValue)
                address.Floor = adressDTO.Floor;

            if (!string.IsNullOrWhiteSpace(adressDTO.ApartmentNumber))
                address.ApartmentNumber = adressDTO.ApartmentNumber;
        }
    }
}
