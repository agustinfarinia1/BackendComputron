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

        // Retorna todos los ItemCarts de un CartID
        public async Task<IEnumerable<ItemOrder>?> GetByField(string field)
        {
            var search = Search(i => i.OrderID == int.Parse(field));
            if (search != null)
            {
                return search.ToList();
            }
            return null;
        }

        public async Task Add(ItemOrder itemOrder)
              => await _context.ItemsOrders.AddAsync(itemOrder);

        public void Update(ItemOrder itemOrder)
        {
            _context.ItemsOrders.Attach(itemOrder);
            _context.ItemsOrders.Entry(itemOrder).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(ItemOrder itemOrder)
            => _context.ItemsOrders.Remove(itemOrder);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<ItemOrder> Search(Func<ItemOrder, bool> filter)
            => _context.ItemsOrders.Where(filter).ToList();
    }
}
