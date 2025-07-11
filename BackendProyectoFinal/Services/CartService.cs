using BackendProyectoFinal.Repositories;
using Microsoft.IdentityModel.Tokens;
using BackendProyectoFinal.Mappers;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.CartDTO;

namespace BackendProyectoFinal.Services
{
    public class CartService : 
        ICommonService<CartDTO, CartInsertDTO, CartUpdateDTO>
    {
        private IRepository<Cart> _repository;
        public List<string> Errors { get; }
        public CartService(IRepository<Cart> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<CartDTO>> Get()
        {
            var carts = await _repository.Get();
            // CONVIERTE LOS Carritos A DTO
            return carts.Select(c 
                => CartMapper.ConvertCartToDTO(c));
        }

        public async Task<CartDTO?> GetById(int idCart)
        {
            var cart = await _repository.GetById(idCart);
            if (cart != null)
            {
                return CartMapper.ConvertCartToDTO(cart);
            }
            return null;
        }

        public async Task<CartDTO?> GetByField(string field)
        {
            var cart = await _repository.GetByField(field);
            if (cart != null)
            {
                return CartMapper.ConvertCartToDTO(cart);
            }
            return null;
        }

        public async Task<ItemCart?> GetByItemListaId(int userID, int itemID)
        {
            var userCart = await GetByField(userID.ToString());
            if (userCart == null)
            {
                return userCart.ListCarts.FirstOrDefault(e => e.ItemCartID == itemID);
            }
            return null;
        }

        public async Task<CartDTO> Add(CartInsertDTO cartInsertDTO)
        {
            var cart = CartMapper.ConvertDTOToModel(cartInsertDTO);
            await _repository.Add(cart);
            await _repository.Save();
            return CartMapper.ConvertCartToDTO(cart);
        }

        public async Task<CartDTO?> Update(CartUpdateDTO cartUpdateDTO)
        {
            var cart = await _repository.GetById(cartUpdateDTO.Id);
            if (cart != null)
            {
                CartMapper.UpdateCart(cart, cartUpdateDTO);
                _repository.Update(cart);
                await _repository.Save();

                return CartMapper.ConvertCartToDTO(cart);
            }
            return null;
        }

        public async Task<CartDTO?> Delete(int id)
        {
            var cart = await _repository.GetById(id);
            if (cart != null)
            {
                var carritoDTO = CartMapper.ConvertCartToDTO(cart);

                _repository.Delete(cart);
                await _repository.Save();
                return carritoDTO;
            }
            return null;
        }

        public bool Validate(CartInsertDTO cartDTO)
        {
            if (_repository.Search(c 
                => c.UserID == cartDTO.UserId)
                .Count() > 0)
            {
                Errors.Add("No puede existir un carrito con un usuario ya existente");
            }
            if (_repository.Search(c 
                => c.ListCarts.Count() > 0)
                .Count() > 0)
            {
                Errors.Add("El carrito tiene que contener productos");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(CartUpdateDTO cartDTO)
        {
            if (_repository.Search(c 
                => c.UserID == cartDTO.UserId
                && cartDTO.Id != c.CartID)
                .Count() > 0)
            {
                Errors.Add("No puede existir un carrito con un usuario ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
