namespace KFC.Core.Constants
{
    public static class EndPointAPI
    {
        public const string AreaName = "api";
        public const string AreaNameV2 = "apiV2";
        public static class Auth
        {
            private const string BaseEndpoint = "~/" + AreaName + "/auth";
            public const string LoginEndPoint = BaseEndpoint + "/login";
            public const string Info = BaseEndpoint + "/info";
          
        }
       
    }
   
}
