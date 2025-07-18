﻿using Microsoft.EntityFrameworkCore;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Repositories
{
    public class 
        CartRepository : IRepository<Cart>
    {
        private StoreContext _context;
        public CartRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Cart>> Get()
            => await _context.Carts.ToListAsync();

        public async Task<Cart?> GetById(int idCart)
            => await _context.Carts.FindAsync(idCart);

        // Retorna una lista con los Carts que estan en dicho filtro
        public async Task<IEnumerable<Cart>?> GetByField(string field)
        {
            var search = Search(c => c.UserID == int.Parse(field));
            if (search != null)
            {
                return search.ToList(); 
            }
            return null;
        }

        public async Task Add(Cart cart)
              => await _context.Carts.AddAsync(cart);

        public void Update(Cart cart)
        {
            _context.Carts.Attach(cart);
            _context.Carts.Entry(cart).State = EntityState.Modified;
            // AVISA A ENTITY FRAMEWORK QUE FUE MODIFICADO, PARA PODER REALIZAR EL SAVE
        }

        public void Delete(Cart cart)
            => Update(cart);
        // Se utiliza el soft delete, asi que reutilizo el metodo Update

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Cart> Search(Func<Cart, bool> filter)
            => _context.Carts.Where(filter).ToList();
    }
}
