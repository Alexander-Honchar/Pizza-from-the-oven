namespace Пицца_Кухня.Services.GenericServiceis
{
    public interface IGenericServices<T> where T : class
    {
        Task<K> GetOneAsync<K>(uint id);
        Task<K> GetAllAsync<K>();
        Task<K> UpdateStatusAsync<K>(string status, uint id,string? idEmployee);
        Task<K> LoginAsync<K>(T entity);
    }
}
