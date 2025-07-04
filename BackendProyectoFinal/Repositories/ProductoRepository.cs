using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class ProductoRepository : IRepository<Producto>
    {
        private StoreContext _context;
        public ProductoRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Producto>> Get()
            => await _context.Productos
                .Where(p => p.Eliminado == false).ToListAsync();

        public async Task<Producto?> GetById(int id)
            => await _context.Productos
            .FirstOrDefaultAsync(p => p.ProductoID == id && p.Eliminado == false);

        // Busca por titulo, podria buscar por CategoriaID
        public async Task<Producto?> GetByField(string field)
            => await _context.Productos
                .FirstOrDefaultAsync(p => p.Titulo == field && p.Eliminado == false);

        public async Task Add(Producto producto)
              => await _context.Productos.AddAsync(producto);

        public void Update(Producto producto)
        {
            _context.Productos.Attach(producto);
            _context.Productos.Entry(producto).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(Producto producto)
            => Update(producto);
        // Se utiliza el soft delete, asi que reutilizo el metodo Update

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Producto> Search(Func<Producto, bool> filter)
            => _context.Productos.Where(filter).ToList();
    }
}
