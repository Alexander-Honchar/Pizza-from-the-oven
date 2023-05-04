using Microsoft.EntityFrameworkCore;
using Pizza_WebAPI.Data;
using Pizza_WebAPI.Models;
using Pizza_WebAPI.Repository.CategoryPizzaRepositori;
using Pizza_WebAPI.Repository.SizePizzaRepositori;

namespace Pizza_WebAPI.Repository.PizzaRepositori
{
    public class PizzaRepository : GenericRepository<Pizza>, IPizzaRepository
    {
        readonly ApplicationDbContext _dbContext;

        readonly  GenericRepository<Pizza> _repository;

        public PizzaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _repository = new GenericRepository<Pizza>(_dbContext);
        }

        public bool Update(Pizza newPizza, uint id)
        {
            var IsResult = false;

            var pizzaFromDb = _repository.GetOne(u => u.Id == id);
            if (pizzaFromDb.Id>0)
            {
                pizzaFromDb.Name = (pizzaFromDb.Name == newPizza.Name) ? pizzaFromDb.Name : newPizza.Name;
                pizzaFromDb.Price = (pizzaFromDb.Price == newPizza.Price) ? pizzaFromDb.Price : newPizza.Price;
                pizzaFromDb.SizePizzaId = (pizzaFromDb.SizePizzaId == newPizza.SizePizzaId) ? pizzaFromDb.SizePizzaId : newPizza.SizePizzaId;
                pizzaFromDb.CategoryPizzaId = (pizzaFromDb.CategoryPizzaId == newPizza.CategoryPizzaId) ? pizzaFromDb.CategoryPizzaId : newPizza.CategoryPizzaId;


                IsResult = true;
                
            }

            return IsResult;

        }
    }
}
