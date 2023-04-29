using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Master")]
    [ApiController]
    public class MasterTablaController : ControllerBase
    {

        private readonly vinculacionUgContext _context;

        public MasterTablaController(vinculacionUgContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("guardarDataMaster")]
        public async Task<IActionResult> guardarDataMaster([FromBody] MasterTable model)
        {
            var result = await _context.MasterTable.FirstOrDefaultAsync(x => x.Codigo == model.Codigo);

            if (result == null)
            {
                if (ModelState.IsValid)
                {
                    _context.MasterTable.Add(model);
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
                return Ok("Data repetida");
            }
        }

        [HttpGet("GetDataMaster/{mast}")]
        public async Task<IActionResult> GetDataMaster([FromRoute] string mast)
        {

            string Sentencia = " select rtrim(ltrim(master)) as master, rtrim(ltrim( codigo )) as codigo," +
                               " rtrim(ltrim( nombre )) as nombre from MasterTable " +
                               " where master = @master group by master, codigo, nombre ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@master", mast));
                    //adapter.SelectCommand.Parameters.Add(new SqlParameter("@IDS", id));
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
