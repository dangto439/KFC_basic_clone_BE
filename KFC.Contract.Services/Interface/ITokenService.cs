using KFC.Entity;
using KFC.ModelViews.ModelViews.UserModels;

namespace KFC.Contract.Services.Interface
{
    public interface ITokenService
    {
        TokenResponse GenerateTokens(User user, string role);
    }
}
