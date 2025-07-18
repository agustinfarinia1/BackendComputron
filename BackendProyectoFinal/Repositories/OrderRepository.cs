using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private StoreContext _context;
        public OrderRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> Get()
            => await _context.Orders.ToListAsync();

        public async Task<Order?> GetById(int id)
            => await _context.Orders.FindAsync(id);

        public async Task<IEnumerable<Order>?> GetByField(string field)
        {
            var search = Search(o => o.UserID == int.Parse(field));
            if (search != null)
            {
                return search.ToList();
            }
            return null;
        }

        public async Task Add(Order order)
              => await _context.Orders.AddAsync(order);

        public void Update(Order order)
        {
            _context.Orders.Attach(order);
            _context.Orders.Entry(order).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(Order order)
            => Update(order);
        // Se utiliza el soft delete, asi que reutilizo el metodo Update

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Order> Search(Func<Order, bool> filter)
            => _context.Orders.Where(filter).ToList();
    }
}
