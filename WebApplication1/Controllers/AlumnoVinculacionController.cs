using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/AlumnoVinculacion")]
    [ApiController]
    public class AlumnoVinculacionController : ControllerBase
    {


        private readonly vinculacionUgContext _context;

        public AlumnoVinculacionController(vinculacionUgContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("guardarAlumnosVinculacion")]
        public async Task<IActionResult> guardarAlumnosVinculacion([FromBody] Alumno model)
        {
            var result = await _context.Alumno.FirstOrDefaultAsync(x => x.CodAlumno == model.CodAlumno && x.Cedula == model.Cedula);

            if (result == null)
            {
                if (ModelState.IsValid)
                {
                    _context.Alumno.Add(model);
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

        [HttpPut]
        [Route("EditarAlumno/{userCod}")]
        public async Task<IActionResult> EditarAlumno([FromRoute] string userCod, [FromBody] Alumno model)
        {

            if (userCod != model.CodAlumno)
            {
                return BadRequest("No existe este usuario");
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(model);

        }

        private bool AlumnoExists(string id)
        {
            return _context.Alumno.Any(e => e.CodAlumno == id);
        }

        [HttpGet("CreateAccountProcess/{tipo}/{cUser}")]
        public async Task<IActionResult> CreateAccountProcess([FromRoute] int tipo,[FromRoute] string cUser)
        {

            string Sentencia = " exec CreateAccount @tp, @codUser ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@tp", tipo));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@codUser", cUser));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se ha podido crear...");
            }

            return Ok(dt);

        }


        [HttpGet("EliminacionDeEntidadesProcesos/{cUser}")]
        public async Task<IActionResult> EliminacionDeEntidadesProcesos([FromRoute] string cUser)
        {

            string Sentencia = " exec EliminarRegistroAlumno @codUser ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@codUser", cUser));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se ha podido crear...");
            }

            return Ok(dt);

        }


        [HttpGet("ObtenerAlumnos/{ccia}")]
        public async Task<IActionResult> ObtenerAlumnos([FromRoute] string ccia)
        {

                string Sentencia = "exec ObtenerAlumnos @codCia";

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

    }
}
