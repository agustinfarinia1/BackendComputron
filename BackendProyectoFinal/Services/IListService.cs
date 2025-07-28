using BackendProyectoFinal.DTOs.Cart;

namespace BackendProyectoFinal.Services
{
    public interface IListService<TDTO, TIDTO, TUDTO> : ICommonService<TDTO, TIDTO, TUDTO>
    {
        public void EmptyList(int listID,int userID);
    }
}
