using AutoMapper;
using KiddyCheckApi.Core.Requests;
using KiddyCheckApi.Core.Response;
using KiddyCheckApi.Core.ViewModels;
using KiddyCheckApi.DataAccess.Entities;
using KiddyCheckApi.DataAccess.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyCheckApi.Core.Mappers
{
    public class AppMapper
    {
        private static readonly Mapper _mapper;

        static AppMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UsuariosVM, Usuarios>().ReverseMap();
                cfg.CreateMap<UsuarioRequest, Usuarios>().ReverseMap();
                cfg.CreateMap<PermisosVM, Permisos>().ReverseMap();
                cfg.CreateMap<UsuarioPermisosVM, UsuarioPermisos>().ReverseMap();
                cfg.CreateMap<UsuariosResponse, Usuarios>().ReverseMap();
                cfg.CreateMap<Materias, MateriasVM>().ReverseMap();
                cfg.CreateMap<MateriasDTO, MateriasVM>().ReverseMap();
                cfg.CreateMap<MateriasRequest, Materias>().ReverseMap();
            });

            _mapper = new Mapper(config);
        }

        public static TDestination Map<TSource, TDestination>(TSource obj)
        {
            return _mapper.Map<TSource, TDestination>(obj);
        }
    }
}
