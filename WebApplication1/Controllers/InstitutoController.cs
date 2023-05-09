using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Instituto")]
    [ApiController]
    public class InstitutoController : ControllerBase
    {

        private readonly vinculacionUgContext _context;
        public InstitutoController(vinculacionUgContext context)
        {
            _context = context;
        }

        [HttpGet("obtenerInstituto")]
        public async Task<IActionResult> obtenerInstituto()
        {

            string Sentencia = " select ins.codcia, ins.ruc, ins.licenceCodec, ins.nombre, ins.descripcion, ins.telefA, "
                               + " ins.telfB, ins.telfC, ins.email1, ins.email2, ins.direccion, ins.fecCrea, "
                               + " ins.web, ins.horasVinc, ins.codProvincia,  mt1.nombre as provincia, "
                               + " ins.codCanton, mt2.nombre as canton, ins.fecha_fundacion "
                               + " from instituto as ins "
                               + " left join MasterTable as mt1 on mt1.codigo = ins.codProvincia and mt1.master = 'PRV00' "
                               + " left join MasterTable as mt2 on mt2.codigo = ins.codCanton    and mt2.master = ins.codProvincia  ";

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

        [HttpGet("eliminarInstituto/{codCia}")]
        public async Task<IActionResult> eliminarInstituto([FromRoute] string codCia )
        {

            string Sentencia = " delete from instituto where codcia = @codcia";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@codCia", codCia));
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
        [Route("actualizarInstituto/{codcia}")]
        public async Task<IActionResult> actualizarInstituto([FromRoute] string codcia, [FromBody] Instituto model)
        {

            if (codcia != model.Codcia)
            {
                return BadRequest("No existe esta instituciones");
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(model);

        }

        [HttpPost]
        [Route("guardarInstituto")]
        public async Task<IActionResult> guardarInstituto([FromBody] Instituto model)
        {
            var result = await _context.Instituto.FirstOrDefaultAsync(x => x.Codcia == model.Codcia );

            if (result == null)
            {
                if (ModelState.IsValid)
                {
                    _context.Instituto.Add(model);
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
                return Ok("Instituto repetido");
            }
        }

    }
}
