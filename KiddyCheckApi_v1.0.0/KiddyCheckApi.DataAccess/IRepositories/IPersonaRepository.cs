using KiddyCheckApi.DataAccess.Entities;
using KiddyCheckApi.DataAccess.Response;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyCheckApi.DataAccess.IRepositories
{
    public interface IPersonaRepository : IBaseRepository<Personas>
    {
        Task<GenericResponse<Personas>> AgregarPersona(Personas persona, ILogger logger);
        Task<GenericResponse<Personas>> ActualizarPersona(Personas persona, ILogger logger);
        Task<GenericResponse<Personas>> EliminarPersona(int id, int userMod, ILogger logger);
        Task<GenericResponse<List<Personas>>> ObtenerPersonas(ILogger Logger);
    }
    
}
