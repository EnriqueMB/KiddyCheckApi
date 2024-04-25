using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyCheckApi.Core.Requests
{
    public class MateriasRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<int> Grados { get; set; }
    }
}
