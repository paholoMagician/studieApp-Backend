﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public partial class vinculacionUgContext
    {
        private IvinculacionUgContextProcedures _procedures;

        public virtual IvinculacionUgContextProcedures Procedures
        {
            get
            {
                if (_procedures is null) _procedures = new vinculacionUgContextProcedures(this);
                return _procedures;
            }
            set
            {
                _procedures = value;
            }
        }

        public IvinculacionUgContextProcedures GetProcedures()
        {
            return Procedures;
        }

        protected void OnModelCreatingGeneratedProcedures(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreateAccountResult>().HasNoKey().ToView(null);
            modelBuilder.Entity<ObtenerAlumnoRegistrosResult>().HasNoKey().ToView(null);
            modelBuilder.Entity<ObtenerAlumnosResult>().HasNoKey().ToView(null);
            modelBuilder.Entity<ObtenerConveniosResult>().HasNoKey().ToView(null);
            modelBuilder.Entity<ObtenerEntidadesResult>().HasNoKey().ToView(null);
            modelBuilder.Entity<ObtenerGrupoAlumnoResult>().HasNoKey().ToView(null);
            modelBuilder.Entity<ObtenerInstitutosResult>().HasNoKey().ToView(null);
            modelBuilder.Entity<ObtenerPersonalVinculacionResult>().HasNoKey().ToView(null);
            modelBuilder.Entity<ObtenerProcesosResult>().HasNoKey().ToView(null);
            modelBuilder.Entity<ObtenerProyectosResult>().HasNoKey().ToView(null);
            modelBuilder.Entity<ObtenerRegistrosActividadesResult>().HasNoKey().ToView(null);
            modelBuilder.Entity<ValidacionAlumnoProyectoResult>().HasNoKey().ToView(null);
        }
    }

    public partial class vinculacionUgContextProcedures : IvinculacionUgContextProcedures
    {
        private readonly vinculacionUgContext _context;

        public vinculacionUgContextProcedures(vinculacionUgContext context)
        {
            _context = context;
        }

        public virtual async Task<List<CreateAccountResult>> CreateAccountAsync(int? tipo, string codUser, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "tipo",
                    Value = tipo ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                new SqlParameter
                {
                    ParameterName = "codUser",
                    Size = 20,
                    Value = codUser ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<CreateAccountResult>("EXEC @returnValue = [dbo].[CreateAccount] @tipo, @codUser", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<int> EliminarInstitutoAsync(string codInstituto, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "codInstituto",
                    Size = 35,
                    Value = codInstituto ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[EliminarInstituto] @codInstituto", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<int> EliminarRegistroAlumnoAsync(string codAlumno, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "codAlumno",
                    Size = 30,
                    Value = codAlumno ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[EliminarRegistroAlumno] @codAlumno", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<ObtenerAlumnoRegistrosResult>> ObtenerAlumnoRegistrosAsync(string idEstudiantes, string codCia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "idEstudiantes",
                    Size = 100,
                    Value = idEstudiantes ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "codCia",
                    Size = 25,
                    Value = codCia ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<ObtenerAlumnoRegistrosResult>("EXEC @returnValue = [dbo].[ObtenerAlumnoRegistros] @idEstudiantes, @codCia", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<ObtenerAlumnosResult>> ObtenerAlumnosAsync(string codCia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "codCia",
                    Size = 15,
                    Value = codCia ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<ObtenerAlumnosResult>("EXEC @returnValue = [dbo].[ObtenerAlumnos] @codCia", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<ObtenerConveniosResult>> ObtenerConveniosAsync(string codCia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "codCia",
                    Size = 150,
                    Value = codCia ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<ObtenerConveniosResult>("EXEC @returnValue = [dbo].[ObtenerConvenios] @codCia", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<ObtenerEntidadesResult>> ObtenerEntidadesAsync(string tipo, string codAgente, string codCia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "tipo",
                    Size = 35,
                    Value = tipo ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "codAgente",
                    Size = 50,
                    Value = codAgente ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "codCia",
                    Size = 20,
                    Value = codCia ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<ObtenerEntidadesResult>("EXEC @returnValue = [dbo].[ObtenerEntidades] @tipo, @codAgente, @codCia", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<ObtenerGrupoAlumnoResult>> ObtenerGrupoAlumnoAsync(string tipo, string codGrupo, string codCia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "tipo",
                    Size = 100,
                    Value = tipo ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "codGrupo",
                    Size = 100,
                    Value = codGrupo ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "codCia",
                    Size = 50,
                    Value = codCia ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<ObtenerGrupoAlumnoResult>("EXEC @returnValue = [dbo].[ObtenerGrupoAlumno] @tipo, @codGrupo, @codCia", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<ObtenerInstitutosResult>> ObtenerInstitutosAsync(string codCia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "codCia",
                    Size = 15,
                    Value = codCia ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<ObtenerInstitutosResult>("EXEC @returnValue = [dbo].[ObtenerInstitutos] @codCia", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<ObtenerPersonalVinculacionResult>> ObtenerPersonalVinculacionAsync(string ccia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "ccia",
                    Size = 20,
                    Value = ccia ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<ObtenerPersonalVinculacionResult>("EXEC @returnValue = [dbo].[ObtenerPersonalVinculacion] @ccia", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<ObtenerProcesosResult>> ObtenerProcesosAsync(string codcia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "codcia",
                    Size = 20,
                    Value = codcia ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<ObtenerProcesosResult>("EXEC @returnValue = [dbo].[ObtenerProcesos] @codcia", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<ObtenerProyectosResult>> ObtenerProyectosAsync(string ccia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "ccia",
                    Size = 50,
                    Value = ccia ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<ObtenerProyectosResult>("EXEC @returnValue = [dbo].[ObtenerProyectos] @ccia", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<ObtenerRegistrosActividadesResult>> ObtenerRegistrosActividadesAsync(string idAlumno, string ccia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "idAlumno",
                    Size = 40,
                    Value = idAlumno ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "ccia",
                    Size = 10,
                    Value = ccia ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<ObtenerRegistrosActividadesResult>("EXEC @returnValue = [dbo].[ObtenerRegistrosActividades] @idAlumno, @ccia", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<ValidacionAlumnoProyectoResult>> ValidacionAlumnoProyectoAsync(string usercod, string idGrupo, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "usercod",
                    Size = 25,
                    Value = usercod ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "idGrupo",
                    Size = 25,
                    Value = idGrupo ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<ValidacionAlumnoProyectoResult>("EXEC @returnValue = [dbo].[ValidacionAlumnoProyecto] @usercod, @idGrupo", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }
    }
}
