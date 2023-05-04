using Pizza_WebAPI.Data;
using Pizza_WebAPI.Models;

namespace Pizza_WebAPI.Repository.CategoryMenuItemRepositori
{
    public class CategoryMenuRepository : GenericRepository<CategoryMenu>, ICategoryMenuRepository
    {

        readonly ApplicationDbContext _dbContext;

        readonly  GenericRepository<CategoryMenu> _repository;

        public CategoryMenuRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _repository = new GenericRepository<CategoryMenu>(_dbContext);
        }

        public bool Update(CategoryMenu newModel, uint id)
        {
            var IsResult = false;

            var categoryMenuFromDb = _repository.GetOne(u => u.Id == id);
            if (categoryMenuFromDb.Id > 0)
            {
                categoryMenuFromDb.NameCategory = (categoryMenuFromDb.NameCategory == newModel.NameCategory) ? categoryMenuFromDb.NameCategory : newModel.NameCategory;
                


                IsResult = true;

            }

            return IsResult;
        }
    }
}
