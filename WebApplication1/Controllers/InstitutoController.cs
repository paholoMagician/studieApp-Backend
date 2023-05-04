using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Instituto")]
    [ApiController]
    public class InstitutoController : ControllerBase
    {

        private readonly vinculacionUgContext _context;
        public InstitutoController(vinculacionUgContext context)
        {
            _context = context;
        }

        [HttpGet("obtenerInstituto")]
        public async Task<IActionResult> obtenerInstituto()
        {

            string Sentencia = " select * from instituto  ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se ha podido crear...");
            }

            return Ok(dt);

        }

        [HttpPost]
        [Route("guardarInstituto")]
        public async Task<IActionResult> guardarInstituto([FromBody] Instituto model)
        {
            var result = await _context.Instituto.FirstOrDefaultAsync(x => x.Codcia == model.Codcia );

            if (result == null)
            {
                if (ModelState.IsValid)
                {
                    _context.Instituto.Add(model);
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
                return Ok("Instituto repetido");
            }
        }

    }
}
