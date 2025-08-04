using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class PaymentDetailRepository : IRepository<PaymentDetail>
    {
        private StoreContext _context;
        public PaymentDetailRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PaymentDetail>> Get()
            => await _context.PaymentDetails.ToListAsync();

        public async Task<PaymentDetail?> GetById(int id)
            => await _context.PaymentDetails.FindAsync(id);

        public async Task<IEnumerable<PaymentDetail>?> GetByField(string field)
        {
            var search = Search(p => p.PaymentID == int.Parse(field));
            if (search != null)
            {
                return search.ToList();
            }
            return null;
        }

        public async Task Add(PaymentDetail role)
              => await _context.PaymentDetails.AddAsync(role);

        public void Update(PaymentDetail role)
        {
            _context.PaymentDetails.Attach(role);
            _context.PaymentDetails.Entry(role).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(PaymentDetail role)
            => _context.PaymentDetails.Remove(role);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PaymentDetail> Search(Func<PaymentDetail, bool> filter)
            => _context.PaymentDetails.Where(filter).ToList();
    }
}
