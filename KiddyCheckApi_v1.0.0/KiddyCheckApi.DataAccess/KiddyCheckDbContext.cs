using KiddyCheckApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyCheckApi.DataAccess
{
    public class KiddyCheckDbContext : DbContext
    {
        public KiddyCheckDbContext(DbContextOptions<KiddyCheckDbContext> options) : base(options)
        {
        }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Permisos> Permisos { get; set; }
        public DbSet<UsuarioPermisos> UsuarioPermisos { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UsuariosRoles> UsuariosRoles { get; set; }
        public DbSet<RolesPermisos> RolesPermisos { get; set; }
        public DbSet<Grados> Grados { get; set; }
        public DbSet<Materias> Materias { get; set; }
        public DbSet<GradoMateria> GradoMateria { get; set; }
    }
}
