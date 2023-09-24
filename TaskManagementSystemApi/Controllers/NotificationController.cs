using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementDomain.DTO;
using TaskManagementSystemApi.IService;
using TaskManagementSystemApi.Service;

namespace TaskManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
                _notificationService = notificationService;    
        }
        [HttpPost]
        public async Task<IActionResult> AddTask(NotificationDto notificationDto)
        {
            var response = _notificationService.AddNotificationAsync(notificationDto);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetTask(int pageNumber, int pageSize)
        {
            var response = _notificationService.GetNotificationAsync(pageNumber, pageSize);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdatTask(Guid Id, NotificationDto notificationDto)
        {
            var response = _notificationService.UpdateNotificationAsyncById(Id, notificationDto);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTask(Guid Id)
        {
            var response = _notificationService.DeleteNotificationAsyncById(Id);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost("MarkNotificaiton")]
        public async Task<IActionResult> MarkNotificaiton(Guid notificationId,[FromBody]MarkNotificationDto markNotificationDto)
        {
            var response = _notificationService.MarkNotification(notificationId, markNotificationDto);
            if (response.IsCompletedSuccessfully)
            {
                return Ok(response);
            }
            return BadRequest();
        }

    }
}
