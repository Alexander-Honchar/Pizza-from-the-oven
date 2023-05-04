
using Pizza_WebAPI.Repository.CategoryMenuItemRepositori;
using Pizza_WebAPI.Repository.CategoryPizzaRepositori;
using Pizza_WebAPI.Repository.ClientRepositori;
using Pizza_WebAPI.Repository.MenuItemRepositori;
using Pizza_WebAPI.Repository.OrderDetailsRepositori;
using Pizza_WebAPI.Repository.OrderHeaderRepositori;
using Pizza_WebAPI.Repository.PizzaKingSizeRepositori;
using Pizza_WebAPI.Repository.PizzaRepositori;
using Pizza_WebAPI.Repository.SizePizzaRepositori;
using Pizza_WebAPI.Repository.WorkersRepositori;

namespace Pizza_WebAPI.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPizzaRepository Pizza { get; }
        IMenuItemRepository MenuItem { get; }
        ISizePizzaRepository SizePizza { get; }
        ICategoryPizzaRepository CategoryPizza { get; }
        ICategoryMenuRepository CategoryMenu { get; }
        IPizzaKingSizeRepository PizzaKingSize { get; }
        IClientRepository Client { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailsRepository OrderDetails { get; }
        IWorkersRepository Workers { get; }



        void Save();
    }
}
