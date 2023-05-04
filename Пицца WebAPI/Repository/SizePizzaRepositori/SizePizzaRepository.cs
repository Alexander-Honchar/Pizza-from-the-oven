using Pizza_WebAPI.Data;
using Pizza_WebAPI.Models;

namespace Pizza_WebAPI.Repository.SizePizzaRepositori
{
    public class SizePizzaRepository : GenericRepository<SizePizza>, ISizePizzaRepository
    {
        readonly ApplicationDbContext _dbContext;
        

        public SizePizzaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            
        }

        public bool Update(SizePizza newSizePizza,uint id)
        {
            var IsResult=false;

            var objFromDb = _dbContext.SizePizzas.FirstOrDefault(u => u.Id == id);
            if (objFromDb != null)
            {

                objFromDb.Size = (newSizePizza.Size == "string") ? objFromDb.Size : newSizePizza.Size;
                IsResult=true;
            }

            return IsResult;
            
        }

           
    }
}
