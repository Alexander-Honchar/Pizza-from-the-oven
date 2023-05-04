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
    [Route("api/Pizza")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        protected APIResponse _response;

        readonly IUnitOfWork _dbunitOfWork;
        readonly IMapper _mapper;
        readonly ILogger<PizzaController> _logger;
        public  Pizza modelPizza;
        public IEnumerable<Pizza> PizzaList { get; set; }



        public PizzaController(ILogger<PizzaController> logger, IMapper mapper,IUnitOfWork dbunitOfWork)
        {

            _logger = logger;
            _mapper = mapper;
            _dbunitOfWork = dbunitOfWork;
            modelPizza = new Pizza();
            PizzaList = new List<Pizza>();
            _response= new APIResponse();   

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
                PizzaList = _dbunitOfWork.Pizza.GetAll(includeProperties: "SizePizza,CategoryPizza");

                if (PizzaList.Count()>0)
                {
                    _logger.LogInformation("Successfully get all Pizza");
                    _response.StatusCode=System.Net.HttpStatusCode.OK;
                    _response.IsSuccess=true;
                    _response.Result=PizzaList;
                    return Ok(_response);
                }
                _logger.LogInformation("The database does not contain Pizza");
                _response.ErrorsMessages.Add("The database does not contain Pizza");
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess=false;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Pizza not get all.Error");
                _logger.LogError(ex.ToString());
				_response.ErrorsMessages.Add(ex.ToString());
				_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
				_response.IsSuccess = false;
				return BadRequest();
            }
            
        }




		[HttpGet("nameCategory", Name = "GetAllNamePizza")]
		[ResponseCache(CacheProfileName = "Default30")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public ActionResult<APIResponse> GetAllNamePizza(string nameCategory)
		{
			try
			{
				PizzaList = _dbunitOfWork.Pizza.GetAll(includeProperties: "SizePizza,CategoryPizza");


				if (PizzaList.Count() > 0)
				{
					var result = new List<Pizza>();

					foreach (var item in PizzaList)
					{
						if (item.Name == nameCategory)
						{
							result.Add(item);
						}
					}

                    if (result.Count>0)
                    {
						_logger.LogInformation("Successfully get all Pizza");
						_response.StatusCode = System.Net.HttpStatusCode.OK;
						_response.IsSuccess = true;
						_response.Result = result;
						return Ok(_response);
					}

					_logger.LogInformation("The database does not contain Pizza");
					_response.ErrorsMessages.Add("The database does not contain Pizza");
					_response.StatusCode = System.Net.HttpStatusCode.NotFound;
					_response.IsSuccess = false;
					return NotFound(_response);

				}
				_logger.LogInformation("The database does not contain Pizza");
				_response.ErrorsMessages.Add("The database does not contain Pizza");
				_response.StatusCode = System.Net.HttpStatusCode.NotFound;
				_response.IsSuccess = false;
				return NotFound(_response);
			}
			catch (Exception ex)
			{
				_logger.LogInformation("Pizza not get all.Error");
				_logger.LogError(ex.ToString());
				_response.ErrorsMessages.Add(ex.ToString());
				_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
				_response.IsSuccess = false;
				return BadRequest();
			}

		}





		[HttpGet("id", Name = "GetOnePizza")]
		[ResponseCache(CacheProfileName = "Default30")]
		[ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<APIResponse> GetOne(uint id)
        {
            try
            {
                if (id>0)
                {
                    modelPizza = _dbunitOfWork.Pizza.GetOne(u=>u.Id==id,includeProperties: "SizePizza,CategoryPizza");
                    if (modelPizza.Id>0)
                    {
                        _logger.LogInformation("Successfully get Pizza");
						_response.StatusCode = System.Net.HttpStatusCode.OK;
						_response.IsSuccess = true;
						_response.Result = modelPizza;
						return Ok(_response);
						
                    }
                    else
                    {
                        _logger.LogInformation("The database does not contain this Pizza");
						_response.ErrorsMessages.Add("The database does not contain Pizza");
						_response.StatusCode = System.Net.HttpStatusCode.NotFound;
						_response.IsSuccess = false;
						return NotFound(_response);
					}
                }
                else
                {
                    _logger.LogInformation("Pizza not get. Id == 0");
					_response.ErrorsMessages.Add("Pizza not get. Id == 0");
					_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					return BadRequest();
				}
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Pizza not get.Error");
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
        public ActionResult<APIResponse> Create(PizzaDTO modelDTO) 
        {
            try
            {
                if (modelDTO==null)
                {
                    _logger.LogInformation("Pizza  not created.Model not valid");
					_response.ErrorsMessages.Add("Pizza  not created.Model not valid");
					_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					return BadRequest();
				}



				if (modelDTO != null && modelDTO.Id==0)
                {
                    if (ModelState.IsValid)
                    {
                        modelPizza = _mapper.Map<Pizza>(modelDTO);
                        _dbunitOfWork.Pizza.Create(modelPizza);
                        _dbunitOfWork.Save();
                        _logger.LogInformation("Pizza successfully created");
						_response.StatusCode = System.Net.HttpStatusCode.OK;
						_response.IsSuccess = true;
						_response.Result = modelPizza;
						return Ok(_response);
						
                    }
                    else
                    {
                        _logger.LogInformation("Pizza  not created.Model not valid");
						_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
						_response.IsSuccess = false;
						_response.ErrorsMessages.Add("Pizza  not created.Model not valid");
						return BadRequest(_response);
					}
                      
                }
                _logger.LogInformation("Pizza not created . lPizza is equal to NULL");
				_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
				_response.IsSuccess = false;
				_response.ErrorsMessages.Add("Pizza not created . lPizza is equal to NULL");
				return BadRequest(_response);
			}
            catch (Exception ex)
            {
                _logger.LogInformation("Pizza  not created.Error");
                _logger.LogError(ex.ToString());
				_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
				_response.IsSuccess = false;
				_response.ErrorsMessages.Add(ex.ToString());
				return BadRequest(_response);

			}
        }




        [Authorize(Roles = StaticDetails.AdministratorRole)]
        [HttpDelete("id", Name = "DeletePizza")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> Delete(uint id)
        {
            try
            {
                modelPizza = _dbunitOfWork.Pizza.GetOne(u => u.Id == id);
                if (modelPizza.Id > 0)
                {
                    _dbunitOfWork.Pizza.Delete(modelPizza);
                    _dbunitOfWork.Save();
                    _logger.LogInformation("Pizza Successfully removed.");
					_response.StatusCode = System.Net.HttpStatusCode.OK;
					_response.IsSuccess = true;
					return Ok(_response);
					;
				}
                else
                {
                    _logger.LogInformation("Pizza not removed. No Pizza");
					_response.ErrorsMessages.Add("Pizza not removed. No Pizza");
					_response.StatusCode = System.Net.HttpStatusCode.NotFound;
					_response.IsSuccess = false;
					return NotFound(_response);
				}

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Pizza not removed.Error");
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
        public ActionResult<APIResponse> Update(PizzaDTO modelDTO)
        {
            try
            {
                if (modelDTO.Id > 0)
                {
                    modelPizza = _mapper.Map<Pizza>(modelDTO);

					var IsResult = _dbunitOfWork.Pizza.Update(modelPizza, modelDTO.Id);
                    if (IsResult)
                    {
                        _dbunitOfWork.Save();
                        _logger.LogInformation("Pizza Successfully update.");
						_response.StatusCode = System.Net.HttpStatusCode.OK;
						_response.IsSuccess = true;
						return Ok(_response);
					}
                    _logger.LogInformation("Pizza not update. No Pizza");
					_response.ErrorsMessages.Add("Pizza not update. No Pizza");
					_response.StatusCode = System.Net.HttpStatusCode.NotFound;
					_response.IsSuccess = false;
					return NotFound(_response);

				}


                _logger.LogInformation("Pizza not update. No Pizza");
				_response.ErrorsMessages.Add("Pizza not update. No Pizza");
				_response.StatusCode = System.Net.HttpStatusCode.NotFound;
				_response.IsSuccess = false;
				return NotFound(_response);


			}
            catch (Exception ex)
            {
                _logger.LogInformation("Pizza not update.Error");
                _logger.LogError(ex.ToString());
				_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
				_response.IsSuccess = false;
				_response.ErrorsMessages.Add(ex.ToString());
				return BadRequest(_response);
			}
        }

    }
}
