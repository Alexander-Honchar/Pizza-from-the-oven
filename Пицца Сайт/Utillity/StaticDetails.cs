namespace Пицца_Сайт.Utillity
{
    public static class StaticDetails
    {
		public const string Баварская = "Пицца Баварская";
		public const string Фунжи = "Пицца Фуджи";

        public const string ListMenuInSession = "ListOrdersMenu"; // список заказов сохраняем в сесии
        public const string ListCartInSession = "ListOrdersCart"; // список корзин сохраняем в сесии
        public const string OrderViewModelInSession = "OrderViewModel"; // OrderViewModel сохраняем в сесии

        public enum APIType
        {
            GET,
            POST,
            PUT,
            DELETE,
            PATH,
        }
    }
}
