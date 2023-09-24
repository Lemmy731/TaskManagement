using TaskManagementDomain.DTO;
using TaskManagementDomain.Helper;

namespace TaskManagementSystemApi.IService
{
    public interface IProjectService
    {
        Task<Response<ProjectDto>> AddProjectAsync(ProjectDto projectDto);
        Task<Response<List<ProjectDto>>> GetProjectAsync(int pageNumber, int pageSize);
        Task<Response<ProjectDto>> UpdateProjectAsyncById(Guid Id, ProjectDto projectDto);
        Task<Response<Guid>> DeleteProjectAsyncById(Guid Id);
    }
}
