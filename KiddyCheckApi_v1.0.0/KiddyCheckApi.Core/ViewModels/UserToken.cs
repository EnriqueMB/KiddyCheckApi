

namespace KiddyCheckApi.Core.ViewModels
{
    public class UserToken
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public int Id { get; set; }
        public int PerfilId { get; set; }
        public List<long> Permisos { get; set; }
        public List<int> Roles { get; set; }
    }
}
