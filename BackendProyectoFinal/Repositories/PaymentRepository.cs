using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class PaymentRepository : IRepository<Payment>
    {
        private StoreContext _context;
        public PaymentRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Payment>> Get()
            => await _context.Payments.ToListAsync();

        public async Task<Payment?> GetById(int id)
            => await _context.Payments.FindAsync(id);

        public async Task<IEnumerable<Payment>?> GetByField(string field)
        {
            var search = Search(p => p.OrderID == int.Parse(field));
            if (search != null)
            {
                return search.ToList();
            }
            return null;
        }

        public async Task Add(Payment payment)
              => await _context.Payments.AddAsync(payment);

        public void Update(Payment payment)
        {
            _context.Payments.Attach(payment);
            _context.Payments.Entry(payment).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(Payment payment)
            => _context.Payments.Remove(payment);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Payment> Search(Func<Payment, bool> filter)
            => _context.Payments.Where(filter).ToList();
    }
}
