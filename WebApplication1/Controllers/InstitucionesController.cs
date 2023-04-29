using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Instituciones")]
    [ApiController]
    public class InstitucionesController : ControllerBase
    {

        private readonly vinculacionUgContext _context;

        public InstitucionesController(vinculacionUgContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("guardarInstitutos")]
        public async Task<IActionResult> guardarInstitutos([FromBody] MaestroInstitucion model)
        {
            var result = await _context.MaestroInstitucion.FirstOrDefaultAsync(x => x.CodInstiProy == model.CodInstiProy);

            if (result == null)
            {
                if (ModelState.IsValid)
                {
                    _context.MaestroInstitucion.Add(model);
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
                return Ok("Instituciones repetidos");
            }
        }

        [HttpPut]
        [Route("actualizarInstitutos/{CodInstiProy}")]
        public async Task<IActionResult> actualizarInstitutos([FromRoute] string CodInstiProy, [FromBody] MaestroInstitucion model)
        {

            if (CodInstiProy != model.CodInstiProy)
            {
                return BadRequest("No existe esta instituciones");
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(model);

        }

        [HttpGet("obtenerInstitutos/{ccia}")]
        public async Task<IActionResult> obtenerInstitutos([FromRoute] string ccia)
        {

            string Sentencia = " exec ObtenerInstitutos @codCia ";

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

        [HttpGet("EliminarInstitutos/{codInst}")]
        public async Task<IActionResult> EliminarInstitutos([FromRoute] string codInst)
        {

            string Sentencia = " delete from maestroInstitucion where codInstiProy = @cInst; " +
                               " update personalVinculacion set idInstituto = '' where idInstituto = @cInst; ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@cInst", codInst));
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
