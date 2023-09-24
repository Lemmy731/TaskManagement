using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Threading.Tasks;
using TaskManagementApplication.Data;
using TaskManagementApplication.Repository;
using TaskManagementDomain.DTO;
using TaskManagementDomain.Entity;
using TaskManagementDomain.Helper;
using TaskManagementDomain.IRepository;
using TaskManagementSystemApi.Helper.Pagination;
using TaskManagementSystemApi.IService;

namespace TaskManagementSystemApi.Service
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGenericRepository<MyTask> _genericReposistory;
        private readonly ILogger<TaskService> _logger;
        private readonly IMapper _mapper;

        public TaskService(AppDbContext appDbContext, IGenericRepository<MyTask> genericRepository, ILogger<TaskService> logger, IMapper mapper) : base()
        {
            _appDbContext = appDbContext;
            _genericReposistory = genericRepository;
            _logger = logger;
            _mapper = mapper;
        }
        //Task Crud
        public async Task<Response<TaskDto>> AddTaskAsync(TaskDto taskDto)
        {
            try
            {
                var map = _mapper.Map<MyTask>(taskDto);
                var result = await _genericReposistory.AddAsync(map);
                if (result == "successfully added")
                {
                    return Response<TaskDto>.Success("Successful", taskDto, 200);
                }
                return Response<TaskDto>.Fail("Failed to add the task.");
            }catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                return Response<TaskDto>.Fail("An error occurred while processing your request.", 500);
            }
        }
        public async Task<Response<List<TaskDto>>> GetTaskAsync(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _genericReposistory.GetAllAsync();
                if (result != null)
                {

                    var bookPagin = PageList<MyTask>.ToPageList(result, pageNumber, pageSize);
                    var data = _mapper.Map<List<TaskDto>>(bookPagin);
                    return Response<List<TaskDto>>.Success("Successful", data);

                };
                return Response<List<TaskDto>>.Fail($"No data found", 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                return Response<List<TaskDto>>.Fail("An error occurred while processing your request.", 500);
            }
           
        }
            
    public async Task<Response<TaskDto>> UpdatTaskAsyncById(Guid Id, TaskDto taskDto)
    {
            try
            {

                var data = _mapper.Map<MyTask>(new TaskDto
                {
                    Title = taskDto.Title,
                    Description = taskDto.Description,
                    Priority = taskDto.Priority,
                    DueDate = taskDto.DueDate,
                    Status = taskDto.Status,
                    UserId = taskDto.UserId,
                    ProjectId = taskDto.ProjectId
                });
                var result = await _genericReposistory.UpdateAsync(Id, data);
                if (result != null)
                {
                    return Response<TaskDto>.Success("Successful", taskDto, 200);

                };
                return Response<TaskDto>.Fail($"unable to update", 500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                return Response<TaskDto>.Fail("An error occurred while processing your request.", 500);
            }

        }
        public async Task<Response<Guid>> DeleteTaskAsyncById(Guid Id)
        {
            try
            {
                var result = await _genericReposistory.DeleteAsync(Id);
                if (result != null)
                {
                    return Response<Guid>.Success("Successful",Id, 200);

                };
                return Response<Guid>.Fail($"No data found", 500);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                return Response<Guid>.Fail("An error occurred while processing your request.", 500);
            }   
            

        }
    public async Task<Response<List<MyTask>>> FilterTask(PriorityDto taskDto)
    {
            try
            {
                var result = await _appDbContext.MyTasks.Where(x => x.Priority == taskDto.Priority).ToListAsync();
                if (result.Count > 0)
                {
                    return Response<List<MyTask>>.Success("Successful", result);
                }
                return Response<List<MyTask>>.Fail($"No data found", 404);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                return Response<List<MyTask>>.Fail("An error occurred while processing your request.", 500);
            }
       
    }
    public async Task<Response<List<MyTask>>> FilterTaskByDueDate(DueDateDto dueDto)
    {
            try
            {
                var result = await _appDbContext.MyTasks.Where(x => x.DueDate == dueDto.Date).ToListAsync();
                if (result.Count > 0)
                {
                    return Response<List<MyTask>>.Success("Successful", result);
                }
                return Response<List<MyTask>>.Fail($"No data found", 404);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                return Response<List<MyTask>>.Fail("An error occurred while processing your request.", 500);
            }
       
    }
    }
    
}
