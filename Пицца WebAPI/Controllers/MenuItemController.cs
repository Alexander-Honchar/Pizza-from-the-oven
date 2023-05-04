using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Pizza_WebAPI.Models;
using Pizza_WebAPI.Models.DTO;
using Pizza_WebAPI.Repository.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Pizza_WebAPI.Utillity;

namespace Pizza_WebAPI.Controllers
{
    [Route("api/MenuItem")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        protected APIResponse _response;

        readonly IUnitOfWork _dbunitOfWork;
        readonly IMapper _mapper;
        readonly ILogger<MenuItemController> _logger;


        public Pizza modelPizzaForDb;
        public MenuItem modelMenuItemForDb;
        public IEnumerable<MenuItem> MenuItemList { get; set; }


        public MenuItemController(IUnitOfWork dbunitOfWork, ILogger<MenuItemController> logger, IMapper mapper)
        {
            _dbunitOfWork = dbunitOfWork;
            _logger = logger;
            _mapper = mapper;
            MenuItemList= new List<MenuItem>();
            modelPizzaForDb= new Pizza();
            modelMenuItemForDb= new MenuItem();
            _response= new APIResponse();
            _response.ErrorsMessages = new List<string>();
        }



        [HttpGet]
        //[ResponseCache(Duration = 30)]   //caching
        [ResponseCache(CacheProfileName = "Default30")] //caching
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> GetAll()
        {
            try
            {
                MenuItemList = _dbunitOfWork.MenuItem.GetAll();
                
                if (MenuItemList.Count() > 0)
                {
                    _response.Result = MenuItemList;
                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    _response.IsSuccess= true;

                    _logger.LogInformation("Successfully get all MenuItem");
                    return Ok(_response);
                }
                _logger.LogInformation("The database does not contain MenuItem");
                _response.IsSuccess = false;
                _response.StatusCode= System.Net.HttpStatusCode.NotFound;
                _response.ErrorsMessages.Add("Error.The database does not contain MenuItem");
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("MenuItem not get all.Error");
                _logger.LogError(ex.ToString());

                _response.IsSuccess = false;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);
            }

        }



        [HttpGet("nameCategory", Name = "GetAllNameCategory")]
        //[ResponseCache(Duration = 30)]   //caching
        [ResponseCache(CacheProfileName = "Default30")] //caching
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> GetAllNameCategory(string nameCategory)
        {
            try
            {
                MenuItemList = _dbunitOfWork.MenuItem.GetAll();

                if (MenuItemList.Count() > 0)
                {
                    var result =new List<MenuItem>();

                    foreach (var item in MenuItemList)
                    {
                        if (item.NameCategory== nameCategory)
                        {
                            result.Add(item);
                        }
                    }
                    if (result.Count>0)
                    {
                        _response.Result = result;
                        _response.StatusCode = System.Net.HttpStatusCode.OK;
                        _response.IsSuccess = true;

                        _logger.LogInformation("Successfully get all MenuItem");
                        return Ok(_response);
                    }
                    _logger.LogInformation("The database does not contain MenuItem");
                    _response.IsSuccess = false;
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    _response.ErrorsMessages.Add("Error.The database does not contain MenuItem");
                    return NotFound(_response);
                }
                _logger.LogInformation("The database does not contain MenuItem");
                _response.IsSuccess = false;
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.ErrorsMessages.Add("Error.The database does not contain MenuItem");
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("MenuItem not get all.Error");
                _logger.LogError(ex.ToString());

                _response.IsSuccess = false;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);
            }

        }





        [HttpGet("id", Name = "GetOneMenuItem")]
		[ResponseCache(CacheProfileName = "Default30")]
		[ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> GetOne(uint id)
        {
            try
            {
                if (id > 0)
                {

                    modelMenuItemForDb = _dbunitOfWork.MenuItem.GetOne(u => u.Id == id/*,includeProperties:"Pizza"*/);
                    //modelPizzaForDb=_dbunitOfWork.Pizza.GetOne(u => u.Id == modelMenuItemForDb.PizzaMenuItemId,includeProperties: "SizePizza,CategoryPizza");
                    //modelMenuItemForDb.Pizza = modelPizzaForDb;
                    if (modelMenuItemForDb.Id > 0)
                    {
                        _response.Result = modelMenuItemForDb;
                        _response.StatusCode = System.Net.HttpStatusCode.OK;
                        _response.IsSuccess = true;

                        _logger.LogInformation("Successfully get MenuItem");
                        return Ok(_response);
                    }
                    else
                    {
                        
                        _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                        _response.IsSuccess = false;
                        _response.ErrorsMessages.Add("The database does not contain this MenuItem");
                        _logger.LogInformation("The database does not contain this MenuItem");
                        return NotFound(_response);
                    }
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.ErrorsMessages.Add("MenuItem not get. Id == 0");
                    _logger.LogInformation("MenuItem not get. Id == 0");
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("MenuItem not get.Error");
                _logger.LogError(ex.ToString());

                _response.IsSuccess = false;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);
            }
        }




        [Authorize(Roles = StaticDetails.AdministratorRole)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<APIResponse> Create(MenuItemDTO modelDTO)
        {
            try
            {
                if (modelDTO == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.ErrorsMessages.Add("MenuItem not created. MenuItem not valid");
                    _logger.LogInformation("MenuItem not created. MenuItem not valid");
                    return BadRequest(_response);
                }

                var nameObjFromDb=_dbunitOfWork.MenuItem.GetOne(u=>u.Name==modelDTO.Name);

                if (modelDTO.Name!=null && nameObjFromDb.Name!=null && modelDTO.Name== nameObjFromDb.Name)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.ErrorsMessages.Add("MenuItem not created. Name MenuItem not uniqum");
                    _logger.LogInformation("MenuItem not created. Name MenuItem not uniqum");
                    return BadRequest(_response);
                }

                if (ModelState.IsValid)
                {
                    var objForDb=_mapper.Map<MenuItem>(modelDTO);
                    _dbunitOfWork.MenuItem.Create(objForDb);
                    _dbunitOfWork.Save();

                    _response.Result = objForDb;
                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    _response.IsSuccess = true;

                    return Ok(_response);
                }
                _response.IsSuccess = false;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.ErrorsMessages.Add("MenuItem not created. MenuItem not valid");
                _logger.LogInformation("MenuItem not created. MenuItem not valid");
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("MenuItem not created.Error");
                _logger.LogError(ex.ToString());

                _response.IsSuccess = false;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);
            }
          
        }




        [Authorize(Roles = StaticDetails.AdministratorRole)]
        [HttpDelete("id", Name = "DeleteMenuItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> Delete(uint id)
        {
            try
            {
                modelMenuItemForDb = _dbunitOfWork.MenuItem.GetOne(u => u.Id == id);
                if (modelMenuItemForDb.Id > 0)
                {
                    _dbunitOfWork.MenuItem.Delete(modelMenuItemForDb);
                    _dbunitOfWork.Save();
                    _logger.LogInformation("MenuItem Successfully removed.");

                    //_response.Result = modelMenuItemForDb;
                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    return Ok(_response);
                }
                else
                {
                    _logger.LogInformation("MenuItem not removed. No MenuItem");

                    _response.IsSuccess = false;
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.ErrorsMessages.Add("MenuItem not removed. No MenuItem");
                    return BadRequest(_response);
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation("MenuItem not removed.Error");
                _logger.LogError(ex.ToString());

                _response.IsSuccess = false;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);
            }
        }





        [Authorize(Roles = StaticDetails.AdministratorRole)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> Update(MenuItemDTO modelDTO)
        {
            try
            {
                if (modelDTO.Id > 0)
                {
                    var modelMenuItemForDb = _mapper.Map<MenuItem>(modelDTO);


                    var IsResult = _dbunitOfWork.MenuItem.Update(modelMenuItemForDb, modelDTO.Id);
                    if (IsResult)
                    {
                        _dbunitOfWork.Save();
                        _logger.LogInformation("MenuItem Successfully update.");

                        _response.StatusCode = System.Net.HttpStatusCode.OK;
                        _response.IsSuccess = true;
                        return Ok(_response);
                    }
                    _logger.LogInformation("MenuItem not update. No MenuItem");

                    _response.IsSuccess = false;
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    _response.ErrorsMessages.Add("MenuItem not update. No MenuItem");
                    return NotFound(_response);

                }


                _logger.LogInformation("MenuItem not update. No MenuItem");

                _response.IsSuccess = false;
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.ErrorsMessages.Add("MenuItem not update. No MenuItem");
                return NotFound(_response);


            }
            catch (Exception ex)
            {
                _logger.LogInformation("MenuItem not update.Error");
                _logger.LogError(ex.ToString());

                _response.IsSuccess = false;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);
            }
        }



        [Authorize(Roles = StaticDetails.AdministratorRole)]
        /// <summary>
        /// "path": "/name",
        ///"op": "replace",
        ///"value": "11111"
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pathModelDTO"></param>
        /// <returns></returns>
        [HttpPatch("id", Name = "UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> UpdatePartial(int id,JsonPatchDocument<MenuItem> pathModelDTO) 
        {
            try
            {
                if (id > 0 && pathModelDTO != null)
                {
                    var objFromDb = _dbunitOfWork.MenuItem.GetOne(u => u.Id == id);
                    if (objFromDb.Id > 0)
                    {
                        if (ModelState.IsValid)
                        {
                            pathModelDTO.ApplyTo(objFromDb);
                            _dbunitOfWork.Save();
                            _logger.LogInformation("MenuItem successfully path.");

                            _response.StatusCode = System.Net.HttpStatusCode.OK;
                            _response.IsSuccess = true;

                            return Ok(_response);
                        }
                        _response.IsSuccess = false;
                        _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        _response.ErrorsMessages.Add("MenuItem not path.BadRequest");
                        return BadRequest(_response);
                    }
                    _response.IsSuccess = false;
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.ErrorsMessages.Add("MenuItem not path.NotFound");
                    return NotFound(_response);
                }
                _response.IsSuccess = false;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.ErrorsMessages.Add("MenuItem not path.BadRequest");
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("MenuItem not path.Error");
                _logger.LogError(ex.ToString());

                _response.IsSuccess = false;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);
            }
            
        }

    }

}   
