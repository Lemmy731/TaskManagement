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
    public class ProjectService: IProjectService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGenericRepository<Project> _genericReposistory;
        private readonly ILogger<ProjectService> _logger;
        private readonly IMapper _mapper;

        public ProjectService(AppDbContext appDbContext, IGenericRepository<Project> genericRepository, ILogger<ProjectService> logger, IMapper mapper) : base()
        {
            _appDbContext = appDbContext;
            _genericReposistory = genericRepository;
            _logger = logger;
            _mapper = mapper;   
        }

        public async Task<Response<ProjectDto>> AddProjectAsync(ProjectDto projectDto)
        {
            try
            {
                var map = _mapper.Map<Project>(projectDto);
                var result = await _genericReposistory.AddAsync(map);
                if (result != null)
                {
                    return Response<ProjectDto>.Success("Successful", _mapper.Map<ProjectDto>(result));
                }
                return Response<ProjectDto>.Fail("Failed to add the task.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                return Response<ProjectDto>.Fail("An error occurred while processing your request.", 500);
            }
        }
        public async Task<Response<List<ProjectDto>>> GetProjectAsync(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _genericReposistory.GetAllAsync();
                if (result != null)
                {

                    var bookPagin = PageList<Project>.ToPageList(result, pageNumber, pageSize);
                    var data = _mapper.Map<List<ProjectDto>>(bookPagin);
                    return Response<List<ProjectDto>>.Success("Successful", data);

                };
                return Response<List<ProjectDto>>.Fail($"No data found", 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                return Response<List<ProjectDto>>.Fail("An error occurred while processing your request.", 500);
            }
        }
        public async Task<Response<ProjectDto>> UpdateProjectAsyncById(Guid Id, ProjectDto projectDto)
        {
            try
            {

                var data = _mapper.Map<Project>(new ProjectDto
                {
                    Name = projectDto.Name,

                    Description = projectDto.Description
                });
                var result = await _genericReposistory.UpdateAsync(Id, data);
                if (result != null)
                {
                    return Response<ProjectDto>.Success("Successful", projectDto, 200);

                };
                return Response<ProjectDto>.Fail($"unable to update", 500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                return Response<ProjectDto>.Fail("An error occurred while processing your request.", 500);
            }
        }
        public async Task<Response<Guid>> DeleteProjectAsyncById(Guid Id)
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
    }
}
