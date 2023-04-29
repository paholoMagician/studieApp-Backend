using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Dom")]
    [ApiController]
    public class ConfDomController : ControllerBase
    {

        private readonly vinculacionUgContext _context;
        public ConfDomController(vinculacionUgContext context)
        {
            _context = context;
        }

        [HttpGet("GetConfDom/{userCod}/{Id}")]
        public async Task<IActionResult> GetConfDom([FromRoute] string userCod, [FromRoute] int id)
        {

            string Sentencia = " select * from confDom where id = @IDS and userCod = @usCod ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@userCod", userCod));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@IDS", id));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se encontro esta Configuracion...");
            }

            return Ok(dt);

        }

    }
}
