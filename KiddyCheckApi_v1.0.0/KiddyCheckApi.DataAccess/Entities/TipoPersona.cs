using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyCheckApi.DataAccess.Entities
{
    public class TipoPersona
    {
        [Key]
        public int Id { get; set; }
        public string nombre { get; set; }
    }
}
