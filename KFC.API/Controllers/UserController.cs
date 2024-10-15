using KFC.Contract.Services.Interface;
using KFC.Core.Base;
using KFC.Core.Constants;
using KFC.ModelViews.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace KFC.API.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;
        public UserController(IUser authService)
        {
            _userService = authService;
        }

        [HttpPost]
        [Route(EndPointAPI.Auth.LoginEndPoint)]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            var result = await _userService.LoginAsync(request);
            return Ok(BaseResponseModel<LoginResponse>.OkDataResponse(result, "Lấy toàn ca làm việc thành công"));
        }
    }
}
