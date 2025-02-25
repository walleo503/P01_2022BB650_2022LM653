using Microsoft.AspNetCore.Mvc;
using P01_2022BB650_2022LM653.Models;
using Microsoft.EntityFrameworkCore;


namespace P01_2022BB650_2022LM653.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly DatosContext _SucursalContexto;

        public SucursalesController(DatosContext SucursalContexto)
        {
            _SucursalContexto = SucursalContexto;
        }


        //Ver todas las sucusales.
        [HttpGet]
        [Route("GetAll")]
        public IActionResult getSucursales() {
            List <Sucursales> listaSucursales =(from e in _SucursalContexto.Sucursales
                                                select e).ToList();
            if (listaSucursales.Count == 0) {
              return NotFound();
            }
           return Ok (listaSucursales);
        }

        //Agregar nueva sucursal 
        [HttpPost]
        [Route("add")]
        public IActionResult GuardarSucursal([FromBody] Sucursales sucursales)
        {
            try
            {
                _SucursalContexto.Sucursales.Add(sucursales);
                _SucursalContexto.SaveChanges();
                return Ok(sucursales);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
