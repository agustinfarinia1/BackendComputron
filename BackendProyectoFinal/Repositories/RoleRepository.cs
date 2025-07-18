using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class RoleRepository : IRepository<Role>
    {
        private StoreContext _context;
        public RoleRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Role>> Get()
            => await _context.Roles.ToListAsync();

        public async Task<Role?> GetById(int id)
            => await _context.Roles.FindAsync(id);

        public async Task<IEnumerable<Role>?> GetByField(string field)
        {
            var search = Search(r => r.Name == field);
            if (search != null)
            {
                return search.ToList();
            }
            return null;
        }

        public async Task Add(Role role)
              => await _context.Roles.AddAsync(role);

        public void Update(Role role)
        {
            _context.Roles.Attach(role);
            _context.Roles.Entry(role).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(Role role)
            => _context.Roles.Remove(role);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Role> Search(Func<Role, bool> filter)
            => _context.Roles.Where(filter).ToList();
    }
}
