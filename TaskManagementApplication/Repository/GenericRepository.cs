using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Data;
using TaskManagementDomain.DTO;
using TaskManagementDomain.IRepository;

namespace TaskManagementApplication.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository()
        {}
        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<T>();
        }

        public async Task<string> AddAsync(T entity)
        {
            var add = await _appDbContext.Set<T>().AddAsync(entity);
            var save = await _appDbContext.SaveChangesAsync();
            return "successfully added";
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            var find = await _appDbContext.Set<T>().FindAsync(id);
            if (find != null)
            {
                _appDbContext.Set<T>().Remove(find);
                await _appDbContext.SaveChangesAsync();
                return "deleted";
            }
            return "unable to delete";
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> query = _dbSet;
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();

        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var item = await _appDbContext.Set<T>().FindAsync(id);
            return item;
        }

        public async Task<string> UpdateAsync(Guid id, T entity)
        {
            var find = await _appDbContext.Set<T>().FindAsync(id);
            if (find != null)
            {
                _appDbContext.Entry<T>(find).CurrentValues.SetValues(entity);
                await _appDbContext.SaveChangesAsync();
                return "updated successfully";
            }
            return "unable to update";
        }

        public async Task<string> MarkNotification(Guid id, MarkNotificationDto markNotificationDto)
        {
            var find = await _appDbContext.Notifications.FindAsync(id);
            if (find != null)
            {
               if(markNotificationDto.Status == "read")
                {
                    find.Status = "read";
                    _appDbContext.Notifications.Update(find); 
                    await _appDbContext.SaveChangesAsync();
                    return "mark as read";
                }
              else if (markNotificationDto.Status == "unread")
                {
                    find.Status = "unread";
                    _appDbContext.Notifications.Update(find);
                    await _appDbContext.SaveChangesAsync();
                    return "mark as unread";
                }
               
            }
            return "Notification not found";
        }

    }
}
