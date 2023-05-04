using Pizza_WebAPI.Models;

namespace Pizza_WebAPI.Repository.SizePizzaRepositori
{
    public interface ISizePizzaRepository:IGenericRepository<SizePizza> 
    {
        bool Update(SizePizza newSizePizza,uint id);
    }
}
