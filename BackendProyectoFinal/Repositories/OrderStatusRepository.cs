using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class OrderStatusRepository : IRepository<OrderStatus>
    {
        private StoreContext _context;
        public OrderStatusRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OrderStatus>> Get()
            => await _context.OrderStatuses.ToListAsync();

        public async Task<OrderStatus?> GetById(int id)
            => await _context.OrderStatuses.FindAsync(id);

        public async Task<IEnumerable<OrderStatus>?> GetByField(string field)
        {
            var search = Search(o => o.Name == field);
            if (search != null)
            {
                return search.ToList();
            }
            return null;
        }

        public async Task Add(OrderStatus orderStatus)
              => await _context.OrderStatuses.AddAsync(orderStatus);

        public void Update(OrderStatus orderStatus)
        {
            _context.OrderStatuses.Attach(orderStatus);
            _context.OrderStatuses.Entry(orderStatus).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(OrderStatus orderStatus)
            => _context.OrderStatuses.Remove(orderStatus);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<OrderStatus> Search(Func<OrderStatus, bool> filter)
            => _context.OrderStatuses.Where(filter).ToList();
    }
}
