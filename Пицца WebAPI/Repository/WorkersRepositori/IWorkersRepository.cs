using Microsoft.AspNetCore.Identity;
using Pizza_WebAPI.Models;

using Pizza_WebAPI.Models.DTO;
using Pizza_WebAPI.Models.DTO.Authorization;

namespace Pizza_WebAPI.Repository.WorkersRepositori
{
    public interface IWorkersRepository:IGenericRepository<Workers>
    {
        Task<bool> Update(WorkerDTO newModel, string id, UserManager<Workers> _user);
    }
}
