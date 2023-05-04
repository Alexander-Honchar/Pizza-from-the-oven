using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizza_WebAPI;
using Pizza_WebAPI.Models;
using Pizza_WebAPI.Models.DTO;
using Pizza_WebAPI.Repository.UnitOfWork;
using Pizza_WebAPI.Utillity;
using System.Data;
//using CategoryMenu = Pizza_WebAPI.Models.CategoryMenu;

namespace Pizza_WebAPI.Controllers
{
    [Route("api/CategoryMenu")]
    [ApiController]
    public class CategoryMenuController : Controller
    {
        protected APIResponse _response;

        readonly IUnitOfWork _dbunitOfWork;
        readonly IMapper _mapper;
        readonly ILogger<CategoryMenuController> _logger;
        public CategoryMenu modelCategoryMenu;
        public IEnumerable<CategoryMenu> CategoryMenuList { get; set; }



        public CategoryMenuController(ILogger<CategoryMenuController> logger, IMapper mapper, IUnitOfWork dbunitOfWork)
        {

            _logger = logger;
            _mapper = mapper;
            _dbunitOfWork = dbunitOfWork;
            modelCategoryMenu = new CategoryMenu();
            CategoryMenuList = new List<CategoryMenu>();
            _response = new APIResponse();

        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<APIResponse> GetAll()
        {
            try
            {
                CategoryMenuList = _dbunitOfWork.CategoryMenu.GetAll();

                if (CategoryMenuList.Count() > 0)
                {
                    _logger.LogInformation("Successfully get all CategoryMenu");
                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _response.Result = CategoryMenuList;
                    return Ok(_response);
                }
                _logger.LogInformation("The database does not contain CategoryMenu");
                _response.ErrorsMessages.Add("The database does not contain CategoryMenu");
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("CategoryMenu not get all.Error");
                _logger.LogError(ex.ToString());
                _response.ErrorsMessages.Add(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest();
            }

        }





        [HttpGet("id", Name = "GetOneCategoryMenu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<APIResponse> GetOne(uint id)
        {
            try
            {
                if (id > 0)
                {
                    modelCategoryMenu = _dbunitOfWork.CategoryMenu.GetOne(u => u.Id == id);
                    if (modelCategoryMenu.Id > 0)
                    {
                        _logger.LogInformation("Successfully get CategoryMenu");
                        _response.StatusCode = System.Net.HttpStatusCode.OK;
                        _response.IsSuccess = true;
                        _response.Result = modelCategoryMenu;
                        return Ok(_response);

                    }
                    else
                    {
                        _logger.LogInformation("The database does not contain this CategoryMenu");
                        _response.ErrorsMessages.Add("The database does not contain CategoryMenu");
                        _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                        _response.IsSuccess = false;
                        return NotFound(_response);
                    }
                }
                else
                {
                    _logger.LogInformation("CategoryMenu not get. Id == 0");
                    _response.ErrorsMessages.Add("CategoryMenu not get. Id == 0");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("CategoryMenu not get.Error");
                _logger.LogError(ex.ToString());
                _response.ErrorsMessages.Add(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest();
            }
        }




        [Authorize(Roles = StaticDetails.AdministratorRole)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<APIResponse> Create(CategoryMenuDTO modelDTO)
        {
            try
            {
                if (modelDTO == null)
                {
                    _logger.LogInformation("CategoryMenu  not created.Model not valid");
                    _response.ErrorsMessages.Add("CategoryMenu  not created.Model not valid");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }

                var nameObjFromDb = _dbunitOfWork.CategoryMenu.GetOne(u => u.NameCategory == modelDTO.NameCategory);

                if (modelDTO.NameCategory != null && nameObjFromDb.NameCategory != null && modelDTO.NameCategory == nameObjFromDb.NameCategory)
                {
                    _logger.LogInformation("CategoryMenu not created. Name CategoryMenu not uniqum");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages.Add("CategoryMenu not created. Name CategoryMenu not uniqum");
                    return BadRequest(_response);
                }


                if (modelDTO != null && modelDTO.Id == 0)
                {
                    if (ModelState.IsValid)
                    {
                        modelCategoryMenu = _mapper.Map<Models.CategoryMenu>(modelDTO);
                        _dbunitOfWork.CategoryMenu.Create(modelCategoryMenu);
                        _dbunitOfWork.Save();
                        _logger.LogInformation("CategoryMenu successfully created");
                        _response.StatusCode = System.Net.HttpStatusCode.OK;
                        _response.IsSuccess = true;
                        _response.Result = modelCategoryMenu;
                        return Ok(_response);

                    }
                    else
                    {
                        _logger.LogInformation("CategoryMenu not created.Model not valid");
                        _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        _response.ErrorsMessages.Add("CategoryMenu  not created.Model not valid");
                        return BadRequest(_response);
                    }

                }
                _logger.LogInformation("CategoryMenu not created . CategoryMenuis equal to NULL");
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add("CategoryMenu not created . CategoryMenu is equal to NULL");
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("CategoryMenu  not created.Error");
                _logger.LogError(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);

            }
        }




        [Authorize(Roles = StaticDetails.AdministratorRole)]
        [HttpDelete("id", Name = "DeleteCategoryMenu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> Delete(uint id)
        {
            try
            {
                modelCategoryMenu = _dbunitOfWork.CategoryMenu.GetOne(u => u.Id == id);
                if (modelCategoryMenu.Id > 0)
                {
                    _dbunitOfWork.CategoryMenu.Delete(modelCategoryMenu);
                    _dbunitOfWork.Save();
                    _logger.LogInformation("CategoryMenu Successfully removed.");
                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    return Ok(_response);
                    ;
                }
                else
                {
                    _logger.LogInformation("CategoryMenu not removed. No CategoryMenu");
                    _response.ErrorsMessages.Add("CategoryMenu not removed. No CategoryMenu");
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation("CategoryMenu not removed.Error");
                _logger.LogError(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);
            }
        }





        [Authorize(Roles = StaticDetails.AdministratorRole)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> Update(CategoryMenuDTO modelDTO)
        {
            try
            {
                if (modelDTO.Id > 0)
                {
                    modelCategoryMenu = _mapper.Map<Models.CategoryMenu>(modelDTO);

                    var IsResult = _dbunitOfWork.CategoryMenu.Update(modelCategoryMenu, modelDTO.Id);
                    if (IsResult)
                    {
                        _dbunitOfWork.Save();
                        _logger.LogInformation("CategoryMenu Successfully update.");
                        _response.StatusCode = System.Net.HttpStatusCode.OK;
                        _response.IsSuccess = true;
                        return Ok(_response);
                    }
                    _logger.LogInformation("CategoryMenu not update. No CategoryMenu");
                    _response.ErrorsMessages.Add("CategoryMenu not update. No CategoryMenu");
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);

                }


                _logger.LogInformation("CategoryMenu not update. No CategoryMenu");
                _response.ErrorsMessages.Add("CategoryMenu not update. No CategoryMenu");
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);


            }
            catch (Exception ex)
            {
                _logger.LogInformation("CategoryMenu not update.Error");
                _logger.LogError(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);
            }
        }
    }
}
