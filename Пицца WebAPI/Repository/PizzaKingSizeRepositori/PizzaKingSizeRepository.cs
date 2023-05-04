using Pizza_WebAPI.Data;
using Pizza_WebAPI.Models;

namespace Pizza_WebAPI.Repository.PizzaKingSizeRepositori
{
    public class PizzaKingSizeRepository : GenericRepository<PizzaKingSize>, IPizzaKingSizeRepository
    {
        readonly ApplicationDbContext _dbContext;

        public GenericRepository<PizzaKingSize> _repository;


        public PizzaKingSizeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _repository= new GenericRepository<PizzaKingSize>(_dbContext);
        }

        public bool Update(PizzaKingSize newModel, uint id)
        {
            var IsResult = false;

            var pizzaKingSizeFromDb = _repository.GetOne(u => u.Id == id);
            if (pizzaKingSizeFromDb.Id > 0)
            {
                pizzaKingSizeFromDb.Name = (pizzaKingSizeFromDb.Name == newModel.Name) ? pizzaKingSizeFromDb.Name : newModel.Name;
                pizzaKingSizeFromDb.Price = (pizzaKingSizeFromDb.Price == newModel.Price) ? pizzaKingSizeFromDb.Price : newModel.Price;
                pizzaKingSizeFromDb.Description = (pizzaKingSizeFromDb.Description == newModel.Description) ? pizzaKingSizeFromDb.Description : newModel.Description;
                pizzaKingSizeFromDb.ImagePath = (pizzaKingSizeFromDb.ImagePath == newModel.ImagePath) ? pizzaKingSizeFromDb.ImagePath : newModel.ImagePath;


                IsResult = true;

            }

            return IsResult;
        }
    }
}
