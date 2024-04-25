using KiddyCheckApi.DataAccess.Entities;
using KiddyCheckApi.DataAccess.IRepositories;
using KiddyCheckApi.DataAccess.Response;
using KiddyCheckApi.DataAccess.Response.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KiddyCheckApi.DataAccess.Repositories
{
    public class MateriasRepository : BaseRepository<Materias>, IMateriasRepository
    {
        private readonly KiddyCheckDbContext _context;

        public MateriasRepository(KiddyCheckDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<GenericResponse<Materias>> Agregar(Materias request, List<int> grados, ILogger logger)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Started Success");

                var response = new GenericResponse<Materias>();
                var add = _context.Add(request);

                var addResult = await _context.SaveChangesAsync();

                if (addResult == 1)
                {
                    var Lista = new List<GradoMateria>();
                    foreach (var item in grados)
                    {
                        var registro = new GradoMateria
                        {
                            IdGrado = item,
                            IdMateria = add.Entity.Id
                        };
                        Lista.Add(registro);
                    }

                    var result = _context.AddRangeAsync(Lista);

                    var res = await _context.SaveChangesAsync();

                    if (res == Lista.Count)
                    {
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                        response.Success = true;
                        response.CreatedId = add.Entity.Id.ToString();
                        response.Data = add.Entity;
                        response.Message = "Se agrego la materia correctamente";
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        response.Success = false;
                        response.Message = "No se pudo agregar los grados a la materia";
                    }

                }
                else
                {
                    await transaction.RollbackAsync();
                    response.Success = false;
                    response.Message = "No se pudo agregar la materia";
                }

                logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Finished Success");

                return response;
            }
            catch (SqlException ex)
            {
                await transaction.RollbackAsync();
                logger.LogError(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + ex.Message);
                throw;
            }
        }

        public async Task<GenericResponse<Materias>> Editar(Materias request, List<int> grados, ILogger logger)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Started Success");

                var response = new GenericResponse<Materias>();

                var res = await base.GetById(request.Id, logger);

                if (res != null)
                {
                    res.Nombre = request.Nombre;
                    res.UserUpd = request.UserUpd;
                    res.FechaUpd = request?.FechaUpd;

                    var update = _context.Update(res);

                    var Result = await _context.SaveChangesAsync();

                    if (Result == 1)
                    {

                        var anteriores = await _context.GradoMateria.Where(x=>x.IdMateria == res.Id).ToListAsync();
                        _context.RemoveRange(anteriores);
                        var eliminados = await _context.SaveChangesAsync();

                        if (eliminados == anteriores.Count)
                        {
                            var Lista = new List<GradoMateria>();
                            foreach (var item in grados)
                            {
                                var registro = new GradoMateria
                                {
                                    IdGrado = item,
                                    IdMateria = res.Id
                                };
                                Lista.Add(registro);
                            }

                            var result = _context.AddRangeAsync(Lista);

                            var resul = await _context.SaveChangesAsync();

                            if (resul == Lista.Count)
                            {
                                await transaction.CommitAsync();
                                response.Success = true;
                                response.UpdatedId = res.Id.ToString();
                                response.Data = res;
                                response.Message = "Se modifico la materia correctamente";
                            }
                        }
                        else
                        {
                            await transaction.RollbackAsync();
                            response.Success = false;
                            response.Message = "No se pudo modificar los grados";
                        }
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        response.Success = false;
                        response.Message = "No se pudo modificar la materia";
                    }
                }
                else
                {
                    await transaction.RollbackAsync();
                    response.Success = false;
                    response.Message = "No se encontro la materia";
                }

                logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Finished Success");

                return response;
            }
            catch (SqlException ex)
            {
                await transaction.RollbackAsync();
                logger.LogError(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + ex.Message);
                throw;
            }
        }

        public async Task<Materias> Existe(string nombre, ILogger logger)
        {
            try
            {
                logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Started Success");

                var res = await _context.Materias.Where(x => x.Nombre == nombre).FirstOrDefaultAsync();

                if (res != null)
                {
                    logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Finished Success");
                    return res;
                }
                else
                {
                    logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Finished Success");
                    return null;
                }

            }
            catch (SqlException ex)
            {
                logger.LogError(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + ex.Message);
                throw;
            }
        }

        public async Task<List<MateriasDTO>> Get(ILogger logger)
        {
            try
            {
                logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Started Success");

                var res = await _context.Materias.Where(x=>x.Activo == true).Select(x=> new MateriasDTO
                {
                    Id= x.Id,
                    Nombre = x.Nombre,
                    Grados = _context.GradoMateria.Where(y=>y.IdMateria == x.Id).Select(x=>x.IdGrado).ToList()
                }).ToListAsync();
                    //await from materia in _context.Materias
                    //           join grados in _context.GradoMateria on materia.Id equals grados.IdMateria

                if (res != null)
                {
                    logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Finished Success");
                    return res;
                }
                else
                {
                    logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Finished Success");
                    return null;
                }

            }
            catch (SqlException ex)
            {
                logger.LogError(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + ex.Message);
                throw;
            }
        }
    }
}
