using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/mobileModule")]
    [ApiController]
    public class MobileModuleController : ControllerBase
    {

        private readonly vinculacionUgContext _context;

        public MobileModuleController(vinculacionUgContext context)
        {
            _context = context;
        }


        [HttpGet("ObtenerModuleMobile")]
        public async Task<IActionResult> ObtenerModuleMobile()
        {

            string Sentencia = "select * from module_mobile";

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

    }
}


