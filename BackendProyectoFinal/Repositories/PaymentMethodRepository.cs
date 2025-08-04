using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class PaymentMethodRepository : IRepository<PaymentMethod>
    {
        private StoreContext _context;
        public PaymentMethodRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PaymentMethod>> Get()
            => await _context.PaymentMethods.ToListAsync();

        public async Task<PaymentMethod?> GetById(int id)
            => await _context.PaymentMethods.FindAsync(id);

        public async Task<IEnumerable<PaymentMethod>?> GetByField(string field)
        {
            var search = Search(r => r.Name == field);
            if (search != null)
            {
                return search.ToList();
            }
            return null;
        }

        public async Task Add(PaymentMethod paymentMethod)
              => await _context.PaymentMethods.AddAsync(paymentMethod);

        public void Update(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Attach(paymentMethod);
            _context.PaymentMethods.Entry(paymentMethod).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(PaymentMethod paymentMethod)
            => _context.PaymentMethods.Remove(paymentMethod);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PaymentMethod> Search(Func<PaymentMethod, bool> filter)
            => _context.PaymentMethods.Where(filter).ToList();
    }
}
