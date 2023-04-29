using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroActividadController : ControllerBase
    {

        private readonly vinculacionUgContext _context;

        public RegistroActividadController(vinculacionUgContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("guardarRegistroActividad")]
        public async Task<IActionResult> guardarRegistroActividad([FromBody] RegistroActividad model)
        {
            var result = await _context.RegistroActividad.FirstOrDefaultAsync(x => x.CodRegActivity == model.CodRegActivity);

            if (result == null)
            {
                if (ModelState.IsValid)
                {
                    _context.RegistroActividad.Add(model);
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
            else
            {
                return Ok("Actividad Repetida");
            }
        }

        [HttpGet("ObtenerAlumnosGrupoRegistros/{cUser}/{codCia}")]
        public async Task<IActionResult> ObtenerAlumnosGrupoRegistros([FromRoute] string cUser, [FromRoute] string codCia)
        {

            string Sentencia = "exec ObtenerAlumnoRegistros @idAlumno, @ccia";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection)) {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@idAlumno", cUser));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ccia", codCia));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se ha podido crear...");
            }

            return Ok(dt);

        }

        [HttpGet("ObtenerRegistrosdeActividades/{cUser}/{codCia}")]
        public async Task<IActionResult> ObtenerRegistrosdeActividades([FromRoute] string cUser, [FromRoute] string codCia)
        {

            string Sentencia = " select RA.codRegActivity, RA.nombreActivity as nombre, MT.nombre as nombreActivity, " +
                               " RA.descriptionActivity, RA.fecCreacion, RA.idProceso, RA.horas, pr.fecInicio, pr.fecFin," +
                               " pr.numeroProceso, GP.nombreProyecto, GP.descripcionProyecto, GP.directorProyecto " +
                               " from registroActividad as RA " +
                               " left join MasterTable as MT on Mt.codigo = RA.nombreActivity and MT.master = 'LA00' " +
                               " left join procesos as PR on PR.numeroProceso = RA.idProceso " +
                               " left join generarProyecto as GP on GP.idProyecto =  PR.idProyecto" +
                               " where idUser = @idAlumno and ccia = @ccia" +
                               " order by RA.fecCreacion asc ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString)) {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection)) {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@idAlumno", cUser));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ccia", codCia));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se ha podido crear...");
            }

            return Ok(dt);

        }

        [HttpGet("DeleteRegistrosActividades/{cUser}")]
        public async Task<IActionResult> DeleteRegistrosActividades([FromRoute] string cUser)
        {

            string Sentencia = " delete from registroActividad where idUser = @idAlumno ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@idAlumno", cUser));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se ha podido crear...");
            }

            return Ok(dt);

        }


    }
}
