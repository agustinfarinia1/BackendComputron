using BackendProyectoFinal.Repositories;
using Microsoft.IdentityModel.Tokens;
using BackendProyectoFinal.Mappers;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.ItemCart;

namespace BackendProyectoFinal.Services
{
    public class ItemCartService : IItemCartService
    {
        private IRepository<ItemCart> _repository;
        public List<string> Errors { get; }
        public ItemCartService(IRepository<ItemCart> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<ItemCartDTO>> Get()
        {
            var itemsCarts = await _repository.Get();
            // Convierte los ItemCarts A DTO
            return itemsCarts.Select(item =>
            ItemCartMapper.ConvertItemCartToDTO(item)
            );
        }

        public async Task<ItemCartDTO?> GetById(int idCart)
        {
            var itemsCart = await _repository.GetById(idCart);
            if (itemsCart != null)
            {
                return ItemCartMapper.ConvertItemCartToDTO(itemsCart);
            }
            return null;
        }

        // Podria buscarse por UserID pero necesitaria el CartService
        public async Task<ItemCartDTO?> GetByField(string field)
        {
            var itemCart = _repository.Search(i => i.CartID == int.Parse(field)).FirstOrDefault();
            if (itemCart != null)
            {
                return ItemCartMapper.ConvertItemCartToDTO(itemCart);
            }
            return null;
        }

        public async Task<IEnumerable<ItemCartDTO>?> GetItemCartByCartId(int cartId)
        {
            var itemCarts = _repository.Search(i => i.CartID == cartId);
            if (itemCarts != null)
            {
                return itemCarts.Select(item =>
                    ItemCartMapper.ConvertItemCartToDTO(item)).ToList();
            }
            return null;
        }

        public async Task<ItemCartDTO> Add(ItemCartInsertDTO ItemCartInsertDTO)
        {
            var itemCart = ItemCartMapper.ConvertDTOToModel(ItemCartInsertDTO);
            await _repository.Add(itemCart);
            await _repository.Save();

            return ItemCartMapper.ConvertItemCartToDTO(itemCart);
        }

        public async Task<ItemCartDTO?> Update(ItemCartUpdateDTO itemCartUpdateDTO)
        {
            var itemCart = await _repository.GetById(itemCartUpdateDTO.Id);
            if (itemCart != null)
            {
                ItemCartMapper.UpdateItemCart(itemCart, itemCartUpdateDTO);

                _repository.Update(itemCart);
                await _repository.Save();

                return ItemCartMapper.ConvertItemCartToDTO(itemCart);
            }
            return null;
        }

        public async Task<ItemCartDTO?> Delete(int id)
        {
            var itemCart = await _repository.GetById(id);
            if (itemCart != null)
            {
                var itemCartDTO = ItemCartMapper.ConvertItemCartToDTO(itemCart);

                _repository.Delete(itemCart);
                await _repository.Save();
                return itemCartDTO;
            }
            return null;
        }

        public bool Validate(ItemCartInsertDTO itemOrderDTO)
        {
            if (_repository.Search(i => i.Quantity <= 0).Count() > 0)
            {
                Errors.Add("No puede existir un Item Cart que sea negativo");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(ItemCartUpdateDTO itemOrderDTO)
        {
            if (_repository.Search(
                i => i.Quantity <= 0
                && itemOrderDTO.Id != i.ItemCartID).Count() > 0)
            {
                Errors.Add("No puede existir un Item Cart que sea negativo");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
