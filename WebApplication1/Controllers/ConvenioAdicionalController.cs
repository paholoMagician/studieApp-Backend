using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Especifico")]
    [ApiController]
    public class ConvenioAdicionalController : ControllerBase
    {

        private readonly vinculacionUgContext _context;

        public ConvenioAdicionalController(vinculacionUgContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("guardarConvenioEspecifico")]
        public async Task<IActionResult> guardarConvenioEspecifico([FromBody] ConvenioEspecifico model)
        {
            var result = await _context.ConvenioEspecifico.FirstOrDefaultAsync(x => x.CodConvenioEsp == model.CodConvenioEsp);

            if (result == null)
            {
                if (ModelState.IsValid)
                {
                    _context.ConvenioEspecifico.Add(model);
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

        [HttpGet("obtenerConvenioAdicional/{codMa}")]
        public async Task<IActionResult> obtenerConvenioAdicional([FromRoute] string codMa)
        {

            string Sentencia = " select * from convenioEspecifico where codConvenioMarco = @codMarco ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@codMarco", codMa));
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
        [Route("EditarConvenioEspecifico/{codConvenio}")]
        public async Task<IActionResult> EditarConvenioMarco([FromRoute] string codConvenio, [FromBody] ConvenioEspecifico model)
        {

            if (codConvenio != model.CodConvenioEsp)
            {
                return BadRequest("No existe este convenio Especifico");
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(model);

        }

    }
}
