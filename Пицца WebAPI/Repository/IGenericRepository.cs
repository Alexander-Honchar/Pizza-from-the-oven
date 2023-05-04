using System.Linq.Expressions;

namespace Pizza_WebAPI.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? includeProperties = null);

        T GetOne(Expression<Func<T,bool>> filter, string? includeProperties = null);

        void  Create(T entity);

        void Delete(T entity);

        
        
        //string CreateNewNameForImage(IFormFile file, string nameFolder);

        //void DeleteFile(Expression<Func<T, bool>> filter, string imagePath);

    }
}
