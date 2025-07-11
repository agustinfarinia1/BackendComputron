using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> Get()
            => await _context.Products
                .Where(p => p.Eliminated == false).ToListAsync();

        public async Task<Product?> GetById(int id)
            => await _context.Products
            .FirstOrDefaultAsync(p => p.ProductID == id && p.Eliminated == false);

        // Busca por titulo, podria buscar por BrandID
        public async Task<Product?> GetByField(string field)
            => await _context.Products
                .FirstOrDefaultAsync(p => p.Title == field && p.Eliminated == false);

        public async Task Add(Product product)
              => await _context.Products.AddAsync(product);

        public void Update(Product product)
        {
            _context.Products.Attach(product);
            _context.Products.Entry(product).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(Product product)
            => Update(product);
        // Se utiliza el soft delete, asi que reutilizo el metodo Update

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Product> Search(Func<Product, bool> filter)
            => _context.Products.Where(filter).ToList();
    }
}
