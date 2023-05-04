using Pizza_WebAPI.Models;

namespace Pizza_WebAPI.Repository.CategoryMenuItemRepositori
{
    public interface ICategoryMenuRepository:IGenericRepository<CategoryMenu>
    {
        bool Update(CategoryMenu newModel, uint id);
    }
}
