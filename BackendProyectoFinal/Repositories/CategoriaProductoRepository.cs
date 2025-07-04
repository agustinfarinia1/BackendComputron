using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class CategoriaProductoRepository : IRepository<CategoriaProducto>
    {
        private StoreContext _context;
        public CategoriaProductoRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CategoriaProducto>> Get()
            => await _context.CategoriaProductos.ToListAsync();

        public async Task<CategoriaProducto?> GetById(int id)
            => await _context.CategoriaProductos.FindAsync(id);

        public async Task<CategoriaProducto?> GetByField(string field)
            => await _context.CategoriaProductos
                .FirstOrDefaultAsync(c => c.Nombre == field);

        public async Task Add(CategoriaProducto categoria)
              => await _context.CategoriaProductos.AddAsync(categoria);

        public void Update(CategoriaProducto categoria)
        {
            _context.CategoriaProductos.Attach(categoria);
            _context.CategoriaProductos.Entry(categoria).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(CategoriaProducto categoria)
            => _context.CategoriaProductos.Remove(categoria);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<CategoriaProducto> Search(Func<CategoriaProducto, bool> filter)
            => _context.CategoriaProductos.Where(filter).ToList();
    }
}
