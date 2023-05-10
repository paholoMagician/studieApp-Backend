using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/AsignacionProyectos")]
    [ApiController]
    public class AsignProyController : ControllerBase
    {

        private readonly vinculacionUgContext _context;
        public AsignProyController(vinculacionUgContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("guardarAsignProy")]
        public async Task<IActionResult> guardarAsignProy([FromBody] Asignproyprocess model)
        {
            var result = await _context.Asignproyprocess
                                    .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (result == null)
            {
                if (ModelState.IsValid)
                {
                    _context.Asignproyprocess.Add(model);
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

        [HttpGet("obtenerAsigProy")]
        public async Task<IActionResult> obtenerAsigProy()
        {

            string Sentencia = " select apr.id, apr.codContraparte, apr.codCoordgeneral, apr.codProy, apr.codProceso, "
                               +" gp.nombreProyecto, pr.numeroProceso, pv.personaNombre as coordinadorGeneral, pv1.personaNombre as contraparte "
                               + " , pv1.tipo, pv1.idInstituto, mi.nombreInstituto, mi.alias "
                               + " from asignproyprocess as apr "
                               +" left join generarProyecto as gp on gp.idProyecto = apr.codProy "
                               +" left join procesos as pr on pr.idProcesos = apr.codProceso "
                               +" left join personalVinculacion as pv on pv.codPersonal = apr.codCoordgeneral "
                               +" left join personalVinculacion as pv1 on pv1.codPersonal = apr.codContraparte "
                               +" left join maestroInstitucion as mi on mi.codInstiProy = pv1.idInstituto ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection)) {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    //adapter.SelectCommand.Parameters.Add(new SqlParameter("@userCod", userCod));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se ha podido crear...");
            }

            return Ok(dt);

        }


        [HttpGet("deleteAsigProy/{id}")]
        public async Task<IActionResult> deleteAsigProy([FromRoute] int id)
        {

            string Sentencia = " delete from asignproyprocess where id = @IDS";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
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

        [HttpPut]
        [Route("EditarAsignProy/{id}")]
        public async Task<IActionResult> EditarAlumno([FromRoute] int id, [FromBody] Asignproyprocess model)
        {

            if (id != model.Id)
            {
                return BadRequest("No existe este usuario");
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(model);

        }



    }
}
