using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class CarritoRepository : IRepository<Carrito>
    {
        private StoreContext _context;
        public CarritoRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Carrito>> Get()
            => await _context.Carritos.ToListAsync();

        public async Task<Carrito?> GetById(int id)
            => await _context.Carritos.FindAsync(id);

        public async Task<Carrito?> GetByField(string field)
            => await _context.Carritos
                .FirstOrDefaultAsync(c => c.UsuarioID == int.Parse(field));

        public async Task Add(Carrito usuario)
              => await _context.Carritos.AddAsync(usuario);

        public void Update(Carrito usuario)
        {
            _context.Carritos.Attach(usuario);
            _context.Carritos.Entry(usuario).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(Carrito usuario)
            => Update(usuario);
        // Se utiliza el soft delete, asi que reutilizo el metodo Update

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Carrito> Search(Func<Carrito, bool> filter)
            => _context.Carritos.Where(filter).ToList();
    }
}
