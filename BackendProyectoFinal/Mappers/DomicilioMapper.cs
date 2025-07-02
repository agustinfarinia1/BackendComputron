using BackendProyectoFinal.DTOs;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Utils.Mappers
{
    public static class DomicilioMapper
    {
        public static Domicilio ConvertDTOToDomicilio(DomicilioInsertDTO domicilioDTO)
        {
            var domicilio = new Domicilio()
            {
                Nombre = domicilioDTO.Nombre,
                Numero = domicilioDTO.Numero,
                Piso = domicilioDTO.Piso,
                Departamento = domicilioDTO.Departamento
            };
            return domicilio;
        }
        
        public static DomicilioDTO ConvertDomicilioToDTO(Domicilio domicilio)
        {
            var domicilioDTO = new DomicilioDTO()
            {
                Id = domicilio.DomicilioID,
                Nombre = domicilio.Nombre,
                Numero = domicilio.Numero,
                Piso = domicilio.Piso,
                Departamento   = domicilio.Departamento
            };
            return domicilioDTO;
        }
    }
}
