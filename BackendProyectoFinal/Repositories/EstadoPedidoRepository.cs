using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class EstadoPedidoRepository : IRepository<EstadoPedido>
    {
        private StoreContext _context;
        public EstadoPedidoRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EstadoPedido>> Get()
            => await _context.EstadosDePedidos.ToListAsync();

        public async Task<EstadoPedido?> GetById(int id)
            => await _context.EstadosDePedidos.FindAsync(id);

        public async Task<EstadoPedido?> GetByField(string field)
            => await _context.EstadosDePedidos
                .FirstOrDefaultAsync(e => e.Nombre == field);

        public async Task Add(EstadoPedido estado)
              => await _context.EstadosDePedidos.AddAsync(estado);

        public void Update(EstadoPedido estado)
        {
            _context.EstadosDePedidos.Attach(estado);
            _context.EstadosDePedidos.Entry(estado).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(EstadoPedido estado)
            => _context.EstadosDePedidos.Remove(estado);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<EstadoPedido> Search(Func<EstadoPedido, bool> filter)
            => _context.EstadosDePedidos.Where(filter).ToList();
    }
}
