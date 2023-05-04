using Pizza_WebAPI.Data;
using Pizza_WebAPI.Models;

namespace Pizza_WebAPI.Repository.ClientRepositori
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        readonly GenericRepository<Client> _repository;

        public ClientRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _repository= new GenericRepository<Client>(dbContext);
        }

        public bool Update(Client newModel, uint id)
        {
            var IsResult = false;

            var objFromDb = _repository.GetOne(u => u.Id == id);

            if (objFromDb.Id > 0)
            {
                objFromDb.FirstName = (objFromDb.FirstName == newModel.FirstName) ? objFromDb.FirstName : newModel.FirstName;
                objFromDb.LastName = (objFromDb.LastName == newModel.LastName) ? objFromDb.LastName : newModel.LastName;
                objFromDb.PhoneNumber = (objFromDb.PhoneNumber == newModel.PhoneNumber) ? objFromDb.PhoneNumber : newModel.PhoneNumber;
                

                IsResult = true;
            }


            return IsResult;
        }
    }
}
