using Microsoft.AspNetCore.Mvc.Rendering;
using Пицца_Офис.Models.DTO;

namespace Пицца_Офис.Models.ViewModels
{
    public class MenuItemDTOViewModel
    {
        public MenuItemDTO? MenuItem { get; set; }
        public IEnumerable<SelectListItem>? PizzaList { get; set;}
        public IEnumerable<SelectListItem>? PizzaKingSizeList { get; set; }
        public IEnumerable<SelectListItem>? CategoryMenuList { get; set; }
    }
}
