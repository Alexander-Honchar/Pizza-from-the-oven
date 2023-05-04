using Microsoft.AspNetCore.Mvc.Rendering;
using Пицца_Офис.Models.DTO;

namespace Пицца_Офис.Models.ViewModels
{
    public class PizzaDTOViewModel
    {
        public PizzaDTO? PizzaDTO { get; set; }
        public IEnumerable<SelectListItem>? CategorySelectList { get; set; }
        public IEnumerable<SelectListItem>? SizeSelectList { get; set; }
    }
}
