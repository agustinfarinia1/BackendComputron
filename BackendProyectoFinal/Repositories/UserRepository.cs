using BackendProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private StoreContext _context;
        public UserRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> Get()
            => await _context.Users
                .Where(u => u.Eliminated == false).ToListAsync();

        public async Task<User?> GetById(int id)
            => await _context.Users
                .FirstOrDefaultAsync(u => u.UserID == id && u.Eliminated == false);

        public async Task<IEnumerable<User>?> GetByField(string field)
        {
            var search = Search(u => u.Email == field && u.Eliminated == false);
            if (search != null)
            {
                return search.ToList();
            }
            return null;
        }

        public async Task Add(User user)
              => await _context.Users.AddAsync(user);

        public void Update(User user)
        {
            _context.Users.Attach(user);
            _context.Users.Entry(user).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(User user)
            => Update(user);
        // Se utiliza el soft delete, asi que reutilizo el metodo Update

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<User> Search(Func<User, bool> filter)
            => _context.Users.Where(filter).ToList();
    }
}
