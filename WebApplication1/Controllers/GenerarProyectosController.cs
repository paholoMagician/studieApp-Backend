using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/GenerarProyectos")]
    [ApiController]
    public class GenerarProyectosController : ControllerBase
    {

        private readonly vinculacionUgContext _context;

        public GenerarProyectosController(vinculacionUgContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("guardarProyecto")]
        public async Task<IActionResult> guardarProyecto([FromBody] GenerarProyecto model)
        {
            var result = await _context.GenerarProyecto.FirstOrDefaultAsync(x => x.IdProyecto == model.IdProyecto);

            if (result == null)
            {
                if (ModelState.IsValid)
                {
                    _context.GenerarProyecto.Add(model);
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
                return Ok("Proyecto repetido");
            }
        }


        [HttpGet("obtenerGestoresProyectos/{ccia}")]
        public async Task<IActionResult> obtenerGestoresProyectos([FromRoute] string ccia)
        {

            string Sentencia = " select m.nombre as nombreTippo, p.personaNombre," +
                               " p.cedula, p.tipo, p.codPersonal from personalVinculacion as p" +
                               " left join MasterTable as m on p.tipo = m.codigo  and m.master = 'TP01'" +
                               " where p.codcia = @codCia and p.personaNombre != 'administrador' ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@codCia", ccia));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se ha podido crear...");
            }

            return Ok(dt);

        }


        [HttpGet("ValidacionAsignacionProyectos/{cuser}/{cgrupo}")]
        public async Task<IActionResult> ValidacionAsignacionProyectos([FromRoute] string cuser, [FromRoute] string cgrupo)
        {

            string Sentencia = " exec ValidacionAlumnoProyecto @codUser, @codGrupo ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@codUser", cuser));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@codGrupo", cgrupo));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se ha podido crear...");
            }

            return Ok(dt);

        }

        [HttpPut]
        [Route("EditarProyecto/{ProyCod}")]
        public async Task<IActionResult> EditarAlumno([FromRoute] string ProyCod, [FromBody] GenerarProyecto model)
        {

            if (ProyCod != model.IdProyecto)
            {
                return BadRequest("No existe este usuario");
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(model);

        }

        [HttpGet("obtenerProyectos/{ccia}")]
        public async Task<IActionResult> obtenerProyectos([FromRoute] string ccia)
        {

            string Sentencia = " exec ObtenerProyectos @codCia ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@codCia", ccia));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("NO se ha encontrado...");
            }

            return Ok(dt);

        }


    }
}
