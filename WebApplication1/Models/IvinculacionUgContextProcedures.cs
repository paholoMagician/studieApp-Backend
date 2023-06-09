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
    public partial interface IvinculacionUgContextProcedures
    {
        Task<List<CreateAccountResult>> CreateAccountAsync(int? tipo, string codUser, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> EliminarInstitutoAsync(string codInstituto, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> EliminarRegistroAlumnoAsync(string codAlumno, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ObtenerAlumnoRegistrosResult>> ObtenerAlumnoRegistrosAsync(string idEstudiantes, string codCia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ObtenerAlumnosResult>> ObtenerAlumnosAsync(string codCia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ObtenerConveniosResult>> ObtenerConveniosAsync(string codCia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ObtenerEntidadesResult>> ObtenerEntidadesAsync(string tipo, string codAgente, string codCia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ObtenerGrupoAlumnoResult>> ObtenerGrupoAlumnoAsync(string tipo, string codGrupo, string codCia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ObtenerInstitutosResult>> ObtenerInstitutosAsync(string codCia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ObtenerPersonalVinculacionResult>> ObtenerPersonalVinculacionAsync(string ccia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ObtenerProcesosResult>> ObtenerProcesosAsync(string codcia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ObtenerProyectosResult>> ObtenerProyectosAsync(string ccia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ObtenerRegistrosActividadesResult>> ObtenerRegistrosActividadesAsync(string idAlumno, string ccia, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ValidacionAlumnoProyectoResult>> ValidacionAlumnoProyectoAsync(string usercod, string idGrupo, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
    }
}
