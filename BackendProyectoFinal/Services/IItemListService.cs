
namespace BackendProyectoFinal.Services
{
    public interface IItemListService <TDTO, TIDTO, TUDTO> : ICommonService<TDTO, TIDTO, TUDTO>
    {
        public Task<IEnumerable<TDTO>?> GetItemByListId(int listID);
        public void EmptyList(int listID);
    }
}
