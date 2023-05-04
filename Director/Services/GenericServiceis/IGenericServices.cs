namespace Director.Services.GenericServiceis
{
    public interface IGenericServices<T> where T : class
    {
        Task<K> GetAllAsync<K>();
        Task<K> GetAlRolesAsync<K>();
        Task<K> GetOneAsync<K>(string id);

        Task<K> RegisterAsync<K>(T entity);

        Task<K> DeleteAsync<K>(string id);
        Task<K> UpdateAsync<K>(T entity);

        Task<K> LoginAsync<K>(T entity);

    }
}
