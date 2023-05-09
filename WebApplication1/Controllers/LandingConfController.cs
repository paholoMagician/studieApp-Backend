using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/landingConf")]
    [ApiController]
    public class LandingConfController : ControllerBase
    {

        private readonly vinculacionUgContext _context;

        public LandingConfController(vinculacionUgContext context)
        {
            _context = context;
        }

        [HttpGet("obtenerConfiguracionLanding/{ccia}")]
        public async Task<IActionResult> obtenerInstitutos([FromRoute] string ccia)
        {

            string Sentencia = " select * from  ";

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

        [HttpPost]
        [Route("guardarLandingConf")]
        public async Task<IActionResult> guardarLandingConf([FromBody] LandingConfiguration model)
        {

                if (ModelState.IsValid)
                {
                    _context.LandingConfiguration.Add(model);
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

    }
}
