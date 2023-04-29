using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/GrupoAlumnos")]
    [ApiController]
    public class GrupoAlumnosController : ControllerBase
    {

        private readonly vinculacionUgContext _context;
        public GrupoAlumnosController(vinculacionUgContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("guardarGrupoAlumno")]
        public async Task<IActionResult> guardarGrupoAlumno([FromBody] GrupoEstudiante model)
        {
            if (ModelState.IsValid)
            {
                _context.GrupoEstudiante.Add(model);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok(model);
                }
                else
                {
                    return BadRequest("Datos incorrectos");
                }
            }
            else
            {
                return BadRequest("ERROR");
            }
        }

        [HttpPut]
        [Route("EditarGrupo/{id}")]
        public async Task<IActionResult> EditarGrupo([FromRoute] int id, [FromBody] GrupoEstudiante model)
        {

            if (id != model.Id)
            {
                return BadRequest("No existe la grupo");
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(model);

        }

        [HttpGet("ObtenerGrupo/{tp}/{cGrupo}/{ccia}")]
        public async Task<IActionResult> ObtenerGrupo([FromRoute] string tp, [FromRoute] string cGrupo, [FromRoute] string ccia)
        {

            string Sentencia = " exec ObtenerGrupoAlumno @tipo, @codGrupo, @codCia ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@tipo",     tp));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@codGrupo", cGrupo));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@codCia",   ccia));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se ha podido crear...");
            }

            return Ok(dt);

        }   
        

        [HttpGet("ObtenerAlumnoSinGrupo/{ccia}")]
        public async Task<IActionResult> ObtenerAlumnoSinGrupo( [FromRoute] string ccia )
        {

            string Sentencia = " SELECT t1.codAlumno, t1.alumnoNombre, t1.codCurso, mt1.nombre as capacidades  FROM alumno as t1 " +
                               " left join MasterTable as mt1 on mt1.codigo = t1.capacidades and mt1.master = 'CPAD' " +
                               " WHERE NOT EXISTS " +
                               " (SELECT NULL FROM grupoEstudiante t2 WHERE t2.idEstudiante = t1.codAlumno) " +
                               " and t1.codCia = @codCia ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@codCia",   ccia));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se ha podido crear...");
            }

            return Ok(dt);

        }   
        
        [HttpGet("ObtenerGruposEstudiantes/{ccia}")]
        public async Task<IActionResult> ObtenerGruposEstudiantes( [FromRoute] string ccia )
        {

            string Sentencia = " select count( t1.idEstudiante) as cantidadEstudiantes, t1.nombreGrupo, t1.codGrupo " +
                               " from grupoEstudiante as t1 " +
                               " where NOT EXISTS(SELECT NULL FROM procesos t2 WHERE t2.idAlumno = t1.codGrupo) and " +
                               " t1.codcia = @codCia and t1.idEstudiante != '' " +
                               " group by t1.nombreGrupo, t1.codGrupo ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@codCia",   ccia));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se ha podido crear...");
            }

            return Ok(dt);

        }   
        
        
        [HttpGet("ObtenerCantiddadAlumnosCurso/{ccia}")]
        public async Task<IActionResult> ObtenerCantiddadAlumnosCurso( [FromRoute] string ccia ) {

            string Sentencia = " select count( alumnoNombre ) as cantidadAlumno, " +
                               " codCurso, codCia from alumno " +
                               " where codCia = @codCia " +
                               " group by codCurso, codCia ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@codCia",   ccia));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se ha podido crear...");
            }

            return Ok(dt);

        }        
        [HttpGet("borrarGrupoEstudiantes/{cestudiante}")]
        public async Task<IActionResult> borrarGrupoEstudiantes( [FromRoute] string cestudiante ) {

            string Sentencia = " delete from grupoEstudiante where idEstudiante = @codEstudiante ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@codEstudiante", cestudiante));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se ha podido crear...");
            }

            return Ok(dt);

        }

        [HttpGet("borrarGrupo/{cGrupo}")]
        public async Task<IActionResult> borrarGrupo([FromRoute] string cGrupo)
        {

            string Sentencia = " delete from grupoEstudiante where codGrupo = @codGrupo ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@codGrupo", cGrupo));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se ha podido borrar...");
            }

            return Ok(dt);

        }



    }



}
