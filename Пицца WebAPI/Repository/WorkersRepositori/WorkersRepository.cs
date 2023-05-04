using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;
using Pizza_WebAPI.Data;
using Pizza_WebAPI.Models;
using Pizza_WebAPI.Models.DTO;
using Pizza_WebAPI.Models.DTO.Authorization;
using Pizza_WebAPI.Repository.UnitOfWork;
using Pizza_WebAPI.Utillity;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pizza_WebAPI.Repository.WorkersRepositori
{
    public class WorkersRepository : GenericRepository<Workers>, IWorkersRepository
    {
        readonly ApplicationDbContext _dbContext;
        

        public WorkersRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext= dbContext;
        }


        



        public async Task<bool> Update(WorkerDTO newModel, string id, UserManager<Workers> _user)
        {
            var IsResult = false;

            var objFromDb = _dbContext.Workers.FirstOrDefault(u=>u.Id==id);


            if (objFromDb.Id != null)
            {
                objFromDb.UserName = (objFromDb.UserName == newModel.UserName) ? objFromDb.UserName : newModel.UserName;
                objFromDb.FirstName = (objFromDb.FirstName == newModel.FirstName) ? objFromDb.FirstName : newModel.FirstName;
                objFromDb.LastName = (objFromDb.LastName == newModel.LastName) ? objFromDb.LastName : newModel.LastName;

                var oldRole = _dbContext.Roles.Where(u => u.Id == newModel.RoleId).Select(e => e.Name).FirstOrDefault();
                //removing the old role
                await _user.RemoveFromRoleAsync(objFromDb, oldRole);
                //add new role
                await _user.AddToRoleAsync(objFromDb, _dbContext.Roles.FirstOrDefault(u => u.Id == newModel.RoleId).Name);


                IsResult = true;
            }


            return IsResult;
        }
    }
    
}
