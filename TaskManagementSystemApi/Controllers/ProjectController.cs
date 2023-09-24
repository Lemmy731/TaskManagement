using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementDomain.DTO;
using TaskManagementSystemApi.IService;
using TaskManagementSystemApi.Service;

namespace TaskManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
                _projectService = projectService;   
        }
        [HttpPost]
        public async Task<IActionResult> AddTask(ProjectDto projectDto)
        {
            var response = _projectService.AddProjectAsync(projectDto);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetTask(int pageNumber, int pageSize)
        {
            var response = _projectService.GetProjectAsync(pageNumber, pageSize);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdatTask(Guid Id, ProjectDto projectDto)
        {
            var response = _projectService.UpdateProjectAsyncById(Id, projectDto);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTask(Guid Id)
        {
            var response = _projectService.DeleteProjectAsyncById(Id);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
