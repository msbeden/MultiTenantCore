using Microsoft.AspNetCore.Mvc;
using Multiple.Models.ViewModels.User;
using Multiple.Services.Abstractions.User;

namespace Multiple.Controllers.UserControllers
{
    [ApiExplorerSettings(GroupName = "UserControllers")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        readonly IUsersService _userService;
        public UserController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
            => Ok(await _userService.GetAllAsnyc());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
            => Ok(await _userService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UsersViewModel userViewModel)
            => Ok(await _userService.CreateAsync(userViewModel.NameSurname, userViewModel.UserName, userViewModel.Password, userViewModel.ConnectionString, userViewModel.TenantId));
    }
}
