

namespace KiddyCheckApi.Core.Response
{
    public class AuthResponse
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public List<long> Permisos { get; set; }
        public List<int> Roles { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
