using TaskManagementDomain.DTO;
using TaskManagementDomain.Entity;
using TaskManagementDomain.Helper;

namespace TaskManagementSystemApi.IService
{
    public interface ITaskService
    {
        Task<Response<TaskDto>> AddTaskAsync(TaskDto taskDto);
        Task<Response<List<TaskDto>>> GetTaskAsync(int pageNumber, int pageSize);
        Task<Response<TaskDto>> UpdatTaskAsyncById(Guid Id, TaskDto taskDto);
        Task<Response<Guid>> DeleteTaskAsyncById(Guid Id);
        Task<Response<List<MyTask>>> FilterTask(PriorityDto taskDto);
        Task<Response<List<MyTask>>> FilterTaskByDueDate(DueDateDto dueDto);
    }
}
