using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class WebUserController : ControllerBase
    {

        private readonly vinculacionUgContext _context;

        public WebUserController(vinculacionUgContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<WebUser>>> GetWebUser()
        {
            return await _context.WebUser.ToListAsync();
        }



        [HttpGet("GetModulos/{userCod}")]
        public async Task<IActionResult> GetModulos([FromRoute] string userCod)
        {

            string Sentencia = " select a.imagenContent, z.username, z.userCod, z.tipo from webUser as z " +
                               " left join Imagen as a on a.idImagen = z.userCod" +
                               " where z.userCod = @usCod ";

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


        [HttpGet("GetGrupoByUser/{userCod}")]
        public async Task<IActionResult> GetGrupoByUser([FromRoute] string userCod)
        {

            string Sentencia = " select nombreGrupo, codGrupo from grupoEstudiante where idEstudiante = @usCod ";

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


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] WebUser userInfo)
        {
            var result = await _context.WebUser.FirstOrDefaultAsync(x => x.Email == userInfo.Email && x.Password == userInfo.Password);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                //ModelState.AddModelError(string.Empty, "Usuario o contraseña invalido");
                return BadRequest("Datos incorrectos");
            }
        }

    }
}
