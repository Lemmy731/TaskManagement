using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagementDomain.DTO;
using TaskManagementDomain.Helper;

namespace TaskManagementDomain.IRepository
{
    public interface IGenericRepository<T> where T : class, new()
    {
        Task<string> AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(Guid id);
        //Task<string> taskDt(T entity);
        Task<string> UpdateAsync(Guid id, T entity);
        Task<string> DeleteAsync(Guid id);
        Task<string> MarkNotification(Guid id, MarkNotificationDto markNotificationDto);
    }
}
