using Pizza_WebAPI.Models;

namespace Pizza_WebAPI.Repository.PizzaKingSizeRepositori
{
    public interface IPizzaKingSizeRepository:IGenericRepository<PizzaKingSize>
    {
        bool Update(PizzaKingSize newModel, uint id);
    }
}
