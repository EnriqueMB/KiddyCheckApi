using KiddyCheckApi.DataAccess.Entities;
using KiddyCheckApi.DataAccess.IRepositories;
using KiddyCheckApi.DataAccess.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KiddyCheckApi.DataAccess.Repositories
{
    public class PersonaRepository: BaseRepository<Personas>,IPersonaRepository
    {
        private readonly KiddyCheckDbContext _context;
        public PersonaRepository(KiddyCheckDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<GenericResponse<Personas>> AgregarPersona(Personas persona, ILogger logger)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Started Success");

                var response = new GenericResponse<Personas>();

                var add = _context.Add(persona);

                var addResult = await _context.SaveChangesAsync();

                if (addResult > 0)
                {
                    
                    await transaction.CommitAsync();
                    response.Success = true;
                    response.CreatedId = add.Entity.Id.ToString();
                    response.Data = add.Entity;
                 
                }
                else
                {
                    await transaction.RollbackAsync();
                    response.Success = false;
                    response.Message = "No se pudo agregar la persona";
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


        //Obtener todos los usuarios
        public async Task<GenericResponse<List<Personas>>> ObtenerPersonas(ILogger logger)
        {
            try
            {
                logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Started Success");

                var response = new GenericResponse<List<Personas>>();

                var personas = await EntitySet.AsNoTracking().Where(x => x.Activo == true).ToListAsync();

                response.Success = true;
                response.Data = personas;

                logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Finished Success");

                return response;
            }
            catch (SqlException ex)
            {
                logger.LogError(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + ex.Message);
                throw;
            }
        }

        public async Task<GenericResponse<Personas>> ActualizarPersona(Personas persona,ILogger logger)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Started Success");

                var response = new GenericResponse<Personas>();

                var usr = await base.GetById(persona.Id, logger);

                if (usr != null)
                {
                    usr.Nombre = persona.Nombre;
                    usr.ApellidoPaterno = persona.ApellidoPaterno;
                    usr.ApellidoMaterno = persona.ApellidoMaterno;
                    usr.PasswordHash = persona.PasswordHash;
                    usr.PasswordSalt = persona.PasswordSalt;
                    usr.Direccion = persona.Direccion;
                    usr.telefono = persona.telefono;
                    usr.Correo = persona.Correo;
                    usr.Grado = persona.Grado;
                    usr.Grupo=persona.Grupo; ;
                    usr.UserUpd = persona.UserUpd;
                    usr.FechaUpd = persona.FechaUpd;
                    usr.Activo = persona.Activo;

                    var update = _context.Update(usr);

                    var updateResult = await _context.SaveChangesAsync();

                    if (updateResult == 1)
                    {

                        await transaction.CommitAsync();
                        response.Success = true;
                        response.UpdatedId = usr.Id.ToString();
                        response.Data = usr;
                        response.Message = "Se modifico la persona correctamente";
                    
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        response.Success = false;
                        response.Message = "No se pudo modificar la persona";
                    }
                }
                else
                {
                    await transaction.RollbackAsync();
                    response.Success = false;
                    response.Message = "No se encontro la persona";
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

        //Eliminar Personas
        public async Task<GenericResponse<Personas>> EliminarPersona(int id, int userMod, ILogger logger)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Started Success");

                var response = new GenericResponse<Personas>();

                var usr = await base.GetById(id, logger);

                if (usr != null)
                {
                    usr.Activo = false;
                    usr.FechaUpd = DateTime.UtcNow.AddHours(-6);
                    usr.UserUpd = userMod;

                    var update = _context.Update(usr);

                    var updateResult = await _context.SaveChangesAsync();

                    if (updateResult == 1)
                    {
                        await transaction.CommitAsync();
                        response.Success = true;
                        response.UpdatedId = usr.Id.ToString();
                        response.Data = usr;
                        response.Message = "Se elimino la persona correctamente";
                       
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        response.Success = false;
                        response.Message = "No se pudo eliminar la persona";
                    }
                }
                else
                {
                    await transaction.RollbackAsync();
                    response.Success = false;
                    response.Message = "No se encontro el usuario";
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
    }
}
