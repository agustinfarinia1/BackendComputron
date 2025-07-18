using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private StoreContext _context;
        public CategoryRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> Get()
            => await _context.Categories.ToListAsync();

        public async Task<Category?> GetById(int id)
            => await _context.Categories.FindAsync(id);

        public async Task<IEnumerable<Category>?> GetByField(string field)
        {
            var search = Search(c => c.Name == field);
            if (search != null)
            {
                return search.ToList();
            }
            return null;
        }

        public async Task Add(Category category)
              => await _context.Categories.AddAsync(category);

        public void Update(Category category)
        {
            _context.Categories.Attach(category);
            _context.Categories.Entry(category).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(Category category)
            => _context.Categories.Remove(category);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Category> Search(Func<Category, bool> filter)
            => _context.Categories.Where(filter).ToList();
    }
}
