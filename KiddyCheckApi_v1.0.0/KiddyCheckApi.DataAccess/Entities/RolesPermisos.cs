using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyCheckApi.DataAccess.Entities
{
    public class RolesPermisos
    {
        [Key]
        public int Id { get; set; }
        public int IdRol { get; set; }
        public long IdPermiso { get; set; }
    }
}
