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

        public async Task<Domicilio> GetById(int id)
            => await _context.Domicilios.FindAsync(id);

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
