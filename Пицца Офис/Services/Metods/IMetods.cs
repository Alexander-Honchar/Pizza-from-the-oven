using Пицца_Офис.Models.Authorization;

namespace Пицца_Офис.Services.Metods
{
    public interface IMetods
    {

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


        /// <summary>
        /// получаем токен для User
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        Task<LoginResponseDTO> GetTokenForUserAsync(LoginRequestDTO loginRequest);

    }
}
