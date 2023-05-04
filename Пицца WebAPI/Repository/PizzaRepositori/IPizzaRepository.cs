using Pizza_WebAPI.Models;

namespace Pizza_WebAPI.Repository.PizzaRepositori
{
    public interface IPizzaRepository:IGenericRepository<Pizza>
    {
        bool Update(Pizza newPizza, uint id);
    }
}
