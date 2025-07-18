using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class ItemOrderRepository : IRepository<ItemOrder>
    {
        private StoreContext _context;
        public ItemOrderRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ItemOrder>> Get()
            => await _context.ItemsOrders.ToListAsync();

        public async Task<ItemOrder?> GetById(int id)
            => await _context.ItemsOrders.FindAsync(id);

        public async Task<IEnumerable<ItemOrder>?> GetByField(string field)
        {
            var search = Search(i => i.OrderID == int.Parse(field));
            if (search != null)
            {
                return search.ToList();
            }
            return null;
        }

        public async Task Add(ItemOrder role)
              => await _context.ItemsOrders.AddAsync(role);

        public void Update(ItemOrder role)
        {
            _context.ItemsOrders.Attach(role);
            _context.ItemsOrders.Entry(role).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(ItemOrder role)
            => _context.ItemsOrders.Remove(role);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<ItemOrder> Search(Func<ItemOrder, bool> filter)
            => _context.ItemsOrders.Where(filter).ToList();
    }
}
