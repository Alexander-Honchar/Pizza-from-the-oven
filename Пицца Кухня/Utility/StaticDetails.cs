namespace Пицца_Кухня.Utility
{
    public class StaticDetails
    {
        public const string StatusAccepted = "Принят";
        public const string StatusInWork = "В работе";
        public const string StatusReady = "Готов";
        public const string StatusSent = "Достаавлен";


        public const string ManagerRole = "Manager";
        public const string AdministratorRole = "Administrator";
        public const string DirectorRole = "Director";
        public const string CookRole = "Cook";


        public static string? SessionToken = "JWTToken";

    }
}
