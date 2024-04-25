using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyCheckApi.DataAccess.Entities
{
    public class Personas
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Direccion { get; set; }
        public string? telefono { get; set; }
        public string? Correo { get; set; }
        public int? IdTutor { get; set; }
        public int? IdTipoPersona {  get; set; }
        public int? Grado { get; set; }

        public int? Grupo { get; set; }

        public int? UserAlt { get; set; }
        public DateTime? FechaAlt { get; set; }
        public int? UserUpd { get; set; }
        public DateTime? FechaUpd { get; set; }
        public bool? Activo { get; set; }

    }
}
