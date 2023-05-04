using Pizza_WebAPI.Models;

namespace Pizza_WebAPI.Repository.ClientRepositori
{
    public interface IClientRepository:IGenericRepository<Client>
    {
        bool Update(Client newModel,uint id);
    }
}
