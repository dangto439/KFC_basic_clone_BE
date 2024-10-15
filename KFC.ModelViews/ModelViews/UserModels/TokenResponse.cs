namespace KFC.ModelViews.ModelViews.UserModels
{
    public class TokenResponse
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public ResponseUserModels User { get; set; }
    }
}
