using KiddyCheckApi.Core.ViewModels;

namespace KiddyCheckApi.Core.Requests
{
    public class UsuarioRequest
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime UltimoAcceso { get; set; }
        public List<UsuarioPermisosVM> Permisos { get; set; }
    }
}
