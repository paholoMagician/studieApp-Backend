using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Convenios")]
    [ApiController]
    public class MarcoController : ControllerBase
    {

        private readonly vinculacionUgContext _context;

        public MarcoController(vinculacionUgContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("guardarConvenioMarco")]
        public async Task<IActionResult> guardarConvenioMarco([FromBody] ConvenioMarco model)
        {
            var result = await _context.ConvenioMarco.FirstOrDefaultAsync(x => x.CodConvenioMarco == model.CodConvenioMarco);

            if (result == null)
            {
                if (ModelState.IsValid)
                {
                    _context.ConvenioMarco.Add(model);
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
                return Ok("Alumno repetidos");
            }
        }




        [HttpGet("obtnerConvenioMacro/{ccia}")]
        public async Task<IActionResult> obtnerConvenioMacro( [FromRoute] string ccia )
        {

            string Sentencia = " exec ObtenerConvenios @codCia ";

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

        [HttpPut]
        [Route("EditarConvenioMarco/{codConvenio}")]
        public async Task<IActionResult> EditarConvenioMarco([FromRoute] string codConvenio, [FromBody] ConvenioMarco model)
        {

            if (codConvenio != model.CodConvenioMarco)
            {
                return BadRequest("No existe este convenio");
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(model);

        }

    }
}
