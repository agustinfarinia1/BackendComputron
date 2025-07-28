using BackendProyectoFinal.Repositories;
using Microsoft.IdentityModel.Tokens;
using BackendProyectoFinal.Mappers;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.ItemOrder;

namespace BackendProyectoFinal.Services
{
    public class ItemOrderService : IItemListService<ItemOrderDTO, ItemOrderInsertDTO, ItemOrderUpdateDTO>
    {
        private IRepository<ItemOrder> _repository;
        public List<string> Errors { get; }
        public ItemOrderService(IRepository<ItemOrder> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<ItemOrderDTO>> Get()
        {
            var itemsOrders = await _repository.Get();
            // Convierte los ItemOrders A DTO
            return itemsOrders.Select(item =>
            ItemOrderMapper.ConvertItemOrderToDTO(item)
            );
        }

        public async Task<ItemOrderDTO?> GetById(int orderId)
        {
            var itemsOrder = await _repository.GetById(orderId);
            if (itemsOrder != null)
            {
                return ItemOrderMapper.ConvertItemOrderToDTO(itemsOrder);
            }
            return null;
        }

        // Podria buscarse por UserID pero necesitaria el OrderService
        public async Task<ItemOrderDTO?> GetByField(string field)
        {
            var itemOrder = _repository.Search(i => i.OrderID == int.Parse(field)).FirstOrDefault();
            if (itemOrder != null)
            {
                return ItemOrderMapper.ConvertItemOrderToDTO(itemOrder);
            }
            return null;
        }

        public async Task<IEnumerable<ItemOrderDTO>?> GetItemByListId(int orderId)
        {
            var itemOrders = _repository.Search(i => i.OrderID == orderId);
            if (itemOrders != null)
            {
                return itemOrders.Select(item =>
                    ItemOrderMapper.ConvertItemOrderToDTO(item)).ToList();
            }
            return null;
        }

        public async Task<ItemOrderDTO> Add(ItemOrderInsertDTO ItemOrderDTO)
        {
            var itemOrder = ItemOrderMapper.ConvertDTOToModel(ItemOrderDTO);
            await _repository.Add(itemOrder);
            await _repository.Save();

            return ItemOrderMapper.ConvertItemOrderToDTO(itemOrder);
        }

        public async Task<ItemOrderDTO?> Update(ItemOrderUpdateDTO itemOrderDTO)
        {
            var itemOrder = await _repository.GetById(itemOrderDTO.Id);
            if (itemOrder != null)
            {
                ItemOrderMapper.UpdateItemOrder(itemOrder, itemOrderDTO);

                _repository.Update(itemOrder);
                await _repository.Save();

                return ItemOrderMapper.ConvertItemOrderToDTO(itemOrder);
            }
            return null;
        }

        public async Task<ItemOrderDTO?> Delete(int id)
        {
            var itemOrder = await _repository.GetById(id);
            if (itemOrder != null)
            {
                var itemCartDTO = ItemOrderMapper.ConvertItemOrderToDTO(itemOrder);

                _repository.Delete(itemOrder);
                await _repository.Save();
                return itemCartDTO;
            }
            return null;
        }

        public bool Validate(ItemOrderInsertDTO itemOrderDTO)
        {
            if (_repository.Search(i => i.Quantity <= 0).Count() > 0)
            {
                Errors.Add("No puede existir un Item Order que sea negativo");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(ItemOrderUpdateDTO itemOrderDTO)
        {
            if (_repository.Search(
                i => i.Quantity <= 0
                && itemOrderDTO.Id != i.ItemOrderID).Count() > 0)
            {
                Errors.Add("No puede existir un Item Order que sea negativo");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public void EmptyList(int listID)
        {
            throw new NotImplementedException();
        }
    }
}
