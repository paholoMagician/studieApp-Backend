using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/conf")]
    [ApiController]
    public class ConfiguracionesSistemaController : ControllerBase
    {

        private readonly vinculacionUgContext _context;
        public ConfiguracionesSistemaController(vinculacionUgContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("guardarConfiguracionConvenio")]
        public async Task<IActionResult> guardarConfiguracionConvenio([FromBody] VinculacionConfig model)
        {
            var result = await _context.VinculacionConfig.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (result == null)
            {
                if (ModelState.IsValid)
                {
                    _context.VinculacionConfig.Add(model);
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
                return Ok("repetida");
            }
        }


        [HttpGet("obtenerConfVinc/{codcia}")]
        public async Task<IActionResult> obtenerConfVinc([FromRoute] string codcia)
        {

            string Sentencia = " select * from vinculacion_config where codcia = @ccia  ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ccia", codcia));
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
        [Route("EditarConfiguracionConvenio/{id}")]
        public async Task<IActionResult> EditarConvenioMarco([FromRoute] int id, [FromBody] VinculacionConfig model)
        {

            if (id != model.Id)
            {
                return BadRequest("No existe esta configuración");
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(model);

        }

    }
}
