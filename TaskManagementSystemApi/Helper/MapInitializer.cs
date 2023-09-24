using AutoMapper;
using TaskManagementDomain.DTO;
using TaskManagementDomain.Entity;

namespace TaskManagementSystemApi.Helper
{
    public class MapInitializer: Profile
    {
        public MapInitializer()
        {
            CreateMap<TaskDto, MyTask>().ReverseMap();
            CreateMap<NotificationDto, Notification>().ReverseMap(); 
            CreateMap<MarkNotificationDto, Notification>().ReverseMap();
            CreateMap<ProjectDto, Project>().ReverseMap();  
        }
    }
}
