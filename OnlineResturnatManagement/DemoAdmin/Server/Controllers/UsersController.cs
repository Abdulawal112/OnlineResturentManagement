using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using MOnlineResturnatManagement.Server.Services.RoleService;
using OnlineResturnatManagement.Server.Helper;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Server.Services.IService;
using OnlineResturnatManagement.Shared.DTO;
using static OnlineResturnatManagement.Server.Helper.Permissions;
using User = OnlineResturnatManagement.Server.Models.User;

namespace OnlineResturnatManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
       
        private IUserService _userService;
        private ILoggerManager _logger;
        private IDataAccessService _dataAccessService;
        private IRoleService _roleService;
        private IMapper _mapper;
        //private ICashHelper<Employee> _cashHelper;
        public UsersController(IUserService userService, ILoggerManager logger,IDataAccessService dataAccessService, IRoleService roleService,IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _dataAccessService = dataAccessService;
            _roleService = roleService;
            _mapper = mapper;
            //_cashHelper = cashHelper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                
                var userDtos = new List<UserDto>();

                userDtos = (List<UserDto>)await _userService.GetAllUserAsync();
                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("GetUserMenus")]
        public async Task<IActionResult> GetUserMenus(string id)
        {
            try
            {
                var result = "";

                return Ok(result);
                 //employeeList = _cashHelper.GetDataAsync(cacheKey).Result;

                 //if (employeeList.Count <= 0)
                 //{
                 //    employeeList = (List<Employee>)await _employeeService.GetAllEmployeeAsync();

                 //    _cashHelper.SetDataAsync(cacheKey, employeeList);

                 //}
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
               var roles=await _roleService.GetRoles();
                if(roles != null)
                {
                    return Ok(roles);
                }

                return NoContent();
               

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllRoles action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("CreateRole")]
        public async Task<IActionResult> AddRole([FromBody] RoleDto roleDto)
        {
            try
            {
                var role =_mapper.Map<Role>(roleDto);
                if (await _roleService.IsExistRole(role))
                    return Conflict();
                var newRole = await _roleService.UpdateRole(role);
                if (newRole.Id !=0)
                    return Created("created",_mapper.Map<RoleDto>(newRole));
                else
                    return BadRequest();


            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside AddRole action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("UpdateRole")]
        public async Task<IActionResult> EditRole([FromBody] RoleDto roleDto)
        {
            try
            {
                var role = _mapper.Map<Role>(roleDto);
                if (await _roleService.IsExistRole(role))
                    return Conflict();
                var updatedRole = await _roleService.UpdateRole(role);
                if (updatedRole.Id != 0)
                    return Ok(_mapper.Map<RoleDto>(updatedRole));
                else
                    return BadRequest();


            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside EditRole action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpGet("user")]
        public async Task<ActionResult<UserDto>> GetUser(int userId)
        {
            var response = await _userService.GetUser(userId);
            if (response == null)
            {
                return StatusCode(400);
            }
                return Ok(response);
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult<UserDto>>UpdateUser(UserDto userDto)
        {
            if (userDto == null)
                return BadRequest();
            
            var response = await _userService.UpdateUserWithRole(userDto);
            if(response != null)
            {
                return Ok(response);
            }
            return StatusCode(400);
        }

        [HttpGet("RoleWiseMenus")]
        public async Task<ActionResult<IEnumerable<NavigationMenuDto>>>GetNavigationMenus(int roleId)
        {
            var response = await _roleService.GetNavigationManus(roleId);
            if(response != null)
            {
                return Ok(response);
            }
            return StatusCode(400);
        }

        [HttpPost("UpdateMenuByRole")]
        public async Task<ActionResult<NavigationMenuDto>>UpdateRoleMenu(List<NavigationMenuDto>menus, int roleId)
        {
            var response = await _roleService.UpdateNavigationMenu(menus, roleId);
            if(response != null)
            {
                return Ok(response);
            }
            return StatusCode(400);
        }
    }
}
