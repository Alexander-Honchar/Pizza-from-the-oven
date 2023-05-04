using Pizza_WebAPI.Models;

namespace Pizza_WebAPI.Repository.MenuItemRepositori
{
    public interface IMenuItemRepository:IGenericRepository<MenuItem>
    {
        bool Update(MenuItem newMenuItem, uint id);
    }
}
