using Director.Models;
using Director.Models.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Director.Services.Metods
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
        Task<T> GetOneAsync<T>(string id) where T : class;


        /// <summary>
        /// получаем список ролей из Db
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nameCategory"></param>
        /// <returns></returns>
        Task<List<IdentityRole>> GetListRolesAsync();



        /// <summary>
        /// получаем токен для User
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        Task<LoginResponseDTO> GetTokenForUserAsync(LoginRequestDTO loginRequest);


    }
}
