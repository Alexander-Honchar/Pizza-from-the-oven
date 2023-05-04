using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Core;
using System.Linq.Expressions;
using Pizza_WebAPI.Data;
using Pizza_WebAPI.Utillity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Pizza_WebAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        readonly ApplicationDbContext _dbContext;
        readonly DbSet<T> _dbSet;
        
        readonly T _modelEntity;


        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            _modelEntity = GetObject();
        }



        public void  Create(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                //_logger.LogInformation("Instance successfully created");
            }
            catch (Exception ex)
            {
                //_logger.LogInformation("Instance  not created");
                //_logger.LogError(ex.ToString());
                throw;
            }
            
        }



        //public string CreateNewNameForImage(IFormFile file,string nameFolder)
        //{
        //    try
        //    {
        //        var wwwrootPatch = _webHostEnvironment.WebRootPath;
        //        var newFileName = Guid.NewGuid().ToString();
        //        var upload = Path.Combine(wwwrootPatch, @"imeges\" + nameFolder);
        //        var extension = Path.GetExtension(file.FileName);

        //        #region Создаем поток чтобы сохранить файл
        //        using (var fileStreams = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
        //        {
        //            file.CopyTo(fileStreams);
        //        }
        //        #endregion

                
        //        _logger.LogInformation("New name for file successfully created");


        //        return @"\images\" + nameFolder + newFileName + extension;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogInformation("New name for file not created");
        //        _logger.LogError(ex.ToString());
        //        throw;
        //    }
            

        //}



        public void Delete(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                //_logger.LogInformation("Instance successfully removed");
            }
            catch (Exception ex)
            {
                //_logger.LogInformation("Instance not removed");
                //_logger.LogError(ex.ToString());
                throw;
            }
            
        }



        //public void DeleteFile(Expression<Func<T,bool>> filter,string imagePath)
        //{
        //    try
        //    {
                
        //        var getModelFromDb = _dbSet.AsNoTracking().FirstOrDefault(filter);
        //        if (getModelFromDb != null)
        //        {
        //            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath.TrimStart('\\'));
        //            if (System.IO.File.Exists(oldImagePath))
        //            {
        //                System.IO.File.Delete(oldImagePath);
        //                _logger.LogInformation("File successfully removed");
        //            }

        //            _logger.LogInformation("File not found on server");
        //        }
        //        _logger.LogInformation("File not removed.This is not in DataBase");
                

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogInformation("File not removed.");
        //        _logger.LogError(ex.ToString());
        //        throw;
        //    }
        //}




        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? includeProperties = null)
        {
            try
            {
                IQueryable<T> queryab = _dbSet;
                if (filter!=null)
                {
                    queryab=queryab.Where(filter);
                }

                if (includeProperties!=null)
                {
                    foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        queryab = queryab.Include(item);
                    }
                }


                //_logger.LogInformation("Instances successfully found");
                return queryab.ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogInformation("Instances not found");
                //_logger.LogError(ex.ToString());
                throw;
            }
        }




        public T GetOne(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            try
            {
                IQueryable<T> queryab = _dbSet;
                if (filter != null)
                {
                    queryab = queryab.Where(filter);
                }

                if (includeProperties != null)
                {
                    foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        queryab = queryab.Include(item);
                    }
                }

                
                var result = queryab.FirstOrDefault();
                if (result != null)
                {
                    //_logger.LogInformation("Instance successfully found");
                    return result;
                }

                return _modelEntity;


            }
            catch (Exception ex)
            {
                //_logger.LogInformation("Instance  not found");
                //_logger.LogError(ex.ToString());
                throw;
            }
        }




        protected T GetObject()
        {
            if (typeof(T).IsValueType || typeof(T) == typeof(string))
            {
                return default(T);
            }
            else
            {
                return (T)Activator.CreateInstance(typeof(T));
            }
        }

    }
}
