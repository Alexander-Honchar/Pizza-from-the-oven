using Pizza_WebAPI.Models;

namespace Pizza_WebAPI.Repository.CategoryPizzaRepositori
{
    public interface ICategoryPizzaRepository:IGenericRepository<CategoryPizza>
    {
        bool Update(CategoryPizza newCategoryPizza, uint id);
    }
}
