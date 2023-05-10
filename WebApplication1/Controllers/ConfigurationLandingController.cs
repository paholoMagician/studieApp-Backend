using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/LandingConfiguration")]
    [ApiController]
    public class ConfigurationLandingController : ControllerBase
    {

        private readonly vinculacionUgContext _context;
        public ConfigurationLandingController(vinculacionUgContext context)
        {
            _context = context;
        }

        //[HttpPost]
        //[Route("guardarConfiguracionLanding")]
        //public async Task<IActionResult> guardarConfiguracionLanding([FromBody] LandingConfiguration model)
        //{
        //    var result = await _context.LandingConfiguration.FirstOrDefaultAsync(x => x.Id == model.Id);

        //    if (result == null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _context.LandingConfiguration.Add(model);
        //            if (await _context.SaveChangesAsync() > 0)
        //            {
        //                return Ok(model);
        //            }
        //            else
        //            {
        //                return BadRequest("Datos incorrectos");
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest("ERROR");
        //        }
        //    }
        //    else
        //    {
        //        return Ok("repetida");
        //    }
        //}

        [HttpGet("obtenerConfLanding/{codcia}")]
        public async Task<IActionResult> obtenerConfLanding([FromRoute] string codcia)
        {

            string Sentencia = " select * from landing_configuration where cod_instituto = @ccia  ";

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

        [HttpGet("eliminarConfLanding/{codcia}/{id}")]
        public async Task<IActionResult> eliminarConfLanding([FromRoute] string codcia, [FromRoute] int id)
        {

            string Sentencia = " delete from landing_configuration where cod_instituto = @ccia and id = @IDS ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ccia", codcia));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@IDS", id));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se ha podido crear...");
            }

            return Ok(dt);

        }

        //[HttpPut]
        //[Route("updateConfLanding/{id}/{codcia}")]
        //public async Task<IActionResult> EditarConvenioMarco([FromRoute] int id, [FromRoute] string codcia, [FromBody] LandingConfiguration model)
        //{

        //    if (id != model.Id && codcia != model.CodInstituto )
        //    {
        //        return BadRequest("No existe esta configuración");
        //    }

        //    _context.Entry(model).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //    return Ok(model);

        //}

    }
}
