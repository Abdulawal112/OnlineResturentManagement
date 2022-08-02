﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private IMemoryCache _memoryCache;
        //private ICashHelper<Employee> _cashHelper;
        public UsersController(IUserService userService, ILoggerManager logger,IDataAccessService dataAccessService, IRoleService roleService,IMapper mapper,IMemoryCache memoryCache)
        {
            _userService = userService;
            _logger = logger;
            _dataAccessService = dataAccessService;
            _roleService = roleService;
            _mapper = mapper;
            _memoryCache = memoryCache;
            //_cashHelper = cashHelper;
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {

                
                var userDtos = new List<UserDto>();

                userDtos = (List<UserDto>)await _userService.GetAllUserAsync("");
                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("GetUsersBySearch")]
        public async Task<IActionResult> GetUsersBySearch(string search)
        {
            try
            {

                var userDtos = new List<UserDto>();

                userDtos = (List<UserDto>)await _userService.GetAllUserAsync(search);
                bool isExist = _memoryCache.TryGetValue("users", out IEnumerable<UserDto>userDtos);
                if (!isExist)
                {
                    userDtos =await _userService.GetAllUserAsync();
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(100))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);
                    _memoryCache.Set("users", userDtos, cacheEntryOptions);
                }
               /* userDtos = (List<UserDto>)await _userService.GetAllUserAsync();*/
                return Ok(userDtos);
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
                bool isExist = _memoryCache.TryGetValue("roles", out var roles);
                if (!isExist)
                {
                    roles = await _roleService.GetRoles();
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(100))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);

                    _memoryCache.Set("roles", roles, cacheEntryOptions);
                }
               /* var roles=await _roleService.GetRoles();*/
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
                if(role==null)
                    return BadRequest();
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
                if (role == null)
                    return BadRequest();
                if (await _roleService.IsExistRole(role))
                    return Conflict();
                var updatedRole = await _roleService.UpdateRole(role);
                if (updatedRole.Id != 0)
                {
                    _memoryCache.Remove("roles");
                    return Ok(_mapper.Map<RoleDto>(updatedRole));
                }
                   
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
            if (userId <= 0)
                return BadRequest();
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
                _memoryCache.Remove("users");
                return Ok(response);
            }
            return StatusCode(400);
        }
        [HttpGet("GetAllMenu")]
        public async Task<ActionResult> GetAllMenu()
        {
            var response = await _roleService.GetMenus();
            if (response != null)
            {
                return Ok(response);
            }
            return StatusCode(400);
        }
        [HttpGet("RoleWiseMenus")]
        public async Task<ActionResult<IEnumerable<NavigationMenuDto>>>GetNavigationMenus(int roleId)
        {
            if (roleId <= 0)
                return BadRequest();
            var response = await _roleService.GetNavigationManus(roleId);
            if(response != null)
            {
                return Ok(response);
            }
            return StatusCode(400);
        }

        [HttpPut("UpdateRoleMenu")]
        public async Task<ActionResult<NavigationMenuDto>>UpdateRoleMenu(int roleId,List<NavigationMenuDto>menus)
        {
            if (roleId <= 0 || menus.Count == 0)
                return BadRequest();
            var response = await _roleService.UpdateNavigationMenu(menus, roleId);
            if(response != null)
            {
                return Ok(response);
            }
            return StatusCode(400);
        }

        [HttpGet("GetMenusByUser")]
        public async Task<ActionResult>GetMenusByUser(string name)
        {
            if (name == "")
                return BadRequest();
            var response = await _userService.GetUsersNavMenus(name);
            if(response != null)
            {
                return Ok(_mapper.Map<List<NavigationMenuDto>>(response));
            }
            return StatusCode(400);
        }


        //For User Profile
        [HttpGet("UserByName")]
        public async Task<ActionResult<UserDto>> GetUserByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            var response = await _userService.GetUserByName(name);
            if (response == null)
            {
                return StatusCode(400);
            }
            return Ok(_mapper.Map<UserDto>(response));
        }

        //[HttpPut("UpdateUserProfile")]
        //public async Task<ActionResult<UserDto>> UpdateUserProfile(UserDto userDto)
        //{
        //    if (userDto == null)
        //        return BadRequest();

        //    var response = await _userService.UpdateUserWithRole(userDto);
        //    if (response != null)
        //    {
        //        return Ok(response);
        //    }
        //    return StatusCode(400);
        //}

    }
}
