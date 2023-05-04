namespace Пицца_Сайт.Services.GenericServiceis
{
    public interface IGenericServices<T> where T : class
    {
        Task<K> GetAllAsync<K>();
        Task<K> GetAllNameCategoryAsync<K>(string nameCategory);
        Task<K> GetOneAsync<K>(uint id);

        Task<K> CreateAsync<K>(T entity);

        //Task<K> DeleteAsync<K>(uint id);
        //Task<K> UpdateAsync<K>(T entity);

    }
}
