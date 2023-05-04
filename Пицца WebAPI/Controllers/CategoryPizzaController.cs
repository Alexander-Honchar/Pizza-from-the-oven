using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pizza_WebAPI.Models.DTO;
using Pizza_WebAPI.Models;
using Pizza_WebAPI.Repository.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Pizza_WebAPI.Utillity;

namespace Pizza_WebAPI.Controllers
{
    [Route("api/CategoryPizza")]
    [ApiController]
    public class CategoryPizzaController : ControllerBase
    {
        protected APIResponse _response;

        readonly IUnitOfWork _dbunitOfWork;
        readonly IMapper _mapper;
        readonly ILogger<CategoryPizzaController> _logger;


        public IEnumerable<CategoryPizza> CategoryPizzaList;
        public CategoryPizza CategoryPizza;


        public CategoryPizzaController(IUnitOfWork dbunitOfWork, ILogger<CategoryPizzaController> logger, IMapper mapper)
        {
            _dbunitOfWork = dbunitOfWork;
            _logger = logger;
            _mapper = mapper;
            CategoryPizzaList = new List<CategoryPizza>();
            CategoryPizza = new CategoryPizza();
            _response= new APIResponse();

        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> GetAll()
        {
            try
            {
                CategoryPizzaList = _dbunitOfWork.CategoryPizza.GetAll();
                if (CategoryPizzaList.Count() > 0)
                {
                    _logger.LogInformation("CategoryPizza Successfully created.");
                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _response.Result = CategoryPizzaList;
                    return Ok(_response);
                }
                else
                {
                    _logger.LogInformation("In CategoryPizza not content.");
                    _response.StatusCode = System.Net.HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages.Add("In CategoryPizza not content.");
                    return NotFound(_response);
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation("CategoryPizza not created.Error");
                _logger.LogError(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add("CategoryPizza not created.Error");
                return BadRequest(_response);
            }
        }





        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> GetOne(uint id)
        {
            try
            {
                if (id > 0)
                {
                    CategoryPizza = _dbunitOfWork.CategoryPizza.GetOne(u => u.Id == id);
                    if (CategoryPizza.Id > 0)
                    {
                        _logger.LogInformation("Successfully get CategoryPizza");
                        _response.StatusCode = System.Net.HttpStatusCode.OK;
                        _response.IsSuccess = true;
                        _response.Result = CategoryPizza;
                        return Ok(_response);
                    }
                    else
                    {
                        _logger.LogInformation("The database does not contain this CategoryPizza");
                        _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                        _response.IsSuccess = false;
                        _response.ErrorsMessages.Add("The database does not contain this CategoryPizza");
                        return NotFound(_response);
                    }
                }
                else
                {
                    _logger.LogInformation("CategoryPizza not get. Id == 0");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages.Add("CategoryPizza not get. Id == 0");
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("CategoryPizza not get.Error");
                _logger.LogError(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add("CategoryPizza not get.Error");
                return BadRequest(_response);
            }
        }





        [Authorize(Roles = StaticDetails.AdministratorRole)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CategoryPizza> Create(CategoryPizzaDTO modelDTO)
        {
            try
            {
                if (modelDTO == null || modelDTO.Id > 0)
                {
                    _logger.LogInformation("CategoryPizza not created. CategoryPizza not valid");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages.Add("CategoryPizza not created. CategoryPizza not valid");
                    return BadRequest(_response);
                }

                var nameObjFromDb = _dbunitOfWork.CategoryPizza.GetOne(u => u.Name == modelDTO.Name);

                if (modelDTO.Name != null && nameObjFromDb.Name != null && modelDTO.Name == nameObjFromDb.Name)
                {
                    _logger.LogInformation("CategoryPizza not created. Name CategoryPizza not uniqum");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages.Add("CategoryPizza not created. Name CategoryPizza not uniqum");
                    return BadRequest(_response);
                }

                if (modelDTO.Id == 0)
                {
                    if (ModelState.IsValid)
                    {
                        var objForDb = _mapper.Map<CategoryPizza>(modelDTO);
                        _dbunitOfWork.CategoryPizza.Create(objForDb);
                        _dbunitOfWork.Save();
                        _logger.LogInformation("CategoryPizza Successfully created.");
                        _response.StatusCode = System.Net.HttpStatusCode.OK;
                        _response.IsSuccess = true;
                        return Ok(_response);
                    }
                
                }

                _logger.LogInformation("CategoryPizza not created. CategoryPizza not valid");
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add("CategoryPizza not created. CategoryPizza not valid");
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("CategoryPizza not created.Error");
                _logger.LogError(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);
            }

        }





        [Authorize(Roles = StaticDetails.AdministratorRole)]
        [HttpDelete("id", Name = "DeleteCategoryPizza")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(uint id)
        {
            try
            {
                CategoryPizza = _dbunitOfWork.CategoryPizza.GetOne(u => u.Id == id);
                if (CategoryPizza.Id > 0)
                {
                    _dbunitOfWork.CategoryPizza.Delete(CategoryPizza);
                    _dbunitOfWork.Save();
                    _logger.LogInformation("CategoryPizza Successfully removed.");
                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    return Ok(_response);
                }
                else
                {
                    _logger.LogInformation("CategoryPizza not removed. No CategoryPizza");
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages.Add("SizePizza not removed. No SizePizza");
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation("CategoryPizza not removed.Error");
                _logger.LogError(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add("SizePizza not removed.Error");
                return BadRequest(_response);
            }
        }





        [Authorize(Roles = StaticDetails.AdministratorRole)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Update(CategoryPizzaDTO modelDTO)
        {
            try
            {
                if (modelDTO.Id > 0)
                {
                    CategoryPizza = _mapper.Map<CategoryPizza>(modelDTO);


                    var nameObjFromDb = _dbunitOfWork.CategoryPizza.GetOne(u => u.Name == modelDTO.Name);

                    if (modelDTO.Name != null && nameObjFromDb.Name != null && modelDTO.Name == nameObjFromDb.Name)
                    {
                        _logger.LogInformation("CategoryPizza not update. Name CategoryPizza not uniqum");
                        _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        _response.ErrorsMessages.Add("CategoryPizza not update. Name CategoryPizza not uniqum");
                        return BadRequest(_response);
                    }



                    var IsResult = _dbunitOfWork.CategoryPizza.Update(CategoryPizza, modelDTO.Id);
                    if (IsResult)
                    {
                        _dbunitOfWork.Save();
                        _logger.LogInformation("CategoryPizza Successfully update.");
                        _response.StatusCode = System.Net.HttpStatusCode.OK;
                        _response.IsSuccess = true;
                        return Ok(_response);
                    }
                    _logger.LogInformation("CategoryPizza not update. No CategoryPizza");
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages.Add("CategoryPizza not update. No CategoryPizza");
                    return NotFound(_response);

                }


                _logger.LogInformation("CategoryPizza not update. No CategoryPizza");
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add("CategoryPizza not update. No CategoryPizza");
                return NotFound(_response);


            }
            catch (Exception ex)
            {
                _logger.LogInformation("CategoryPizza not update.Error");
                _logger.LogError(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add("CategoryPizza not update.Error");
                return BadRequest(_response);
            }
        }
    }
}

