using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BackendProyectoFinal.Repositories
{
    public class RolRepository : IRepository<Rol>
    {
        private StoreContext _context;
        public RolRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Rol>> Get()
            => await _context.Roles.ToListAsync();

        public async Task<Rol?> GetById(int id)
            => await _context.Roles.FindAsync(id);

        public async Task<Rol?> GetByField(string field)
            => await _context.Roles
                .FirstOrDefaultAsync(r => r.Nombre == field);

        public async Task Add(Rol rol)
              => await _context.Roles.AddAsync(rol);

        public void Update(Rol rol)
        {
            _context.Roles.Attach(rol);
            _context.Roles.Entry(rol).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(Rol rol)
            => _context.Roles.Remove(rol);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Rol> Search(Func<Rol, bool> filter)
            => _context.Roles.Where(filter).ToList();
    }
}
