using KiddyCheckApi.Core.Helpers;
using KiddyCheckApi.Core.Mappers;
using KiddyCheckApi.Core.Requests;
using KiddyCheckApi.Core.Response;
using KiddyCheckApi.Core.ViewModels;
using KiddyCheckApi.DataAccess.Entities;
using KiddyCheckApi.DataAccess.IRepositories;
using KiddyCheckApi.DataAccess.Response.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KiddyCheckApi.Core.Services
{
    public class MateriasService
    {
        #region Variables
        private readonly IMateriasRepository _repository;
        private ILogger<MateriasService> _logger;
        #endregion

        #region Constructor
        public MateriasService(IMateriasRepository repository, ILogger<MateriasService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        #endregion

        #region Metodos
        public async Task<GenericResponse> Agregar(MateriasRequest request, int id)
        {
            try
            {
                _logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Started Success");

                var response = new GenericResponse();


                var existe = await _repository.Existe(request.Nombre, _logger);

                if (existe != null)
                {
                    if (existe.Activo == true)
                    {
                        response.Message = "La materia que intenta agregar ya se encuestra registrado.";
                        response.Success = false;

                        _logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Finished Success");

                        return response;
                    }
                    else
                    {
                        existe.Activo = true;
                        existe.UserUpd = id;
                        existe.FechaUpd = HoraHelper.GetHora("mx");

                        var results = await _repository.Editar(existe, request.Grados, _logger);

                        if (results.Success)
                        {
                            response.Message = "Materia agregado correctamente";
                            response.Success = true;
                            response.UpdatedId = results.UpdatedId;
                        }
                        return response;
                    }

                }

                var data = AppMapper.Map<MateriasRequest, Materias>(request);
                data.Activo = true;
                data.UserAlt = id;
                data.FechaAlt = HoraHelper.GetHora("mx");

                var result = await _repository.Agregar(data, request.Grados, _logger);

                if (result.Success)
                {
                    response.Message = "Materia agregado correctamente.";
                    response.Success = true;
                    response.CreatedId = result.CreatedId;
                }

                _logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Finished Success");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + ex.Message);
                throw;
            }
        }

        public async Task<GenericResponse> Editar(MateriasRequest request, int id)
        {
            try
            {
                _logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Started Success");

                var response = new GenericResponse();

                var exist = await _repository.GetById(request.Id, _logger);

                if (exist == null)
                {
                    response.Message = "La materia no existe";
                    response.Success = false;

                    _logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Finished Success");

                    return response;
                }

                var existe = await _repository.Existe(request.Nombre, _logger);

                if (existe != null && existe.Id != request.Id)
                {
                    response.Message = "La materia ya se encuestra registrado.";
                    response.Success = false;

                    _logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Finished Success");

                    return response;
                }

                var data = AppMapper.Map<MateriasRequest, Materias>(request);
                data.Activo = true;
                data.UserUpd = id;
                data.FechaUpd = HoraHelper.GetHora("mx");

                var result = await _repository.Editar(data, request.Grados, _logger);

                if (result.Success)
                {
                    response.Message = "Materia actualizado correctamente.";
                    response.Success = true;
                    response.CreatedId = result.CreatedId;
                }
                else
                {
                    response.Message = result.Message;
                    response.Success = false;
                }

                _logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Finished Success");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + ex.Message);
                throw;
            }
        }

        public async Task<ListResponse<MateriasVM>> Listar()
        {
            try
            {
                _logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Started Success");

                var response = new ListResponse<MateriasVM>();

                var lista = await _repository.Get(_logger);
                if (lista != null)
                {
                    var ListaVM = AppMapper.Map<List<MateriasDTO>, List<MateriasVM>>(lista);
                    response.Data = ListaVM;
                }
                else
                {
                    response.Data = new List<MateriasVM>();
                }

                _logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Finished Success");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + ex.Message);
                throw;
            }
        }

        public async Task<MateriasVM> ObtenerXId(int id)
        {
            try
            {
                _logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Started Success");

                var response = new MateriasVM();

                var lista = await _repository.GetById(id, _logger);
                if (lista != null)
                {
                    response = AppMapper.Map<Materias, MateriasVM>(lista);
                }
                else
                {
                    response = new MateriasVM();
                }

                _logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Finished Success");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + ex.Message);
                throw;
            }
        }

        public async Task<GenericResponse> Eliminar(int id, int user)
        {
            try
            {
                _logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Started Success");

                var response = new GenericResponse();

                var lista = await _repository.GetById(id, _logger);

                if (lista != null)
                {
                    lista.Activo = false;
                    lista.UserUpd = user;
                    lista.FechaUpd = HoraHelper.GetHora("mx");

                    var update = await _repository.Update(lista, _logger);

                    if (update != null)
                    {
                        response.Success = true;
                        response.Message = "Materia eliminado correctamente";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "No se pudo eliminar la materia";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "La materia no existe";
                }

                _logger.LogInformation(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + "Finished Success");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(MethodBase.GetCurrentMethod().DeclaringType.DeclaringType.Name + ex.Message);
                throw;
            }
        }
        #endregion
    }
}
