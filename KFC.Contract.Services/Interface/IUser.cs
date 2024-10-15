
using KFC.ModelViews.ModelViews.UserModels;
using KFC.ModelViews.UserModels;

namespace KFC.Contract.Services.Interface
{
    public interface IUser
    {
        ResponseUserModels GetById(string id);
        List<ResponseUserModels> GetAll();
        void Create(ResponseUserModels model);
        void Update(ResponseUserModels model);
        void DeleteById(string id);

        Task<LoginResponse> LoginAsync(LoginRequestModel request);
    }
}
