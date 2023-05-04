using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizza_WebAPI.Models;
using Pizza_WebAPI.Models.DTO;
using Pizza_WebAPI.Repository.UnitOfWork;
using Pizza_WebAPI.Utillity;
using System.Data;

namespace Pizza_WebAPI.Controllers
{
    [Route("api/PizzaKingSize")]
    [ApiController]
    public class PizzaKingSizeController : ControllerBase
    {
        protected APIResponse _response;

        readonly IUnitOfWork _dbunitOfWork;
        readonly IMapper _mapper;
        readonly ILogger<PizzaKingSizeController> _logger;
        public PizzaKingSize modelPizzaKingSize;
        public IEnumerable<PizzaKingSize> PizzaKingSizeList { get; set; }



        public PizzaKingSizeController(ILogger<PizzaKingSizeController> logger, IMapper mapper, IUnitOfWork dbunitOfWork)
        {

            _logger = logger;
            _mapper = mapper;
            _dbunitOfWork = dbunitOfWork;
            modelPizzaKingSize = new PizzaKingSize();
            PizzaKingSizeList = new List<PizzaKingSize>();
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
                PizzaKingSizeList = _dbunitOfWork.PizzaKingSize.GetAll();

                if (PizzaKingSizeList.Count() > 0)
                {
                    _logger.LogInformation("Successfully get all PizzaKingSize");
                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _response.Result = PizzaKingSizeList;
                    return Ok(_response);
                }
                _logger.LogInformation("The database does not contain PizzaKingSize");
                _response.ErrorsMessages.Add("The database does not contain PizzaKingSize");
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("PizzaKingSize not get all.Error");
                _logger.LogError(ex.ToString());
                _response.ErrorsMessages.Add(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest();
            }

        }





        [HttpGet("id", Name = "GetOnePizzaKingSize")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<APIResponse> GetOne(uint id)
        {
            try
            {
                if (id > 0)
                {
                    modelPizzaKingSize = _dbunitOfWork.PizzaKingSize.GetOne(u => u.Id == id);
                    if (modelPizzaKingSize.Id > 0)
                    {
                        _logger.LogInformation("Successfully get PizzaKingSize");
                        _response.StatusCode = System.Net.HttpStatusCode.OK;
                        _response.IsSuccess = true;
                        _response.Result = modelPizzaKingSize;
                        return Ok(_response);

                    }
                    else
                    {
                        _logger.LogInformation("The database does not contain this PizzaKingSize");
                        _response.ErrorsMessages.Add("The database does not contain PizzaKingSize");
                        _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                        _response.IsSuccess = false;
                        return NotFound(_response);
                    }
                }
                else
                {
                    _logger.LogInformation("PizzaKingSize not get. Id == 0");
                    _response.ErrorsMessages.Add("PizzaKingSize not get. Id == 0");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("PizzaKingSize not get.Error");
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
        public ActionResult<APIResponse> Create(PizzaKingSizeDTO modelDTO)
        {
            try
            {
                if (modelDTO == null)
                {
                    _logger.LogInformation("PizzaKingSize  not created.Model not valid");
                    _response.ErrorsMessages.Add("PizzaKingSize  not created.Model not valid");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }

                var nameObjFromDb = _dbunitOfWork.Pizza.GetOne(u => u.Name == modelDTO.Name);

                if (modelDTO.Name != null && nameObjFromDb.Name != null && modelDTO.Name == nameObjFromDb.Name)
                {
                    _logger.LogInformation("PizzaKingSize not created. Name PizzaKingSize not uniqum");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages.Add("PizzaKingSize not created. Name PizzaKingSize not uniqum");
                    return BadRequest(_response);
                }


                if (modelDTO != null && modelDTO.Id == 0)
                {
                    if (ModelState.IsValid)
                    {
                        modelPizzaKingSize = _mapper.Map<PizzaKingSize>(modelDTO);
                        _dbunitOfWork.PizzaKingSize.Create(modelPizzaKingSize);
                        _dbunitOfWork.Save();
                        _logger.LogInformation("PizzaKingSize successfully created");
                        _response.StatusCode = System.Net.HttpStatusCode.OK;
                        _response.IsSuccess = true;
                        _response.Result = modelPizzaKingSize;
                        return Ok(_response);

                    }
                    else
                    {
                        _logger.LogInformation("PizzaKingSize  not created.Model not valid");
                        _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        _response.ErrorsMessages.Add("PizzaKingSize  not created.Model not valid");
                        return BadRequest(_response);
                    }

                }
                _logger.LogInformation("PizzaKingSize not created . lPizza is equal to NULL");
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add("PizzaKingSize not created . lPizza is equal to NULL");
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("PizzaKingSize  not created.Error");
                _logger.LogError(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);

            }
        }




        [Authorize(Roles = StaticDetails.AdministratorRole)]
        [HttpDelete("id", Name = "DeletePizzaKingSize")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> Delete(uint id)
        {
            try
            {
                modelPizzaKingSize = _dbunitOfWork.PizzaKingSize.GetOne(u => u.Id == id);
                if (modelPizzaKingSize.Id > 0)
                {
                    _dbunitOfWork.PizzaKingSize.Delete(modelPizzaKingSize);
                    _dbunitOfWork.Save();
                    _logger.LogInformation("PizzaKingSize Successfully removed.");
                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    return Ok(_response);
                    ;
                }
                else
                {
                    _logger.LogInformation("PizzaKingSize not removed. No Pizza");
                    _response.ErrorsMessages.Add("PizzaKingSize not removed. No Pizza");
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation("PizzaKingSize not removed.Error");
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
        public ActionResult<APIResponse> Update(PizzaKingSizeDTO modelDTO)
        {
            try
            {
                if (modelDTO.Id > 0)
                {
                    modelPizzaKingSize = _mapper.Map<PizzaKingSize>(modelDTO);

                    var IsResult = _dbunitOfWork.PizzaKingSize.Update(modelPizzaKingSize, modelDTO.Id);
                    if (IsResult)
                    {
                        _dbunitOfWork.Save();
                        _logger.LogInformation("PizzaKingSize Successfully update.");
                        _response.StatusCode = System.Net.HttpStatusCode.OK;
                        _response.IsSuccess = true;
                        return Ok(_response);
                    }
                    _logger.LogInformation("PizzaKingSize not update. No PizzaKingSizea");
                    _response.ErrorsMessages.Add("PizzaKingSize not update. No PizzaKingSize");
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);

                }


                _logger.LogInformation("PizzaKingSize not update. No PizzaKingSize");
                _response.ErrorsMessages.Add("PizzaKingSize not update. No PizzaKingSize");
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);


            }
            catch (Exception ex)
            {
                _logger.LogInformation("PizzaKingSize not update.Error");
                _logger.LogError(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);
            }
        }
    }
}
