using KFC.ModelViews.UserModels;
namespace KFC.Contract.Services.Interface
{
    public interface IAuthService
    {
        //Task<ResponseUserModel> GetInfo();
        Task<LoginResponse> LoginAsync(LoginRequestModel request);
        // Phương thức kiểm tra tài khoản hiện tại có bị xóa hay không. Trong trường hợp đăng trong phiên nhập thì bị xóa tài khoản 
       Task CheckUserAccountStatusAsync();
    }
}
