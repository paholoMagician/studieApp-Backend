using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Imagen")]
    [ApiController]
    public class ImagenController : ControllerBase
    {

        private readonly vinculacionUgContext _context;

        public ImagenController(vinculacionUgContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("guardarImagen")]
        public async Task<IActionResult> guardarImagen([FromBody] Imagen model)
        {
            var result = await _context.Imagen.FirstOrDefaultAsync( x => x.IdImagen == model.IdImagen );

            if (result == null)
            {
                if (ModelState.IsValid)
                {
                    _context.Imagen.Add(model);
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
                return Ok("Imagen Repetida");
            }
        }


        [HttpPost]
        [Route("guardarImagenActividad")]
        public async Task<IActionResult> guardarImagenActividad([FromBody] ActividadImagen model)
        {

                if (ModelState.IsValid)
                {
                    _context.ActividadImagen.Add(model);
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

        [HttpPut]
        [Route("EditarImagenActividad/{id}")]
        public async Task<IActionResult> EditarImagenActividad([FromRoute] int id, [FromBody] ActividadImagen model)
        {

            if (id != model.Id)
            {
                return BadRequest("No existe la imagen");
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(model);

        }



        [HttpPut]
        [Route("EditarImagen/{codImagen}")]
        public async Task<IActionResult> EditarImagen([FromRoute] string codImagen, [FromBody] Imagen model)
        {

            if (codImagen != model.IdImagen)
            {
                return BadRequest("No existe este usuario");
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(model);

        }

        [HttpGet("deleteImagenActividad/{id}")]
        public async Task<IActionResult> deleteImagenActividad([FromRoute] int id)
        {

            string Sentencia = " delete from actividadImagen where id = @IDS ";

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


        [HttpGet("obtenerImagenActividad/{regActiv}")]
        public async Task<IActionResult> obtenerImagenActividad( [FromRoute] string regActiv)
        {

            string Sentencia = "select * from actividadImagen where codRegistroActividad = @codRegistroActividad";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@codRegistroActividad", regActiv));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("No se ha podido crear...");
            }

            return Ok(dt);

        }

        [HttpGet("obtenerImagen/{cUser}/{tp}")]
        public async Task<IActionResult> obtenerImagen([FromRoute] string cUser, [FromRoute] string tp)
        {

            string Sentencia = " select * from imagen where idImagen = @cBinding and tipo = @tipo ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@cBinding", cUser));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@tipo", tp));
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
