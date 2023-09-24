using TaskManagementDomain.DTO;
using TaskManagementDomain.Helper;

namespace TaskManagementSystemApi.IService
{
    public interface INotificationService
    {
        Task<Response<NotificationDto>> AddNotificationAsync(NotificationDto notificationDto);
        Task<Response<List<NotificationDto>>> GetNotificationAsync(int pageNumber, int pageSize);
        Task<Response<NotificationDto>> UpdateNotificationAsyncById(Guid Id, NotificationDto notificationDto);
        Task<Response<Guid>> DeleteNotificationAsyncById(Guid Id);
        Task<Response<MarkNotificationDto>> MarkNotification(Guid notificationId,MarkNotificationDto markNotification);
    }
}
