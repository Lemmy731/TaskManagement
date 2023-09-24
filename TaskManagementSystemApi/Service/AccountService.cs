using Microsoft.AspNetCore.Identity;
using TaskManagementApplication.Data;
using TaskManagementDomain.DTO;
using TaskManagementDomain.Entity;
using TaskManagementDomain.Helper;
using TaskManagementSystemApi.IService;

namespace TaskManagementSystemApi.Service
{
    public class AccountService: IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _appDbContext;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appDbContext = appDbContext;
        }

        public async Task<string> Register(UserDto registerDTO)
        {


            var user = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (user != null)
            {
                return "user already exist with this email";
            }

            AppUser appUser = new AppUser()
            {
                UserName = registerDTO.UserName,
                PhoneNumber = registerDTO.PhoneNumber,
                PasswordHash = registerDTO.PassWord,
                FullName = registerDTO.FullName,
                Email = registerDTO.Email
            };

            var userResponse = await _userManager.CreateAsync(appUser, appUser.PasswordHash);
            if (userResponse.Succeeded)
            {
                var roleResponse = await _userManager.AddToRoleAsync(appUser, UserRoles.CustomerRole);

                if (roleResponse.Succeeded)
                {
                    var saveResult = await _appDbContext.SaveChangesAsync();
                }
                return "user and customer created";
            }
            return "unable to create user and customer";
        }
        public async Task<string> Login(LoginDTO loginDTO)
        {

            var user = await _userManager.FindByNameAsync(loginDTO.UserName);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
                if (passwordCheck == false)
                {
                    return "incorrect password";
                }
                if (passwordCheck == true)
                {
                    var signIn = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, false, true);
                    if (signIn.Succeeded)
                    {
                        return "successful signed in";
                    }
                }
            }
            return "user not found";
        }

    }
}

