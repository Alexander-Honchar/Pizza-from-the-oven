using Director.Models;
using Director.Services.Metods;
using Director.Utillity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Director.Controllers
{
    [Authorize(Roles = StaticDetails.DirectorRole)]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        public IMetods _metods;
        List<OrderViewForDirectorDTO> listOrderViewModels;

        public HomeController(IMetods metods)
        {
            _metods= metods;
            listOrderViewModels = new List<OrderViewForDirectorDTO>();

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            listOrderViewModels = await _metods.GetAllAsync<OrderViewForDirectorDTO>();
            if (listOrderViewModels!=null)
            {
                for (int i = 0; i < listOrderViewModels.Count; i++)
                {
                    if (listOrderViewModels[i].DateCreateOrder != null)
                    {
                        listOrderViewModels[i].DateCreateOrder = EditStringDateCreate(listOrderViewModels[i].DateCreateOrder);
                    }
                }
            }

            return Json(listOrderViewModels);
        }






        #region Metods

        private string EditStringDateCreate(string date)
        {
            var newString = "";
            if (date != null)
            {
                var part_1 = date.Substring(11);
                var part_2 = date.Substring(8, 2);
                var part_3 = date.Substring(5, 2);
                var part_4 = date.Substring(0, 4);
                newString = part_1 + " " + part_2 + "-" + part_3 + "-" + part_4;
            }

            return newString;
        }

        #endregion 



    }
}
