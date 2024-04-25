using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyCheckApi.Core.Requests
{
    public class PagginGenericRequest
    {
        public string Buscar { get; set; }
        public int Page { get; set; }
        public int Take { get; set; }
        public int Skip
        {
            get
            {
                return (Page - 1) * Take;
            }
        }
    }
}
