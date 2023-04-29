using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/ModulosAsignacion")]
    [ApiController]
    public class ModuloController : ControllerBase
    {
        private readonly vinculacionUgContext _context;

        public ModuloController(vinculacionUgContext context)
        {
            _context = context;
        }

        [HttpGet("GetModulos/{userCod}")]
        public async Task<IActionResult> GetModulos([FromRoute] string userCod)
        {

            string Sentencia = " select a.cod_user, b.* from asignModUser as a " +
                               " left join modulo as b on a.cod_mod = b.id " +
                               " where a.cod_user = @usCod ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@usCod", userCod));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se encontro este WebUser...");
            }

            return Ok(dt);

        }

        //[HttpPost]
        //[Route("guardarAlumnosVinculacion")]
        //public async Task<IActionResult> guardarAlumnosVinculacion([FromBody] Alumno model)
        //{
        //    var result = await _context.Alumno.FirstOrDefaultAsync(x => x.CodAlumno == model.CodAlumno && x.Cedula == model.Cedula);

        //    if (result == null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _context.Alumno.Add(model);
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
        //        return Ok("Alumno repetidos");
        //    }
        //}

    }
}
