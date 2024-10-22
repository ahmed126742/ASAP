using System.Data;
using ASAP.Application.Features.Users.GetUser;
using ASAP.Application.Services;
using ASAP.Infrastructure.Services.User;
using ASAP_Task.WebAPI.Authentication.Context;
using ASAP_Task.WebAPI.Authentication.Enums;
using ASAP_Task.WebAPI.Authentication.Extensions;
using ASAP_Task.WepApi.Authentication.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASAP_Task.WebAPI.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleManagerController : ControllerBase
    {
        private readonly AuthContext _authContext;
        private readonly UserApplicationManager _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RoleManagerController> _logger;
        private readonly IUserService _userService;
        public RoleManagerController(
            AuthContext authContext,
            UserApplicationManager userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RoleManagerController> logger,
            IUserService userService)
        {
            _authContext = authContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _roleManager.Roles.ToListAsync();
            return Ok(result);
        }

        [HttpGet("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("User does not exist!");

            var result = await _userManager.GetRolesAsync(user);
            return Ok(result);
        }

        [HttpGet("GetUsersByRoles")]
        public async Task<ActionResult<IList<GetUserRsponse>>> GetUsersByRoles(RoleEnum roleType, CancellationToken cancellationToken)
        {
            var role = roleType.GetEnumDescription();
            var applicationUsers = await _userManager.GetUsersInRoleAsync(role);
            if (!applicationUsers.Any())
                throw new Exception("NO users!");

            var result = await _userService.GetUsersByEmails( applicationUsers.Select(x=>x.Email).ToList() , cancellationToken);
            return Ok(result);
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(string name)
        {
            var roleExistence = await _roleManager.RoleExistsAsync(name);
            if (roleExistence)
                throw new Exception("Role already exist!");

            var role = await _roleManager.CreateAsync(new IdentityRole { Name = name });
            if (!role.Succeeded)
                throw new Exception(role.Errors.Select(x => x.Description).First());

            return Ok("Succeeded");
        }

        [HttpPost("CreateUserRole")]
        public async Task<IActionResult> CreateUserRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("User does not exist!");

            var isRoleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!isRoleExist)
                throw new Exception("Role does not exist!");

            var isUserInRole = await _userManager.IsInRoleAsync(user, roleName);
            if (isUserInRole)
                throw new Exception("Role already assigned to this user!");

            var userRoleResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!userRoleResult.Succeeded)
                throw new Exception(userRoleResult.Errors.Select(x => x.Description).First());

            return Ok("Succeeded");
        }

        [HttpPost("DeleteUserRole")]
        public async Task<IActionResult> DeleteUserRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("User does not exist!");

            var isRoleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!isRoleExist)
                throw new Exception("Role does not exist!");

            var isUserInRole = await _userManager.IsInRoleAsync(user, roleName);
            if (!isUserInRole)
                throw new Exception("Role did not assigned to this user!");

            var userRoleResult = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (!userRoleResult.Succeeded)
                throw new Exception(userRoleResult.Errors.Select(x => x.Description).First());

            return Ok("Removed Successfully");
        }
    }
}
