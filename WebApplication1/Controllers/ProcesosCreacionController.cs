using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Procesos")]
    [ApiController]
    public class ProcesosCreacionController : ControllerBase
    {

        private readonly vinculacionUgContext _context;

        public ProcesosCreacionController(vinculacionUgContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("guardarProcesos")]
        public async Task<IActionResult> guardarProcesos([FromBody] Procesos model)
        {
            var result = await _context.Procesos.FirstOrDefaultAsync(x => x.IdProcesos == model.IdProcesos);

            if (result == null)
            {
                if (ModelState.IsValid)
                {
                    _context.Procesos.Add(model);
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
                return Ok("Procesos repetido");
            }
        }

        [HttpPut]
        [Route("editarProceso/{codProceso}")]
        public async Task<IActionResult> editarProceso([FromRoute] string codProceso, [FromBody] Procesos model)
        {

            if (codProceso != model.IdProcesos)
            {
                return BadRequest("No existe este proceso");
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(model);

        }

        [HttpGet("obtenerProcesos/{ccia}")]
        public async Task<IActionResult> obtenerProcesos([FromRoute] string ccia)
        {

            string Sentencia = " exec ObtenerProcesos @codCia ";

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

        [HttpGet("eliminarProceso/{idProceso}/{ccia}")]
        public async Task<IActionResult> eliminarProceso([FromRoute] string idProceso, [FromRoute] string ccia)
        {

            string Sentencia = " delete from procesos where idProcesos = @idProcesos and idcia = @codCia ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@idProcesos", idProceso));
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
