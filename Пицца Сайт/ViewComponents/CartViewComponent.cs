using Microsoft.AspNetCore.Mvc;
using Пицца_Сайт.Services.Metods;

namespace Пицца_Сайт.CartViewComponent
{
    public class CartViewComponent: ViewComponent
    {
        readonly IMetods _metods;


        public CartViewComponent(IMetods metods)
        {
            _metods= metods;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.Run(() => 
            {
                uint count = 0;
                var listMenuInCart = _metods.GetCartFromSession();
                if (listMenuInCart!=null)
                {
                    foreach (var item in listMenuInCart)
                    {
                        count += item.Count;
                    }
                }
                
                return View(count);
            });
            
        }
    }
}
