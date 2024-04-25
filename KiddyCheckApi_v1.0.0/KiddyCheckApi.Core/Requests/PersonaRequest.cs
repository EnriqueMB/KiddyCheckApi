using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyCheckApi.Core.Requests
{
    public class PersonaRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Direccion { get; set; }
        public string? telefono { get; set; }
        public string Correo { get; set; }
        public string? Password { get; set; }
        public int? IdTipoPersona { get; set; }
        public int? Grado { get; set; }

        public int? Grupo { get; set; }
    }
}
