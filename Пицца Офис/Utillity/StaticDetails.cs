namespace Пицца_Офис.Utillity
{
    public static class StaticDetails
    {
        public const string Success = "Success";
        public const string Error = "Error";


        public enum APIType
        {
            GET,
            POST,
            PUT,
            DELETE,
            PATH,
        }



        public const string StatusAccepted = "Принят";
        public const string StatusInWork = "В работе";
        public const string StatusReady = "Готов";
        public const string StatusSent = "Достаавлен";

        public const string StatusDelete = "Удален";


        public const string ManagerRole = "Manager";
        public const string AdministratorRole = "Administrator";
        public const string DirectorRole = "Director";
        public const string CookRole = "Cook";
        public const string OfficeRole = "Manager,Administrator";


        public static string? SessionToken = "JWTToken";
    }
}
