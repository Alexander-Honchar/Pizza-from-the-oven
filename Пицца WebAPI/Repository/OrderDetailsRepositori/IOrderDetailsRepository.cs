using Pizza_WebAPI.Models;

namespace Pizza_WebAPI.Repository.OrderDetailsRepositori
{
    public interface IOrderDetailsRepository:IGenericRepository<OrderDetails>
    {
        bool Update(OrderDetails newModel,uint id);
    }
}
