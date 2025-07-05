using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class PedidoRepository : IRepository<Pedido>
    {
        private StoreContext _context;
        public PedidoRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Pedido>> Get()
            => await _context.Pedidos.ToListAsync();

        public async Task<Pedido?> GetById(int id)
            => await _context.Pedidos.FindAsync(id);

        public async Task<Pedido?> GetByField(string field)
            => await _context.Pedidos
                .FirstOrDefaultAsync(p 
                    => p.UsuarioID == int.Parse(field));

        public async Task Add(Pedido pedido)
              => await _context.Pedidos.AddAsync(pedido);

        public void Update(Pedido pedido)
        {
            _context.Pedidos.Attach(pedido);
            _context.Pedidos.Entry(pedido).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(Pedido pedido)
            => Update(pedido);
        // Se utiliza el soft delete, asi que reutilizo el metodo Update

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Pedido> Search(Func<Pedido, bool> filter)
            => _context.Pedidos.Where(filter).ToList();
    }
}
