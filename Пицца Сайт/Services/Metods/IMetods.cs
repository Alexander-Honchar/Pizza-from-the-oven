using System.Threading.Tasks;
using Пицца_Сайт.Models;
using Пицца_Сайт.Models.DTO;
using Пицца_Сайт.Models.ViewModels;

namespace Пицца_Сайт.Services.Metods
{
    public interface IMetods
    {
        void Plus(uint id, uint count);
        void Minus(uint id, uint count);
        void SaveCartInSession(MenuItemDTO model);


        /// <summary>
        /// достаем меню для корзины из сессии
        /// </summary>
        /// <returns></returns>
        List<MenuItemDTO> GetCartFromSession();

        /// <summary>
        /// достаем из сессии корзину с товарами
        /// </summary>
        /// <returns></returns>
        public ShoppingCart GetShoppingCart();





        /// <summary>
        /// получаем список меню по названию категории из Db
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nameCategory"></param>
        /// <returns></returns>
        Task<List<T>> GetListNameCategoryMenuAsync<T>(string nameCategory) where T : class;


        /// <summary>
        /// универсальный метод возвращает список товаров из Db заданого класса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<List<T>> GetAllAsync<T>() where T : class;

        /// <summary>
        /// универсальный метод возвращает товар из Db заданого класса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetOneAsync<T>(uint id) where T : class;

        ///// <summary>
        ///// универсальный метод создает товар в Db заданого класса
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //Task<T> CrateAsync<T>(T model) where T : class;



        /// <summary>
        /// получаем список Pizza из Db по названию пиццы
        /// </summary>
        /// <param name="namePizza"></param>
        /// <returns></returns>
        Task<List<PizzaDTO>> GetListPizzaByName(string namePizza);

        /// <summary>
        /// возвращает ID меню из сессии счетчика 
        /// </summary>
        /// <returns></returns>
        uint GetIdMenuForPlusMinusInCart();

        /// <summary>
        /// возвращает Count товара меню из сессии  
        /// </summary>
        /// <returns></returns>
        uint GetCountMenuForPlusMinusInCart();



        /// <summary>
        /// изменяем в сессии  CartViewComponent , if count=0  удаляется меню из CartViewComponent
        /// </summary>
        /// <param name="id"> ID MenuItem</param>
        /// <param name="count"> Новое количество меню</param>
        /// <param name="symbol"></param>
        void ChangesCountInCart(uint id, uint count);



        /// <summary>
        ///  удаляем одну позицию в заказе
        /// </summary>
        /// <param name="orderViewModels"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        OrderViewModels RemoveOrderInOrderView(OrderViewModels orderViewModels, uint id);


        //void SaveMenuItemInCookies(MenuItemDTO model);
    }
}
