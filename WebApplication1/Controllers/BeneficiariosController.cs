using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Beneficiarios")]
    [ApiController]
    public class BeneficiariosController : ControllerBase
    {

        private readonly vinculacionUgContext _context;

        public BeneficiariosController(vinculacionUgContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("guardarBeneficiarios")]
        public async Task<IActionResult> guardarBeneficiarios([FromBody] BeneficiariosVinculacion model)
        {
            var result = await _context.BeneficiariosVinculacion
                                    .FirstOrDefaultAsync(x => x.CedBeneficiario == model.CedBeneficiario );

            if (result == null)
            {
                if (ModelState.IsValid)
                {
                    _context.BeneficiariosVinculacion.Add(model);
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
                return Ok("Beneficiarios repetidos");
            }
        }

        [HttpGet("ObtenerProcesosPorProyectos/{ccia}/{idProyecto}")]
        public async Task<IActionResult> ObtenerProcesosPorProyectos([FromRoute] string ccia, [FromRoute] string idProyecto)
        {

            string Sentencia = " select pr.idProcesos, pr.numeroProceso, ge.nombreGrupo,  " +
                               " COUNT(ge.idEstudiante) as cantidad from procesos as pr " +
                               " left join grupoEstudiante as ge on ge.codGrupo = pr.idAlumno " +
                               " where pr.idProyecto = @idProy " +
                               " and ge.idEstudiante != '' and pr.idcia = @codCia " +
                               " group by pr.idProcesos, pr.numeroProceso, ge.nombreGrupo"; 

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@codCia", ccia));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@idProy", idProyecto));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se ha podido crear...");
            }

            return Ok(dt);

        }


        [HttpGet("ObtenerBeneficiarios/{ccia}")]
        public async Task<IActionResult> ObtenerBeneficiarios([FromRoute] string ccia)
        {

            string Sentencia = " exec ObtenerBeneficiario @codCia";

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


        [HttpPut]
        [Route("EditarBeneficiario/{benefCod}")]
        public async Task<IActionResult> EditarAlumno([FromRoute] string benefCod, [FromBody] BeneficiariosVinculacion model)
        {

            if (benefCod != model.CedBeneficiario)
            {
                return BadRequest("No existe este usuario");
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(model);
            
        }

        [HttpGet("BorrarBeneficiarios/{benefCod}")]
        public async Task<IActionResult> BorrarBeneficiarios([FromRoute] string benefCod)
        {

            string Sentencia = " delete from beneficiariosVinculacion where cedBeneficiario = @codBen";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@codBen", benefCod));
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
