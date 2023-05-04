using Hanssens.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using Пицца_Сайт.Models;
using Пицца_Сайт.Models.DTO;
using Пицца_Сайт.Services.Metods;
using Пицца_Сайт.Utillity;

namespace Пицца_Сайт.Controllers
{
	public class CartController : Controller
	{
		readonly IMetods _metods;

		
		
        public Cart Cart { get; set; }
        public List<Cart> CartList { get; set; }
		public  ShoppingCart ShoppingCart { get; set; }
        private ChangesCount changesCount;
        LocalStorage storage;



        public CartController(IMetods metods)
		{
			_metods = metods;
            Cart=new Cart();
			CartList=new List<Cart>();
			ShoppingCart=new ShoppingCart();
            changesCount=new ChangesCount();
            storage = new LocalStorage();

        }

		public async Task<IActionResult> Index(ChangesCount count)
		{

            HttpContext.Session.Remove("countForCart");

            if (!count.IsChangesCount)
            {
                var total = 0.0;
                var getMenuFormSession = _metods.GetCartFromSession();
                if (getMenuFormSession != null)
                {

                    foreach (var item in getMenuFormSession)
                    {
                        #region get for Pizza
                        if (item.NameCategory == "Pizza")
                        {
                            var menuFromDb = await _metods.GetOneAsync<MenuItemDTO>(item.Id);
                            var pizzaFromDb = await _metods.GetOneAsync<PizzaDTO>(item.ProductId);
                            Cart = SetCartWithProduct(menuFromDb.Id, item.ProductId, item.Count, item.NameCategory,
                                                              menuFromDb.Name, pizzaFromDb.Price, menuFromDb.ImagePatch);

                            total += (pizzaFromDb.Price * item.Count);
                            Cart.Pizza = pizzaFromDb;
                            CartList.Add(Cart);
                        }
						#endregion


						#region get for KingSize
						if (item.NameCategory == "KingSize")
						{
							var menuFromDb = await _metods.GetOneAsync<MenuItemDTO>(item.Id);
							Cart = SetCartWithProduct(menuFromDb.Id, item.ProductId, item.Count, item.NameCategory,
															  menuFromDb.Name, (double)menuFromDb.Price, menuFromDb.ImagePatch);

							total += (double)(menuFromDb.Price * item.Count);
							CartList.Add(Cart);
						}
						#endregion

					}

					ShoppingCart.CartsList = CartList;
                    ShoppingCart.TotalSumma = total;
                }
                SaveShoppingCart(ShoppingCart);
                return View(ShoppingCart);
            }
            ShoppingCart =_metods.GetShoppingCart();
            return View(ShoppingCart);
        }








        #region CartAction Plus-Minus
        public IActionResult Plus(uint id)
        {
            var symbol = "+";
            ShoppingCart = _metods.GetShoppingCart();

            if (ShoppingCart != null)
            {
                var changesShoppingCart = ChangesCountInShoppingCart(ShoppingCart, id, symbol);

                SaveShoppingCart(changesShoppingCart);

                changesCount.IsChangesCount = true;
                return RedirectToAction("Index", routeValues: changesCount);
            }

            changesCount.IsChangesCount = true;
            return RedirectToAction("Index",routeValues: changesCount);
        }

        public IActionResult Minus(uint id)
        {
            var symbol = "-";
            ShoppingCart = _metods.GetShoppingCart();

            if (ShoppingCart!=null)
            {
                var changesShoppingCart = ChangesCountInShoppingCart(ShoppingCart, id, symbol);

                SaveShoppingCart(changesShoppingCart);

                changesCount.IsChangesCount = true;
                return RedirectToAction("Index", routeValues: changesCount);
            }

            changesCount.IsChangesCount = false;
            return RedirectToAction("Index", routeValues: changesCount);
        }

        public IActionResult Remove(uint id)
        {
            var symbol = "0";
            ShoppingCart = _metods.GetShoppingCart();

            if (ShoppingCart != null)
            {
                var changesShoppingCart = ChangesCountInShoppingCart(ShoppingCart, id, symbol);

                SaveShoppingCart(changesShoppingCart);

                changesCount.IsChangesCount = true;
                return RedirectToAction("Index", routeValues: changesCount);
            }

            changesCount.IsChangesCount = false;
            return RedirectToAction("Index", routeValues: changesCount);
        }
        #endregion



        #region Metods

        /// <summary>
        /// сохраняем в каждую отдельную корзинку товар  из меню
        /// </summary>
        /// <param name="MenuId"></param>
        /// <param name="ProductId"></param>
        /// <param name="Count"></param>
        /// <param name="MenuNameCategory"></param>
        /// <param name="Name"></param>
        /// <param name="Price"></param>
        /// <param name="ImagePatch"></param>
        /// <returns></returns>
        Cart SetCartWithProduct(uint MenuId, uint ProductId, uint Count, string? MenuNameCategory,
			                              string? Name, double Price, string? ImagePatch)
        {
			Cart cart = new Cart();
			cart.MenuId = MenuId;
			cart.ProductId = ProductId;
			cart.Count = Count;
			cart.MenuNameCategory = MenuNameCategory;
			cart.Name = Name;
			cart.Price = Price;
			cart.ImagePatch = ImagePatch;
		
			return cart;

		}






        /// <summary>
        /// сохраняем в  сессии корзину с товарами
        /// </summary>
        /// <param name="model"></param>
		void SaveShoppingCart(ShoppingCart model)
		{

            var serializingShoppingCart = JsonConvert.SerializeObject(model);
            HttpContext.Session.SetString(StaticDetails.ListCartInSession, serializingShoppingCart);

        }




        


        /// <summary>
        /// изменяем в корзине количество товаров
        /// </summary>
        /// <param name="cart">Корзина покупок</param>
        /// <param name="id"> ID меню</param>
        /// <param name="symbol">символ + или -  </param>
        /// <returns></returns>
        ShoppingCart ChangesCountInShoppingCart (ShoppingCart cart,uint id, string symbol)
        {
            
            ShoppingCart updateCart = new ShoppingCart();
            var updateTotalSumma = 0.0;
            storage.Store("TotalSummaSession", updateTotalSumma);
            for (int i = 0; i < cart.CartsList.Count; i++)
            {
                bool calculate = true;

                if (cart.CartsList[i].MenuId == id)
                {
                    if (symbol == "-")
                    {
                        cart.CartsList[i].Count--;
                        _metods.ChangesCountInCart(cart.CartsList[i].MenuId, cart.CartsList[i].Count); // изменяем CartViewComponent
                        updateTotalSumma += cart.CartsList[i].Price * cart.CartsList[i].Count;
                        if (cart.CartsList[i].Count == 0)
                        {
                            cart.CartsList.RemoveAt(i);
                            calculate = false;
                        }
                        ChangeTotalSuummaInSession(updateTotalSumma);
                    }
                    else if (symbol == "+")
                    {
                        cart.CartsList[i].Count++;
                        _metods.ChangesCountInCart(cart.CartsList[i].MenuId, cart.CartsList[i].Count);  // изменяем CartViewComponent
                        updateTotalSumma += cart.CartsList[i].Price * cart.CartsList[i].Count;
                        ChangeTotalSuummaInSession(updateTotalSumma);
                    }
                    else if (symbol == "0")
                    {
                        cart.CartsList[i].Count = 0;
                        _metods.ChangesCountInCart(cart.CartsList[i].MenuId, cart.CartsList[i].Count);  // изменяем CartViewComponent
                        cart.CartsList.RemoveAt(i);
                        calculate = false;
                    }
                }
                else
                {
                    var oldTotal = storage.Get<double>("TotalSummaSession");
                    oldTotal += cart.CartsList[i].Price * cart.CartsList[i].Count;
                    storage.Store("TotalSummaSession", oldTotal);
                }

            }
                
            updateCart.CartsList=cart.CartsList;
            updateCart.TotalSumma = storage.Get<double>("TotalSummaSession");                       //updateTotalSumma;
            return updateCart;
        }


       

        void ChangeTotalSuummaInSession(double totalSuumma)
        {
            
            var oldTotal = storage.Get<double>("TotalSummaSession");
            if (oldTotal != 0)
            {
                oldTotal += totalSuumma;
                storage.Store("TotalSummaSession", oldTotal);
            }
            else
            {
                oldTotal = totalSuumma;
                storage.Store("TotalSummaSession", oldTotal);
            }
        }



        #endregion
    }
}
