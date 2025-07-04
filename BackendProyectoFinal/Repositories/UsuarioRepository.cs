using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class UsuarioRepository : IRepository<Usuario>
    {
        private StoreContext _context;
        public UsuarioRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Usuario>> Get()
            => await _context.Usuarios
                .Where(u => u.Eliminado == false).ToListAsync();

        public async Task<Usuario?> GetById(int id)
            => await _context.Usuarios
                .FirstOrDefaultAsync(u => u.UsuarioID == id && u.Eliminado == false);

        public async Task<Usuario?> GetByField(string field)
            => await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == field && u.Eliminado == false);

        public async Task Add(Usuario usuario)
              => await _context.Usuarios.AddAsync(usuario);

        public void Update(Usuario usuario)
        {
            _context.Usuarios.Attach(usuario);
            _context.Usuarios.Entry(usuario).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(Usuario usuario)
            => Update(usuario);
        // Se utiliza el soft delete, asi que reutilizo el metodo Update

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Usuario> Search(Func<Usuario, bool> filter)
            => _context.Usuarios.Where(filter).ToList();
    }
}
