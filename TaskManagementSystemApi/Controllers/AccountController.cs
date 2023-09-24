using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementDomain.DTO;
using TaskManagementSystemApi.IService;

namespace TaskManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
                _accountService = accountService;   
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                var response = await _accountService.Register(userDto);
                if(response == "user and customer created")
                {
                    return Ok(response);
                }
                return BadRequest(response);    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]  
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            try
            {
                var response = await _accountService.Login(loginDto);
                if (response == "successful signed in")
                {
                    return Ok(response);
                }
                return BadRequest(response
               );
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}"); 
            }
        }
    }
}
