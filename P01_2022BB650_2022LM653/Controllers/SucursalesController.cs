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

        //Actualizar sucursal
        [HttpPost]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizarSucursal(int id, [FromBody] Sucursales sucursalmodificar)
        {
            var SucursalActual = _SucursalContexto.Sucursales.Find(id);

            if (SucursalActual == null) { return NotFound(); }

            SucursalActual.Nombre_Sucursal = sucursalmodificar.Nombre_Sucursal;
            SucursalActual.Direccion = sucursalmodificar.Direccion;
            SucursalActual.Telefono = sucursalmodificar.Telefono;
            SucursalActual.usuarioId = sucursalmodificar.usuarioId;
            SucursalActual.Espacios_disponibles = sucursalmodificar.Espacios_disponibles;

            _SucursalContexto.Entry(SucursalActual).State = EntityState.Modified;
            _SucursalContexto.SaveChanges();
            return Ok(sucursalmodificar);
        }

        //Eliminar sucursal 
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarSucursal(int id)
        {
            var Sucursal = _SucursalContexto.Sucursales.Find(id);

            if (Sucursal == null)
            {
                return NotFound();
            }
            _SucursalContexto.Remove(Sucursal);
            _SucursalContexto.SaveChanges();
            return Ok(Sucursal);
        }

        

    }
}
