using AutoMapper;
using TaskManagementApplication.Data;
using TaskManagementDomain.DTO;
using TaskManagementDomain.Entity;
using TaskManagementDomain.Helper;
using TaskManagementDomain.IRepository;
using TaskManagementSystemApi.Helper.Pagination;
using TaskManagementSystemApi.IService;

namespace TaskManagementSystemApi.Service
{
    public class NotificationService: INotificationService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGenericRepository<Notification> _genericReposistory;
        private readonly ILogger<NotificationService> _logger;
        private readonly IMapper _mapper;

        public NotificationService(AppDbContext appDbContext, IGenericRepository<Notification> genericRepository, ILogger<NotificationService> logger, IMapper mapper) : base()
        {
            _appDbContext = appDbContext;
            _genericReposistory = genericRepository;
            _logger = logger;
            _mapper = mapper;   
        }

        public async Task<Response<NotificationDto>> AddNotificationAsync(NotificationDto notificationDto)
        {
            try
            {
                var map = _mapper.Map<Notification>(notificationDto);
                var result = await _genericReposistory.AddAsync(map);
                if (result != null)
                {
                    return Response<NotificationDto>.Success("Successful", notificationDto);
                }
                return Response<NotificationDto>.Fail("Failed to add the task.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                return Response<NotificationDto>.Fail("An error occurred while processing your request.", 500);
            }
        }
        public async Task<Response<List<NotificationDto>>> GetNotificationAsync(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _genericReposistory.GetAllAsync();
                if (result != null)
                {

                    var bookPagin = PageList<Notification>.ToPageList(result, pageNumber, pageSize);
                    var data = _mapper.Map<List<NotificationDto>>(bookPagin);
                    return Response<List<NotificationDto>>.Success("Successful", data);

                };
                return Response<List<NotificationDto>>.Fail($"No data found", 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                return Response<List<NotificationDto>>.Fail("An error occurred while processing your request.", 500);
            }

        }
        public async Task<Response<NotificationDto>> UpdateNotificationAsyncById(Guid Id, NotificationDto notificationDto)
        {
            try
            {

                var data = _mapper.Map<Notification>(new NotificationDto
                {
                    Type = notificationDto.Type,    
                    Timestamp = notificationDto.Timestamp,  
                    Message = notificationDto.Message
                });
                var result = await _genericReposistory.UpdateAsync(Id, data);
                if (result != null)
                {
                    return Response<NotificationDto>.Success("Successful", notificationDto, 200);

                };
                return Response<NotificationDto>.Fail($"unable to update", 500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                return Response<NotificationDto>.Fail("An error occurred while processing your request.", 500);
            }

        }
        public async Task<Response<Guid>> DeleteNotificationAsyncById(Guid Id)
        {
            try
            {
                var result = await _genericReposistory.DeleteAsync(Id);
                if (result != null)
                {
                    return Response<Guid>.Success("Successful", Id, 200);

                };
                return Response<Guid>.Fail($"No data found", 500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                return Response<Guid>.Fail("An error occurred while processing your request.", 500);
            }
        }

        public async Task<Response<MarkNotificationDto>> MarkNotification(Guid notificationId,MarkNotificationDto markNotification)
        {
            try
            {
               var result = _genericReposistory.MarkNotification(notificationId, markNotification);   
                if(result.Result == "mark as read" || result.Result == "mark as unread")
                {
                    return Response<MarkNotificationDto>.Success("Successful",markNotification, 200);
                }
                return Response<MarkNotificationDto>.Fail("Unsuccessful", 500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                return Response<MarkNotificationDto>.Fail("An error occurred while processing your request.", 500);
            }
        }
    }
}
