using Pizza_WebAPI.Data;
using Pizza_WebAPI.Repository.CategoryMenuItemRepositori;
using Pizza_WebAPI.Repository.CategoryPizzaRepositori;
using Pizza_WebAPI.Repository.ClientRepositori;
using Pizza_WebAPI.Repository.MenuItemRepositori;
using Pizza_WebAPI.Repository.OrderDetailsRepositori;
using Pizza_WebAPI.Repository.OrderHeaderRepositori;
using Pizza_WebAPI.Repository.PizzaKingSizeRepositori;
using Pizza_WebAPI.Repository.PizzaRepositori;
using Pizza_WebAPI.Repository.SizePizzaRepositori;
using Pizza_WebAPI.Repository.WorkersRepositori;

namespace Pizza_WebAPI.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationDbContext _dbContext;
        


        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext= dbContext;
            
            
            Pizza = new PizzaRepository(_dbContext);
            MenuItem = new MenuItemRepository(_dbContext);
            SizePizza=new SizePizzaRepository(_dbContext);
            CategoryPizza= new CategoryPizzaRepository(_dbContext);
            CategoryMenu=new CategoryMenuRepository(_dbContext);
            PizzaKingSize=new PizzaKingSizeRepository(_dbContext);
            Client=new ClientRepository(_dbContext);
            OrderDetails=new OrderDetailsRepository(_dbContext);
            OrderHeader=new OrderHeaderRepository(_dbContext);
            Workers = new WorkersRepository(_dbContext);

        }


        public IPizzaRepository Pizza {get; set;}

        public IMenuItemRepository MenuItem { get; set; }

        public ISizePizzaRepository SizePizza { get; set; }

        public ICategoryPizzaRepository CategoryPizza { get; set; }

        public ICategoryMenuRepository CategoryMenu { get; set; }

        public IPizzaKingSizeRepository PizzaKingSize { get; set; }

        public IClientRepository Client { get; set; }

        public IOrderHeaderRepository OrderHeader { get; set; }

        public IOrderDetailsRepository OrderDetails { get; set; }

        public IWorkersRepository Workers { get; set; }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
