
using KFC.ModelViews.ModelViews.UserModels;

namespace KFC.ModelViews.UserModels
{
    public class LoginResponse
    {
        public TokenResponse TokenResponse { get; set; }
        public string Role { get; set; }
        public string token { get; set; }
    }
}