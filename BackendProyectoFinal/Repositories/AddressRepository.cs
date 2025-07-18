using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class AddressRepository : IRepository<Address>
    {
        private StoreContext _context;
        public AddressRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Address>> Get()
            => await _context.Addresses.ToListAsync();

        public async Task<Address?> GetById(int id)
            => await _context.Addresses.FindAsync(id);

        // TODO - Esto no es del todo correcto porque Domicilio tendria que tener tambien numero para hacer una mejor busqueda
        // podria hacer que lo mande en un string separado por un espacio y subdividir el string(para que siga con el mismo cuerpo general)
        // O generar una interface especifica para Address
        public async Task<IEnumerable<Address>?> GetByField(string field)
        { 
            var search = Search((a => a.Name == field));
            if (search != null) {
                return search.ToList();
            }
            return null;
        }

        public async Task Add(Address address)
              => await _context.Addresses.AddAsync(address);

        public void Update(Address address)
        {
            _context.Addresses.Attach(address);
            _context.Addresses.Entry(address).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(Address address)
            => _context.Addresses.Remove(address);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Address> Search(Func<Address, bool> filter)
            => _context.Addresses.Where(filter).ToList();
    }
}
