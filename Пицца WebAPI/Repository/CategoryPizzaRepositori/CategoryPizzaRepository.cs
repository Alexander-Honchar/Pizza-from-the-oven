using Pizza_WebAPI.Data;
using Pizza_WebAPI.Models;

namespace Pizza_WebAPI.Repository.CategoryPizzaRepositori
{
    public class CategoryPizzaRepository : GenericRepository<CategoryPizza>, ICategoryPizzaRepository
    {
        readonly ApplicationDbContext _dbContext;

        public CategoryPizzaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Update(CategoryPizza newCategoryPizza, uint id)
        {
            var IsResult = false;

            var objFromDb = _dbContext.CategoryPizzas.FirstOrDefault(u => u.Id == id);
            if (objFromDb != null)
            {

                objFromDb.Name = (newCategoryPizza.Name == "string") ? objFromDb.Name : newCategoryPizza.Name;
                IsResult = true;
            }

            return IsResult;

        }
    }
}
