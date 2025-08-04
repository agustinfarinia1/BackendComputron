using BackendProyectoFinal.Repositories;
using Microsoft.IdentityModel.Tokens;
using BackendProyectoFinal.Mappers;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.Cart;
using BackendProyectoFinal.DTOs.Cart.ItemCart;

namespace BackendProyectoFinal.Services
{
    public class CartService : IListService<CartDTO, CartInsertDTO, CartUpdateDTO>
    {
        private IItemListService<ItemCartDTO,ItemCartInsertDTO,ItemCartUpdateDTO> _itemCartService;
        private IRepository<Cart> _repository;
        public List<string> Errors { get; }
        public CartService(
            [FromKeyedServices("ItemCartService")] IItemListService<ItemCartDTO, ItemCartInsertDTO, ItemCartUpdateDTO> itemCartService,
            IRepository<Cart> repository)
        {
            _itemCartService = itemCartService;
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<CartDTO>> Get()
        {
            var carts = await _repository.Get();
            // Convierte los Carts A DTO
            var cartsDTO = carts.Select(c
                => CartMapper.ConvertCartToDTO(c)).ToList();
            foreach (var cartDTO in cartsDTO)
            {
                // Lista de ItemCart ligada a Cart
                var listCartDTO = await _itemCartService.GetItemByListId(cartDTO.Id);
                if(listCartDTO != null && listCartDTO.Count() > 0)
                {
                    CartMapper.UpdateCart(cartDTO, listCartDTO.ToList());
                }
            }
            return cartsDTO;
        }

        public async Task<CartDTO?> GetById(int cartId)
        {
            var cart = await _repository.GetById(cartId);
            if (cart != null)
            {
                var cartDTO = CartMapper.ConvertCartToDTO(cart);
                var listCartDTO = await _itemCartService.GetItemByListId(cartDTO.Id);
                if(listCartDTO != null)
                    CartMapper.UpdateCart(cartDTO, listCartDTO.ToList());
                return cartDTO;
            }
            return null;
        }

        // Filtra por UserID
        public async Task<CartDTO?> GetByField(string field)
        {
            var cart = _repository.Search(c => c.UserID == int.Parse(field)).FirstOrDefault();
            if (cart != null)
            {
                var cartDTO = CartMapper.ConvertCartToDTO(cart);

                var listCartDTO = await _itemCartService.GetItemByListId(cartDTO.Id);
                if (listCartDTO != null)
                    CartMapper.UpdateCart(cartDTO, listCartDTO.ToList());
                return cartDTO;
            }
            return null;
        }

        // Inicializacion del Cart, todas las operaciones con la listCarts se hacen dentro de ItemCart
        public async Task<CartDTO> Add(CartInsertDTO cartInsertDTO)
        {
            var cart = CartMapper.ConvertDTOToModel(cartInsertDTO);
            await _repository.Add(cart);
            await _repository.Save();
            return CartMapper.ConvertCartToDTO(cart);
        }

        // No tiene uso, solo se modifica cuando se crea la referencia en el inicio
        public async Task<CartDTO?> Update(CartUpdateDTO cartUpdateDTO)
        {
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

        public void EmptyList(int listID, int userID)
        {
            throw new NotImplementedException();
        }
    }
}
