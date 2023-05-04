using AutoMapper;
using Director.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pizza_WebAPI.Models;
using Pizza_WebAPI.Models.DTO;
using Pizza_WebAPI.Repository.UnitOfWork;
using Pizza_WebAPI.Utillity;

namespace Pizza_WebAPI.Controllers
{
    [Authorize]
    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        protected APIResponse _response;

        readonly IUnitOfWork _dbunitOfWork;
        readonly IMapper _mapper;
        readonly ILogger<OrderController> _logger;

        public OrderViewModels modelOrderView;
        public List<OrderViewModels> listOrderViewModels;

        public List<OrderDetails>? OrderDetailsList { get; set; }
        public OrderDetails modelOrderDetails { get; set; } 
        public OrderHeader? modelOrderHeader { get; set; }
        public Client? modelClient { get; set; }

        public StatusOrder modelStatusOrder;




        public OrderController(ILogger<OrderController> logger, IMapper mapper, IUnitOfWork dbunitOfWork)
        {

            _logger = logger;
            _mapper = mapper;
            _dbunitOfWork = dbunitOfWork;
            modelOrderView = new OrderViewModels();
            listOrderViewModels = new List<OrderViewModels>();
            OrderDetailsList = new List<OrderDetails>();
            modelOrderDetails= new OrderDetails();
            _response = new APIResponse();
            modelOrderHeader = new OrderHeader();
            modelClient = new Client();
            modelStatusOrder = new StatusOrder();

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
                var listOrderHeaderFromDb= _dbunitOfWork.OrderHeader.GetAll(includeProperties: "Manager,Сook,Client").ToList();

                foreach (var item in listOrderHeaderFromDb)
                {
                    
                    listOrderViewModels.Add(new OrderViewModels()
                    {
                        OrderHeader=_mapper.Map<OrderHeaderDTO>(item),
                        OrderDetailsList= _mapper.Map<List<OrderDetailsDTO>>(_dbunitOfWork.OrderDetails.GetAll(u => u.OrderHeaderId == item.Id,
                                                                                            includeProperties: "OrderHeader,MenuItem").ToList()),
                    TotalSumma =item.TotalSumma,
                        DateCreatedOrder=item.DateCreatedOrder,

                    });
                }


                if (listOrderViewModels.Count() > 0)
                {
                    _logger.LogInformation("Successfully get all Order");
                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _response.Result = listOrderViewModels;
                    return Ok(_response);
                }
                _logger.LogInformation("The database does not contain Order");
                _response.ErrorsMessages.Add("The database does not contain Order");
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Order not get all.Error");
                _logger.LogError(ex.ToString());
                _response.ErrorsMessages.Add(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest();
            }

        }





        [Authorize(Roles = StaticDetails.DirectorRole)]
        [HttpGet("ForDirector")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<APIResponse> GetAllForDirector()
        {
            try
            {
                var listOrderHeaderFromDb = _dbunitOfWork.OrderHeader.GetAll(includeProperties: "Manager,Сook,Client").ToList();
                var listOrdersForDirector = new List<OrderViewForDirectorDTO>();

                if (listOrderHeaderFromDb.Count>0)
                {
                    foreach (var item in listOrderHeaderFromDb)
                    {
                        listOrderViewModels.Add(new OrderViewModels()
                        {
                            OrderHeader = _mapper.Map<OrderHeaderDTO>(item),
                            OrderDetailsList = _mapper.Map<List<OrderDetailsDTO>>(_dbunitOfWork.OrderDetails.GetAll(u => u.OrderHeaderId == item.Id,
                                                                                                includeProperties: "OrderHeader,MenuItem").ToList()),
                            TotalSumma = item.TotalSumma,
                            DateCreatedOrder = item.DateCreatedOrder,

                        });
                    }

                    
                    foreach (var item in listOrderViewModels)
                    {
                        var listMenu =new List<string>();
                        for (int i = 0; i < item.OrderDetailsList.Count; i++)
                        {
                            listMenu.Add(item.OrderDetailsList[i].MenuName);
                        }
                        var orderForDirector = new OrderViewForDirectorDTO()
                        {
                            IdOrderHeader=item.OrderHeader.Id,
                            NumberOrder=item.OrderHeader.NumberOrder,
                            DateCreateOrder=item.DateCreatedOrder,
                            MenuName=listMenu,
                            Manager= Convert.ToString(item.OrderHeader.Manager),
                            Cook= Convert.ToString(item.OrderHeader.Сook),
                            OrderStatus=item.OrderHeader.OrderStatus,
                        };
                        listOrdersForDirector.Add(orderForDirector);
                    }
                }
                

                if (listOrdersForDirector.Count() > 0)
                {
                    _logger.LogInformation("Successfully get all Order");
                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _response.Result = listOrdersForDirector;
                    return Ok(_response);
                }
                _logger.LogInformation("The database does not contain Order");
                _response.ErrorsMessages.Add("The database does not contain Order");
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Order not get all.Error");
                _logger.LogError(ex.ToString());
                _response.ErrorsMessages.Add(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest();
            }

        }


        #region //
        //[HttpGet("namePizza", Name = "GetAllNamePizza")]
        //[ResponseCache(CacheProfileName = "Default30")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public ActionResult<APIResponse> GetAllNamePizza(string namePizza)
        //{
        //    try
        //    {
        //        PizzaList = _dbunitOfWork.Pizza.GetAll(includeProperties: "SizePizza,CategoryPizza");


        //        if (PizzaList.Count() > 0)
        //        {
        //            var result = new List<Pizza>();

        //            foreach (var item in PizzaList)
        //            {
        //                if (item.Name == namePizza)
        //                {
        //                    result.Add(item);
        //                }
        //            }

        //            if (result.Count > 0)
        //            {
        //                _logger.LogInformation("Successfully get all Pizza");
        //                _response.StatusCode = System.Net.HttpStatusCode.OK;
        //                _response.IsSuccess = true;
        //                _response.Result = result;
        //                return Ok(_response);
        //            }

        //            _logger.LogInformation("The database does not contain Pizza");
        //            _response.ErrorsMessages.Add("The database does not contain Pizza");
        //            _response.StatusCode = System.Net.HttpStatusCode.NotFound;
        //            _response.IsSuccess = false;
        //            return NotFound(_response);

        //        }
        //        _logger.LogInformation("The database does not contain Pizza");
        //        _response.ErrorsMessages.Add("The database does not contain Pizza");
        //        _response.StatusCode = System.Net.HttpStatusCode.NotFound;
        //        _response.IsSuccess = false;
        //        return NotFound(_response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogInformation("Pizza not get all.Error");
        //        _logger.LogError(ex.ToString());
        //        _response.ErrorsMessages.Add(ex.ToString());
        //        _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        return BadRequest();
        //    }

        //}
        #endregion




        [HttpGet("id", Name = "GetOrder")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<APIResponse> GetOne(uint id)
        {
            try
            {
                if (id > 0)
                {
                    modelOrderHeader = _dbunitOfWork.OrderHeader.GetOne(u => u.Id == id, includeProperties: "Manager,Сook,Client");
                    OrderDetailsList= _dbunitOfWork.OrderDetails.GetAll(u => u.OrderHeaderId == id,
                                                                                            includeProperties: "OrderHeader,MenuItem").ToList();



                    modelOrderView.OrderHeader = _mapper.Map<OrderHeaderDTO>(modelOrderHeader);
                    modelOrderView.OrderDetailsList = _mapper.Map<List<OrderDetailsDTO>>(OrderDetailsList);
                    modelOrderView.TotalSumma=modelOrderHeader.TotalSumma;
                    modelOrderView.DateCreatedOrder= modelOrderHeader.DateCreatedOrder;


                    if (modelOrderView.OrderHeader.Id > 0)
                    {
                        _logger.LogInformation("Successfully get Order");
                        _response.StatusCode = System.Net.HttpStatusCode.OK;
                        _response.IsSuccess = true;
                        _response.Result = modelOrderView;
                        return Ok(_response);

                    }
                    else
                    {
                        _logger.LogInformation("The database does not contain this Order");
                        _response.ErrorsMessages.Add("The database does not contain Order");
                        _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                        _response.IsSuccess = false;
                        return NotFound(_response);
                    }
                }
                else
                {
                    _logger.LogInformation("Order not get. Id == 0");
                    _response.ErrorsMessages.Add("Order not get. Id == 0");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Order not get.Error");
                _logger.LogError(ex.ToString());
                _response.ErrorsMessages.Add(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest();
            }
        }



        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<APIResponse> Create(OrderViewModels modelDTO)
        {
            try
            {
                if (modelDTO == null)
                {
                    _logger.LogInformation("Order  not created.Model not valid");
                    _response.ErrorsMessages.Add("Order  not created.Model not valid");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }


                if (modelDTO != null && modelDTO.OrderHeader.Id == 0)
                {
                    if (ModelState.IsValid)
                    {
                        //new Client
                        ClientDTO clientDTO = new ClientDTO()
                        {
                            FirstName = modelDTO.OrderHeader.FirstName,
                            LastName = modelDTO.OrderHeader.LastName,
                            PhoneNumber = modelDTO.OrderHeader.PhoneNumber,
                        };
                        modelClient = _mapper.Map<Client>(clientDTO);
                        var chekingClient = _dbunitOfWork.Client.GetOne(u=>u.FirstName==modelClient.FirstName 
                                                                         && u.LastName==modelClient.LastName 
                                                                         &&  u.PhoneNumber==modelClient.PhoneNumber);

                        // if Client not, create Client
                        if (chekingClient.Id==0)
                        {
                            _dbunitOfWork.Client.Create(modelClient);
                            _dbunitOfWork.Save();
                        }



                        //bind OrderHeader to Client
                        var listClients = _dbunitOfWork.Client.GetAll();
                        foreach (var item in listClients)
                        {
                            if (item.FirstName == modelDTO.OrderHeader.FirstName && item.LastName == modelDTO.OrderHeader.LastName
                                                                           && item.PhoneNumber == modelDTO.OrderHeader.PhoneNumber)
                            {
                                modelOrderHeader.ClientId = item.Id;
                            }
                        }





                        // save OrderHeader in Db
                        var orderHeaderForDb = _mapper.Map<OrderHeader>(modelDTO.OrderHeader);
                        orderHeaderForDb.TotalSumma= modelDTO.TotalSumma;
                        orderHeaderForDb.DateCreatedOrder=modelDTO.DateCreatedOrder;
                        orderHeaderForDb.ClientId = modelOrderHeader.ClientId;
                        _dbunitOfWork.OrderHeader.Create(orderHeaderForDb);
                        _dbunitOfWork.Save();


                        // seach OrderHeader 
                        var listOrderHeaderFromDb =_dbunitOfWork.OrderHeader.GetAll();
                        foreach (var item in listOrderHeaderFromDb)
                        {
                            if (item.NumberOrder==orderHeaderForDb.NumberOrder)
                            {
                                modelOrderHeader = item;
                            }
                        }




                        // Create orderDetails and bind OrderDetails to OrderHeader
                        var listOrderDetailsForDb = _mapper.Map<List<OrderDetails>>(modelDTO.OrderDetailsList);
                        foreach (var item in listOrderDetailsForDb)
                        {
                            item.OrderHeaderId = modelOrderHeader.Id;
                            _dbunitOfWork.OrderDetails.Create(item);
                            _dbunitOfWork.Save();
                        }

                        
                        _dbunitOfWork.OrderHeader.Update(modelOrderHeader, modelOrderHeader.Id);
                        _dbunitOfWork.Save();


                        modelStatusOrder.IsStatus = true;
                        modelStatusOrder.NumberOrder = modelOrderHeader.NumberOrder;
                        modelStatusOrder.Name = modelOrderHeader.FirstName;


                        _logger.LogInformation("Order successfully created");
                        _response.StatusCode = System.Net.HttpStatusCode.OK;
                        _response.IsSuccess = true;
                        _response.Result = modelStatusOrder;
                        return Ok(_response);

                    }
                    else
                    {
                        _logger.LogInformation("Order  not created.Model not valid");
                        _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        _response.ErrorsMessages.Add("Order  not created.Model not valid");
                        return BadRequest(_response);
                    }

                }
                _logger.LogInformation("Order not created . Order is equal to NULL");
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add("Order not created . Order is equal to NULL");
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Order  not created.Error");
                _logger.LogError(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);

            }
        }




        [Authorize(Roles = StaticDetails.OfficeRole)]
        [HttpDelete("id", Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> Delete(uint id)
        {
            try
            {
                modelOrderHeader = _dbunitOfWork.OrderHeader.GetOne(u => u.Id == id);
                OrderDetailsList = _dbunitOfWork.OrderDetails.GetAll().Where(u => u.OrderHeaderId == modelOrderHeader.Id).ToList();

                if (modelOrderHeader.Id > 0)
                {
                    _dbunitOfWork.OrderHeader.Delete(modelOrderHeader);
                    //_dbunitOfWork.Save();

                    foreach (var item in OrderDetailsList)
                    {
                        _dbunitOfWork.OrderDetails.Delete(item);
                        
                    }
					_dbunitOfWork.Save();
					_logger.LogInformation("Order Successfully removed.");
                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    return Ok(_response);
                    
                }
                else
                {
                    _logger.LogInformation("Order not removed. No Pizza");
                    _response.ErrorsMessages.Add("Pizza not removed. No Pizza");
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Order not removed.Error");
                _logger.LogError(ex.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessages.Add(ex.ToString());
                return BadRequest(_response);
            }
        }






        /// <summary>
        /// Update only OrderHeader
        /// </summary>
        /// <param name="modelDTO"></param>
        /// <returns></returns>
        /// 
        [Authorize(Roles = StaticDetails.OfficeRole)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> Update(OrderViewModels modelDTO)
        {
            try
            {
                if (modelDTO.OrderHeader.Id > 0)
                {

                    var orderHeaderForDb=_mapper.Map<OrderHeader>(modelDTO.OrderHeader);

                    //update
                    var IsUpdatemodelOrderHeader = _dbunitOfWork.OrderHeader.Update(orderHeaderForDb, modelDTO.OrderHeader.Id);

                    if (IsUpdatemodelOrderHeader)
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





        /// <summary>
        /// Update only OrderDetalis	
        /// <param name="modelDTO"></param>
        /// <returns></returns>
        /// 
        [Authorize(Roles = StaticDetails.OfficeRole)]
        [HttpPut("id")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<APIResponse> Update(uint id, uint count)
		{
			try
			{
				if (id>0)
				{

                    var IsUpdateOrderDetails = false;
                    
                    modelOrderDetails = _dbunitOfWork.OrderDetails.GetOne(u=>u.Id==id);

                    if (count !=0)
                    {
                        modelOrderDetails.Count = count;
                        IsUpdateOrderDetails = _dbunitOfWork.OrderDetails.Update(modelOrderDetails, id);
                        _dbunitOfWork.Save();
                    }


                    if (count == 0)
                    {
                        _dbunitOfWork.OrderDetails.Delete(modelOrderDetails);
                        
                        _dbunitOfWork.Save();
                        IsUpdateOrderDetails = true;

                    }

                    modelOrderHeader = _dbunitOfWork.OrderHeader.GetOne(u => u.Id == modelOrderDetails.OrderHeaderId);

                    OrderDetailsList =_dbunitOfWork.OrderDetails.GetAll(u => u.OrderHeaderId == modelOrderHeader.Id,
                                                                                            includeProperties: "OrderHeader,MenuItem").ToList();
                    if (OrderDetailsList.Count==0)
                    {
                        _dbunitOfWork.OrderHeader.Delete(modelOrderHeader);
                        _dbunitOfWork.Save();
                    }



                    var total = 0.0;
                    foreach (var item in OrderDetailsList)
                    {
                        total += item.Count * item.Price;
                    }
                    modelOrderHeader.TotalSumma= total;

                    if (IsUpdateOrderDetails)
                    {
                        _dbunitOfWork.Save();
                        
                        modelOrderView.OrderDetailsList = _mapper.Map<List<OrderDetailsDTO>>(OrderDetailsList);
                        modelOrderView.OrderHeader = _mapper.Map<OrderHeaderDTO>(modelOrderHeader);
                        _response.Result = modelOrderView;
                        
                        
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




        /// <summary>
        /// Update only Status Order in OrderHeader	
        /// <param =></param>
        /// <returns></returns>
        /// 
        [Authorize(Roles = StaticDetails.OfficeRole)]
        [HttpPut("status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> Update(string status, uint id,string? idEmployee=null)
        {
            try
            {
                if (id > 0)
                {

                    var IsUpdateOrderHeader = false;
                    

                    modelOrderHeader = _dbunitOfWork.OrderHeader.GetOne(u => u.Id == id);

                    switch (status)
                    {
                        case StaticDetails.StatusAccepted: 
                            modelOrderHeader.OrderStatus=status;
                            break;
                        case StaticDetails.StatusInWork:
                            modelOrderHeader.OrderStatus = status;
                            break;
                        case StaticDetails.StatusReady:
                            modelOrderHeader.OrderStatus = status;
                            break;
                        case StaticDetails.StatusSent:
                            modelOrderHeader.OrderStatus = status;
                            break;
                        default:
                            modelOrderHeader.OrderStatus = null;
                            break;
                    }

                    if (idEmployee != null)
                    {
                        var employee = JsonConvert.DeserializeObject<EmployeeUpdateStatus>(idEmployee);
                        if (employee != null)
                        {
                            switch (employee.JobTitle)
                            {
                                case StaticDetails.CookRole:
                                    modelOrderHeader.СookId = employee.Id;
                                    break;
                                case StaticDetails.ManagerRole:
                                    modelOrderHeader.ManagerId = employee.Id;
                                    break;
                                default:
                                    modelOrderHeader.OrderStatus = null;
                                    break;
                            }
                        }
 
                    }

                    


                    IsUpdateOrderHeader = _dbunitOfWork.OrderHeader.Update(modelOrderHeader, modelOrderHeader.Id);

                    

                    if (IsUpdateOrderHeader)
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


    }
}
