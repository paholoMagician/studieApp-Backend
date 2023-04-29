using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/PersonalVinculacion")]
    [ApiController]
    public class PersonalVinculController : ControllerBase
    {

        private readonly vinculacionUgContext _context;

        public PersonalVinculController(vinculacionUgContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("guardarPersonalVinculacion")]
        public async Task<IActionResult> guardarPersonalVinculacion([FromBody] PersonalVinculacion model)
        {
            var result = await _context.PersonalVinculacion.FirstOrDefaultAsync(x => x.CodPersonal == model.CodPersonal && x.Codcia == model.Codcia);

            if (result == null)
            {
                if (ModelState.IsValid)
                {
                    _context.PersonalVinculacion.Add(model);
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
                return Ok("Persona de vinculación repetidos");
            }
        }

        [HttpPut]
        [Route("editarPersonal/{codPers}")]
        public async Task<IActionResult> editarPersonal([FromRoute] string codPers, [FromBody] PersonalVinculacion model)
        {

            if (codPers != model.CodPersonal) {
                return BadRequest("No existe este usuario");
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(model);

        }

        [HttpGet("obtenerPersonal/{tipo}/{cPers}/{cCia}")]
        public async Task<IActionResult> obtenerPersonal([FromRoute] string tipo, [FromRoute] string cPers, [FromRoute] string cCia)
        {
            
            string Sentencia = " exec ObtenerEntidades @tipo, @cpers, @ccia ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter( "@tipo",  tipo  ));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter( "@cpers", cPers ));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter( "@ccia",  cCia  ));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se ha podido crear...");
            }

            return Ok(dt);

        }

        [HttpGet("obtenerPersonalVinculacionGeneral/{cCia}")]
        public async Task<IActionResult> obtenerPersonalVinculacionGeneral([FromRoute] string cCia)
        {

            string Sentencia = " exec ObtenerPersonalVinculacion @ccia ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ccia", cCia));
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
