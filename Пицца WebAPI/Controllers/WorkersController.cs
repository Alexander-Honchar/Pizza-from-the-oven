using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pizza_WebAPI.Data;
using Pizza_WebAPI.Models;
using Pizza_WebAPI.Models.DTO;
using Pizza_WebAPI.Models.DTO.Authorization;
using Pizza_WebAPI.Repository.UnitOfWork;
using Pizza_WebAPI.Repository.WorkersRepositori;
using Pizza_WebAPI.Utillity;
using System.Data;

namespace Pizza_WebAPI.Controllers
{

    [Authorize(Roles = StaticDetails.DirectorRole)]
    [Route("api/Workers")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        readonly ApplicationDbContext _dbContext;
        readonly IUnitOfWork _dbunitOfWork;
        UserManager<Workers> _user;
        readonly APIResponse _response;
        readonly ILogger<OrderController> _logger;


        public WorkersController( ILogger<OrderController> logger,IUnitOfWork dbunitOfWork, 
                                                                  ApplicationDbContext dbContext,
                                                                  UserManager<Workers> user)
        {
            _dbunitOfWork = dbunitOfWork;
            _dbContext = dbContext;
            _response = new APIResponse();
            _logger = logger;
            _user = user;
        }



        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<APIResponse> GetAll()
        {
            try
            {
                var listWorkersFromDb = _dbunitOfWork.Workers.GetAll().ToList();

                var result = GetListWorkers(listWorkersFromDb);

                if (listWorkersFromDb!=null)
                {
                    _logger.LogInformation("Successfully get all Workers");
                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _response.Result = result;
                    return Ok(_response);
                }

                
                _logger.LogInformation("The database does not contain Workers");
                _response.ErrorsMessages.Add("The database does not contain Workers");
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Workers not get all.Error");
                _logger.LogError(ex.ToString());
                _response.ErrorsMessages.Add(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest();
            }

        }







        [HttpGet("id", Name = "GetWorker")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<APIResponse> GetOne(string id)
        {
            try
            {
                if (id !=null)
                {
                    var result=GetOneWorkerDTO(id);


                    if (result.Id !=null)
                    {
                        _logger.LogInformation("Successfully get Worker");
                        _response.StatusCode = System.Net.HttpStatusCode.OK;
                        _response.IsSuccess = true;
                        _response.Result = result;
                        return Ok(_response);

                    }
                    else
                    {
                        _logger.LogInformation("The database does not contain this Worker");
                        _response.ErrorsMessages.Add("The database does not contain Worker");
                        _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                        _response.IsSuccess = false;
                        return NotFound(_response);
                    }
                }
                else
                {
                    _logger.LogInformation("Worker not get. Id == 0");
                    _response.ErrorsMessages.Add("Worker not get. Id == 0");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Worker not get.Error");
                _logger.LogError(ex.ToString());
                _response.ErrorsMessages.Add(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest();
            }
        }






        [HttpDelete("id", Name = "DeleteWorker")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> Delete(string id)
        {
            try
            {
                var workerFromDb = _dbunitOfWork.Workers.GetOne(u => u.Id == id);
                

                if (workerFromDb!=null)
                {
                    _dbunitOfWork.Workers.Delete(workerFromDb);

                    _dbunitOfWork.Save();
                    _logger.LogInformation("Worker Successfully removed.");
                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    return Ok(_response);

                }
                else
                {
                    _logger.LogInformation("Worker not removed. No Worker");
                    _response.ErrorsMessages.Add("Worker not removed. No Worker");
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Worker not removed.Error");
                _logger.LogError(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);
            }
        }






        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< ActionResult<APIResponse>> Update(WorkerDTO modelDTO)
        {
            try
            {
                if (modelDTO!=null)
                {

                    var IsUpdatWorker = await _dbunitOfWork.Workers.Update(modelDTO, modelDTO.Id,_user);


                    if (IsUpdatWorker)
                    {
                        _dbunitOfWork.Save();
                        _logger.LogInformation("Order Successfully update.");
                        _response.StatusCode = System.Net.HttpStatusCode.OK;
                        _response.IsSuccess = true;
                        return Ok(_response);
                    }
                    _logger.LogInformation("Order not update. No Order");
                    _response.ErrorsMessages.Add("Order not update. No Order");
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);

                }


                _logger.LogInformation("Order not update. No Order");
                _response.ErrorsMessages.Add("Order not update. No Order");
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);


            }
            catch (Exception ex)
            {
                _logger.LogInformation("Order not update.Error");
                _logger.LogError(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);
            }
        }








        #region Metods


        /// <summary>
        /// Возвращает список работников для директера
        /// </summary>
        /// <param name="workersFromDb"></param>
        /// <returns></returns>
        List<WorkerDTO> GetListWorkers(List<Workers> workersFromDb)
        {
            List<WorkerDTO> workersList= new List<WorkerDTO>();
            var listUserRoles = _dbContext.UserRoles.ToList();
            var listroles = _dbContext.Roles.ToList();

            foreach (var worker in workersFromDb)
            {
                if (worker.UserName!="Director")
                {
                    var newWorker = new WorkerDTO()
                    {
                        Id= worker.Id,
                        UserName = worker.UserName,
                        FirstName = worker.FirstName,
                        LastName = worker.LastName,
                    };

                    var role = listUserRoles.FirstOrDefault(u => u.UserId == worker.Id);
                    if (role == null)
                    {
                        newWorker.Role = null;
                        newWorker.RoleId= null;
                    }
                    else
                    {
                        newWorker.Role = listroles.FirstOrDefault(u => u.Id == role.RoleId).Name;
                        newWorker.RoleId = role.RoleId;
                    }

                    workersList.Add(newWorker);
                }
            }

            return workersList;
        }




        /// <summary>
        /// Возвращает  работника для директера
        /// </summary>
        /// <param name="workersFromDb"></param>
        /// <returns></returns>
        WorkerDTO GetOneWorkerDTO(string id)
        {
            WorkerDTO newWorker = new WorkerDTO();
            var workerList = _dbContext.Workers.ToList();
            var listUserRoles = _dbContext.UserRoles.ToList();
            var listroles = _dbContext.Roles.ToList();

            foreach (var worker in workerList)
            {
                if (worker.Id ==id)
                {
                    newWorker.Id = worker.Id;
                    newWorker.UserName = worker.UserName;
                    newWorker.FirstName = worker.FirstName;
                    newWorker.LastName = worker.LastName;

                    var role = listUserRoles.FirstOrDefault(u => u.UserId == worker.Id);
                    if (role != null)
                    {
                        newWorker.Role = listroles.FirstOrDefault(u => u.Id == role.RoleId).Name;
                        newWorker.RoleId = role.RoleId;
                    }
                   
                }
            }

            return newWorker;
        }

        #endregion


    }
}
