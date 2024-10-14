using Microsoft.AspNetCore.Mvc;
using KFC.Contract.Services.Interface;
using KFC.Core.Base;
using KFC.Core.Constants;
using KFC.ModelViews.UserModels;
namespace KFC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        /// <summary>
        /// Đăng nhập.
        /// </summary>
        //[HttpPost]
        //[Route(EndPointAPI.Auth.LoginEndPoint)]
        //public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        //{
        //    return Ok(BaseResponseModel<LoginResponse>.OkResponseModel(await _authService.LoginAsync(request), "Đăng nhập thành công"));
        //}
        ///// <summary>
        ///// Xem thông tin tài khoản hiện tại.
        ///// </summary>
        //[HttpGet]
        //[Route(EndPointAPI.Auth.Info)]
        //public async Task<IActionResult> GetInfo()
        //{
        //    return Ok(BaseResponseModel<ResponseUserModel>.OkResponseModel(await _authService.GetInfo(), "Lấy thông tin tài khoản thành công"));
        //}
    }
}
