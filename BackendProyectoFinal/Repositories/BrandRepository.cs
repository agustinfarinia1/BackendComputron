using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class BrandRepository : IRepository<Brand>
    {
        private StoreContext _context;
        public BrandRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Brand>> Get()
            => await _context.Brands.ToListAsync();

        public async Task<Brand?> GetById(int id)
            => await _context.Brands.FindAsync(id);

        public async Task<Brand?> GetByField(string field)
            => await _context.Brands
                .FirstOrDefaultAsync(m => m.Name == field);

        public async Task Add(Brand brand)
              => await _context.Brands.AddAsync(brand);

        public void Update(Brand brand)
        {
            _context.Brands.Attach(brand);
            _context.Brands.Entry(brand).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(Brand brand)
            => _context.Brands.Remove(brand);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Brand> Search(Func<Brand, bool> filter)
            => _context.Brands.Where(filter).ToList();
    }
}
