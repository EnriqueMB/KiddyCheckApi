using KiddyCheckApi.DataAccess.Entities;
using KiddyCheckApi.DataAccess.Response;
using KiddyCheckApi.DataAccess.Response.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyCheckApi.DataAccess.IRepositories
{
    public interface IMateriasRepository :IBaseRepository<Materias>
    {
        Task<GenericResponse<Materias>> Agregar(Materias request, List<int> grados, ILogger logger);
        Task<GenericResponse<Materias>> Editar(Materias request, List<int> grados, ILogger logger);
        Task<Materias> Existe(string nombre, ILogger logger);
        Task<List<MateriasDTO>> Get(ILogger logger);
    }
}
