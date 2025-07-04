using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class DomicilioRepository : IRepository<Domicilio>
    {
        private StoreContext _context;
        public DomicilioRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Domicilio>> Get()
            => await _context.Domicilios.ToListAsync();

        public async Task<Domicilio?> GetById(int id)
            => await _context.Domicilios.FindAsync(id);

        // TODO - Esto no es del todo correcto porque Domicilio tendria que tener tambien numero para hacer una mejor busqueda
        // podria hacer que lo mande en un string separado por un espacio y subdividir el string(para que siga con el mismo cuerpo general)
        public async Task<Domicilio?> GetByField(string field)
            => await _context.Domicilios
                .FirstOrDefaultAsync(d => d.Nombre == field);

        public async Task Add(Domicilio domicilio)
              => await _context.Domicilios.AddAsync(domicilio);

        public void Update(Domicilio domicilio)
        {
            _context.Domicilios.Attach(domicilio);
            _context.Domicilios.Entry(domicilio).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(Domicilio domicilio)
            => _context.Domicilios.Remove(domicilio);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Domicilio> Search(Func<Domicilio, bool> filter)
            => _context.Domicilios.Where(filter).ToList();
    }
}
