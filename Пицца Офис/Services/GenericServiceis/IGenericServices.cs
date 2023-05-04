namespace Пицца_Офис.Services.GenericServiceis
{
    public interface IGenericServices<T> where T : class
    {
        Task<K> GetAllAsync<K>();
        Task<K> GetAllNameCategoryAsync<K>(string nameCategory);
        Task<K> GetOneAsync<K>(uint id);

        Task<K> CreateAsync<K>(T entity);

        Task<K> DeleteAsync<K>(uint id);
        Task<K> UpdateAsync<K>(T entity);

        Task<K> UpdateOrderDetailsAsync<K>(uint id, uint count);

        Task<K> UpdateStatusAsync<K>(string status, uint id, string? idEmployee);
        Task<K> LoginAsync<K>(T entity);

    }
}
