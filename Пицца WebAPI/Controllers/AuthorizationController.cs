using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizza_WebAPI.Data;
using Pizza_WebAPI.Models;
using Pizza_WebAPI.Models.DTO.Authorization;
using Pizza_WebAPI.Repository.AuthenticationServices;
using Pizza_WebAPI.Utillity;

namespace Pizza_WebAPI.Controllers
{
    [Route("api/Authorization")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {

        readonly ApplicationDbContext _dbContext;
        readonly IAuthServices _authServices;
        readonly APIResponse _response;
        readonly ILogger<OrderController> _logger;


        public AuthorizationController(IAuthServices authServices, ILogger<OrderController> logger, ApplicationDbContext dbContext)
        {
            
            _authServices= authServices;
            _dbContext = dbContext;
            _response = new APIResponse();
            _logger = logger;
        }


        [Authorize(Roles =StaticDetails.DirectorRole)]
        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<APIResponse> GetAlRoles()
        {
            try
            {

                var allRoles= _dbContext.Roles.ToList();


                //var allRoles = new RolesDTO();
                //allRoles.RoleList = _dbContext.Roles.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                //{
                //    Text = u.Name,
                //    Value = u.Id
                //});

                //if (allRoles.RoleList != null)
                //{
                //    _logger.LogInformation("Successfully get all Workers");
                //    _response.StatusCode = System.Net.HttpStatusCode.OK;
                //    _response.IsSuccess = true;
                //    _response.Result = allRoles;
                //    return Ok(_response);
                //}
                if (allRoles!=null)
                {
                    _logger.LogInformation("Successfully get all Workers");
                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _response.Result = allRoles;
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





        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Login(LoginRequestDTO model)
        {
            try
            {
                var loginResponse = await _authServices.LoginAsync(model);
                if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
                {
                    _logger.LogInformation("Error. BadRequest");

                    _response.IsSuccess = false;
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.ErrorsMessages.Add("Error. BadRequest");
                    return BadRequest(_response);
                }

                else
                {
                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _response.Result = loginResponse;
                    return Ok(_response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error");
                _logger.LogError(ex.ToString());

                _response.IsSuccess = false;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);
            }

        }







        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Register([FromBody] RegistrationRequestDTo model)
        {
            try
            {
                var IsUniqumName = _authServices.IsUniqueUser(model.UserName);

                if (!IsUniqumName)
                {
                    _logger.LogInformation("Error. Not Uniqum Name");

                    _response.IsSuccess = false;
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.ErrorsMessages.Add("Error. Not Uniqum Name");
                    return BadRequest(_response);
                }

                var user = await _authServices.RegisterAsync(model);
                if (user == null)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages.Add("Error wile registering");
                    return BadRequest(_response);
                }
                else
                {
                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    return Ok(_response);
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error");
                _logger.LogError(ex.ToString());

                _response.IsSuccess = false;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);
            }

        }


    }
}
