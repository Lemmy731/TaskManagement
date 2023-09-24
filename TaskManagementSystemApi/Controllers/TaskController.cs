using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementDomain.DTO;
using TaskManagementSystemApi.IService;

namespace TaskManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
                _taskService = taskService; 
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(TaskDto taskDto)
        {
            var response = _taskService.AddTaskAsync(taskDto);
            if (response.IsCompletedSuccessfully)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet("GetTask")]
        public async Task<IActionResult> GetTask(int pageNumber, int pageSize)
        {
            var response = _taskService.GetTaskAsync(pageNumber, pageSize);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdatTask(Guid Id, TaskDto taskDto)
        {
            var response = _taskService.UpdatTaskAsyncById(Id, taskDto);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTask(Guid Id)
        {
            var response = _taskService.DeleteTaskAsyncById(Id);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet("FilterTask")]   
        public async Task<IActionResult> FilterTask([FromQuery] PriorityDto taskDto)
        {
             var response = await _taskService.FilterTask(taskDto);
                if (response != null) 
                {
                    return Ok(response);
                }
                return BadRequest();
            
        }
        [HttpGet]
        public async Task<IActionResult> FilterTaskByDueDate([FromQuery] DueDateDto dueDto)
        {
            
                var response = await _taskService.FilterTaskByDueDate(dueDto);
                if (response != null)
                {
                    return Ok(response);
                }
                return BadRequest();
        }
    }
}
