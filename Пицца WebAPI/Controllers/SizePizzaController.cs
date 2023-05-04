using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizza_WebAPI.Data;
using Pizza_WebAPI.Models;
using Pizza_WebAPI.Models.DTO;
using Pizza_WebAPI.Repository.UnitOfWork;
using Pizza_WebAPI.Utillity;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Pizza_WebAPI.Controllers
{
    [Route("api/SizePizza")]
    [ApiController]
    public class SizePizzaController : ControllerBase
    {
        protected APIResponse _response;

        readonly IUnitOfWork _dbunitOfWork;
        readonly IMapper _mapper;
        readonly ILogger<SizePizzaController> _logger;

        
        public IEnumerable<SizePizza> SizePizzaList;
        public SizePizza SizePizza;


        public SizePizzaController(IUnitOfWork dbunitOfWork, ILogger<SizePizzaController> logger, IMapper mapper)
        {
            _dbunitOfWork = dbunitOfWork;
            _logger = logger;
            _mapper = mapper;
            SizePizzaList = new List<SizePizza>();
            SizePizza= new SizePizza();
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
                SizePizzaList = _dbunitOfWork.SizePizza.GetAll();
                if (SizePizzaList.Count()>0)
                {
                    _logger.LogInformation("SizePizza Successfully created.");
                    _response.StatusCode=System.Net.HttpStatusCode.OK;
                    _response.IsSuccess=true;
                    _response.Result=SizePizzaList;
                    return Ok(_response);
                }
                else
                {
                    _logger.LogInformation("In SizePizza not content.");
                    _response.StatusCode = System.Net.HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages.Add("In SizePizza not content.");
                    return NotFound(_response);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogInformation("SizePizza not created.Error");
                _logger.LogError(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add("SizePizza not created.Error");
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
					SizePizza = _dbunitOfWork.SizePizza.GetOne(u => u.Id == id);
					if (SizePizza.Id > 0)
					{
						_logger.LogInformation("Successfully get SizePizza");
						_response.StatusCode = System.Net.HttpStatusCode.OK;
						_response.IsSuccess = true;
						_response.Result = SizePizza;
						return Ok(_response);
					}
					else
					{
						_logger.LogInformation("The database does not contain this SizePizza");
						_response.StatusCode = System.Net.HttpStatusCode.NotFound;
						_response.IsSuccess = false;
						_response.ErrorsMessages.Add("The database does not contain this SizePizza");
						return NotFound(_response);
					}
				}
				else
				{
					_logger.LogInformation("SizePizza not get. Id == 0");
					_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					_response.ErrorsMessages.Add("SizePizza not get. Id == 0");
					return BadRequest(_response);
				}
			}
			catch (Exception ex)
			{
				_logger.LogInformation("SizePizza not get.Error");
				_logger.LogError(ex.ToString());
				_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
				_response.IsSuccess = false;
				_response.ErrorsMessages.Add("SizePizza not get.Error");
                return BadRequest(_response);
            }
        }






        [Authorize(Roles = StaticDetails.AdministratorRole)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<APIResponse> Create([FromBody] SizePizzaDTO modelDTO)
        {
            try
            {
                if (modelDTO == null || modelDTO.Id>0)
                {
                    _logger.LogInformation("SizePizza not created. SizePizza not valid");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages.Add("SizePizza not created. SizePizza not valid");
                    return BadRequest(_response);
                }

                var nameObjFromDb = _dbunitOfWork.SizePizza.GetOne(u => u.Size == modelDTO.Size);

                if (modelDTO.Size != null && nameObjFromDb.Size != null && modelDTO.Size == nameObjFromDb.Size)
                {
                    _logger.LogInformation("SizePizza not created. Name SizePizza not uniqum");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages.Add("SizePizza not created. Name SizePizza not uniqum");
                    return BadRequest(_response);
                }

                if (modelDTO.Id==0)
                {
                    if (ModelState.IsValid)
                    {
                        var objForDb = _mapper.Map<SizePizza>(modelDTO);
                        _dbunitOfWork.SizePizza.Create(objForDb);
                        _dbunitOfWork.Save();
                        _logger.LogInformation("SizePizza Successfully created.");
                        _response.StatusCode = System.Net.HttpStatusCode.OK;
                        _response.IsSuccess = true;
                        return Ok(_response);
                    }
                }
                
                _logger.LogInformation("SizePizza not created. SizePizza not valid");
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add("SizePizza not created. SizePizza not valid");
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("SizePizza not created.Error");
                _logger.LogError(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);
            }

        }





        [Authorize(Roles = StaticDetails.AdministratorRole)]
        [HttpDelete("id", Name = "Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> Delete(uint id)
        {
            try
            {
                SizePizza = _dbunitOfWork.SizePizza.GetOne(u=>u.Id==id);

                if (SizePizza.Id > 0)
                {
                    _dbunitOfWork.SizePizza.Delete(SizePizza);
                    _dbunitOfWork.Save();
                    _logger.LogInformation("SizePizza Successfully removed.");
					_response.StatusCode = System.Net.HttpStatusCode.OK;
					_response.IsSuccess = true;
					return Ok(_response);
                }
                else
                {
                    _logger.LogInformation("SizePizza not removed. No SizePizza");
					_response.StatusCode = System.Net.HttpStatusCode.NotFound;
					_response.IsSuccess = false;
					_response.ErrorsMessages.Add("SizePizza not removed. No SizePizza");
					return NotFound();
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation("SizePizza not removed.Error");
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
        public ActionResult<APIResponse> Update(SizePizzaDTO modelDTO)
        {
            try
            {
                if (modelDTO.Id > 0)
                {
                    SizePizza = _mapper.Map<SizePizza>(modelDTO);

                    var nameObjFromDb = _dbunitOfWork.SizePizza.GetOne(u => u.Size == modelDTO.Size);

                    if (modelDTO.Size != null && nameObjFromDb.Size != null && modelDTO.Size == nameObjFromDb.Size)
                    {
                        _logger.LogInformation("SizePizza not update. Name SizePizza not uniqum");
                        _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        _response.ErrorsMessages.Add("SizePizza not update. Name SizePizza not uniqum");
                        return BadRequest(_response);
                    }


                    var IsResult = _dbunitOfWork.SizePizza.Update(SizePizza, modelDTO.Id);
                    if (IsResult)
                    {
                        _dbunitOfWork.Save();
                        _logger.LogInformation("SizePizza Successfully update.");
						_response.StatusCode = System.Net.HttpStatusCode.OK;
						_response.IsSuccess = true;
						return Ok(_response);
                    }
                    _logger.LogInformation("SizePizza not update. No SizePizza");
					_response.StatusCode = System.Net.HttpStatusCode.NotFound;
					_response.IsSuccess = false;
					_response.ErrorsMessages.Add("SizePizza not update. No SizePizza");
					return NotFound(_response);

                }

                
                _logger.LogInformation("SizePizza not update. No SizePizza");
				_response.StatusCode = System.Net.HttpStatusCode.NotFound;
				_response.IsSuccess = false;
				_response.ErrorsMessages.Add("SizePizza not update. No SizePizza");
				return NotFound(_response);



			}
			catch (Exception ex)
            {
                _logger.LogInformation("SizePizza not update.Error");
                _logger.LogError(ex.ToString());
				_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
				_response.ErrorsMessages.Add("SizePizza not update.Error");
				return BadRequest(_response);
			}
        }
    }
}
