using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class ItemCartRepository : IRepository<ItemCart>
    {
        private StoreContext _context;
        public ItemCartRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ItemCart>> Get()
            => await _context.ItemsCarts.ToListAsync();

        public async Task<ItemCart?> GetById(int id)
            => await _context.ItemsCarts.FindAsync(id);

        // Retorna todos los ItemCarts de un CartID
        public async Task<IEnumerable<ItemCart>?> GetByField(string field)
        {
            var search = Search(i => i.CartID == int.Parse(field));
            if (search != null)
            {
                return search.ToList();
            }
            return null;
        }

        public async Task Add(ItemCart role)
              => await _context.ItemsCarts.AddAsync(role);

        public void Update(ItemCart role)
        {
            _context.ItemsCarts.Attach(role);
            _context.ItemsCarts.Entry(role).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(ItemCart role)
            => _context.ItemsCarts.Remove(role);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<ItemCart> Search(Func<ItemCart, bool> filter)
            => _context.ItemsCarts.Where(filter).ToList();
    }
}
