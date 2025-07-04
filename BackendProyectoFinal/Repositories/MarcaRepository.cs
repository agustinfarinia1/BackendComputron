using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class MarcaRepository : IRepository<Marca>
    {
        private StoreContext _context;
        public MarcaRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Marca>> Get()
            => await _context.Marcas.ToListAsync();

        public async Task<Marca?> GetById(int id)
            => await _context.Marcas.FindAsync(id);

        public async Task<Marca?> GetByField(string field)
            => await _context.Marcas
                .FirstOrDefaultAsync(m => m.Nombre == field);

        public async Task Add(Marca marca)
              => await _context.Marcas.AddAsync(marca);

        public void Update(Marca marca)
        {
            _context.Marcas.Attach(marca);
            _context.Marcas.Entry(marca).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(Marca marca)
            => _context.Marcas.Remove(marca);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Marca> Search(Func<Marca, bool> filter)
            => _context.Marcas.Where(filter).ToList();
    }
}
