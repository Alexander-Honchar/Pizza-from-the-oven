using Microsoft.EntityFrameworkCore;
using Pizza_WebAPI.Data;
using Pizza_WebAPI.Models;
using Pizza_WebAPI.Repository.CategoryPizzaRepositori;
using Pizza_WebAPI.Repository.PizzaRepositori;
using Pizza_WebAPI.Repository.SizePizzaRepositori;

namespace Pizza_WebAPI.Repository.MenuItemRepositori
{
    public class MenuItemRepository : GenericRepository<MenuItem>, IMenuItemRepository
    {
        readonly ApplicationDbContext _dbContext;
        readonly  GenericRepository<MenuItem> _repository;




        public MenuItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _repository = new GenericRepository<MenuItem>(_dbContext);
            
        }

        public bool Update(MenuItem newMenuItem, uint id)
        {
            var IsResult = false;

            var objFromDb = _repository.GetOne(u => u.Id == id);

            if (objFromDb.Id>0)
            {
                objFromDb.Name = (objFromDb.Name == newMenuItem.Name) ? objFromDb.Name : newMenuItem.Name;
                objFromDb.Price = (objFromDb.Price == newMenuItem.Price) ? objFromDb.Price : newMenuItem.Price;
                objFromDb.ImagePatch = (objFromDb.ImagePatch == newMenuItem.ImagePatch) ? objFromDb.ImagePatch : newMenuItem.ImagePatch;
                objFromDb.Description = (objFromDb.Description == newMenuItem.Description) ? objFromDb.Description : newMenuItem.Description;
                objFromDb.NameCategory = (objFromDb.NameCategory == newMenuItem.NameCategory) ? objFromDb.NameCategory : newMenuItem.NameCategory;

                IsResult = true;
            }
            

            return IsResult;
        }
    }
}
