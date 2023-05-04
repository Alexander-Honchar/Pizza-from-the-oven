using Pizza_WebAPI.Data;
using Pizza_WebAPI.Models;

namespace Pizza_WebAPI.Repository.OrderHeaderRepositori
{
    public class OrderHeaderRepository : GenericRepository<OrderHeader>, IOrderHeaderRepository
    {
        readonly ApplicationDbContext _dbContext;

        readonly GenericRepository<OrderHeader> _repository;


        public OrderHeaderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext=dbContext;
            _repository=new GenericRepository<OrderHeader>(_dbContext);
        }

        public bool Update(OrderHeader newModel, uint id)
        {
            var IsResult = false;

            var objFromDb = _repository.GetOne(u => u.Id == id);

            if (objFromDb.Id > 0)
            {
                //objFromDb.ManagerId = (objFromDb.ManagerId == newModel.ManagerId) ? objFromDb.ManagerId : newModel.ManagerId;
                //objFromDb.СookId = (objFromDb.СookId == newModel.СookId) ? objFromDb.СookId : newModel.СookId;
                //objFromDb.ClientId = (objFromDb.ClientId == newModel.ClientId) ? objFromDb.ClientId : newModel.ClientId;
                //objFromDb.NumberOrder = (objFromDb.NumberOrder == newModel.NumberOrder) ? objFromDb.NumberOrder : newModel.NumberOrder;
                objFromDb.FirstName = (objFromDb.FirstName == newModel.FirstName) ? objFromDb.FirstName : newModel.FirstName;
                objFromDb.LastName = (objFromDb.LastName == newModel.LastName) ? objFromDb.LastName  : newModel.LastName;
                objFromDb.PhoneNumber = (objFromDb.PhoneNumber == newModel.PhoneNumber) ? objFromDb.PhoneNumber : newModel.PhoneNumber;
                objFromDb.Email = (objFromDb.Email == newModel.Email) ? objFromDb.Email : newModel.Email;
                objFromDb.Street = (objFromDb.Street == newModel.Street) ? objFromDb.Street : newModel.Street;
                objFromDb.House = (objFromDb.House == newModel.House) ? objFromDb.House : newModel.House;
                objFromDb.Entrance = (objFromDb.Entrance == newModel.Entrance) ? objFromDb.Entrance : newModel.Entrance;
                objFromDb.Apartment = (objFromDb.Apartment == newModel.Apartment) ? objFromDb.Apartment : newModel.Apartment;
                objFromDb.Floor = (objFromDb.Floor == newModel.Floor) ? objFromDb.Floor : newModel.Floor;
                objFromDb.WishesClient = (objFromDb.WishesClient == newModel.WishesClient) ? objFromDb.WishesClient : newModel.WishesClient;

				objFromDb.TotalSumma = (objFromDb.TotalSumma == newModel.TotalSumma) ? objFromDb.TotalSumma : newModel.TotalSumma;
				

				IsResult = true;
            }


            return IsResult;
        }
    }
}
