using Pizza_WebAPI.Data;
using Pizza_WebAPI.Models;

namespace Pizza_WebAPI.Repository.OrderDetailsRepositori
{
    public class OrderDetailsRepository : GenericRepository<OrderDetails>, IOrderDetailsRepository
    {
        readonly GenericRepository<OrderDetails> _repository;

        public OrderDetailsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _repository=new GenericRepository<OrderDetails>(dbContext);
        }

        public bool Update(OrderDetails newModel, uint id)
        {
            var IsResult = false;

            var objFromDb = _repository.GetOne(u => u.Id == id);

            if (objFromDb.Id > 0)
            {
                //objFromDb.OrderHeaderId = (objFromDb.OrderHeaderId == newModel.OrderHeaderId) ? objFromDb.OrderHeaderId : newModel.OrderHeaderId;
                //objFromDb.MenuId = (objFromDb.MenuId == newModel.MenuId) ? objFromDb.MenuId : newModel.MenuId;
                //objFromDb.MenuName = (objFromDb.MenuName == newModel.MenuName) ? objFromDb.MenuName : newModel.MenuName;
                //objFromDb.ProductId = (objFromDb.ProductId == newModel.ProductId) ? objFromDb.ProductId : newModel.ProductId;
                //objFromDb.NameCategory = (objFromDb.NameCategory == newModel.NameCategory) ? objFromDb.NameCategory : newModel.NameCategory;
                objFromDb.Count = (objFromDb.Count == newModel.Count) ? objFromDb.Count : newModel.Count;
                objFromDb.Price = (objFromDb.Price == newModel.Price) ? objFromDb.Price : newModel.Price;


                IsResult = true;
            }


            return IsResult;
        }
    }
}
