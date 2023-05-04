using Pizza_WebAPI.Models;

namespace Pizza_WebAPI.Repository.OrderHeaderRepositori
{
    public interface IOrderHeaderRepository:IGenericRepository<OrderHeader>
    {
        bool Update(OrderHeader model, uint id);
    }
}
