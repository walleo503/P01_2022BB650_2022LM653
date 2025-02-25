using Microsoft.AspNetCore.Mvc;
using P01_2022BB650_2022LM653.Models;
using Microsoft.EntityFrameworkCore;


namespace P01_2022BB650_2022LM653.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Espacios_ParqueoController : ControllerBase
    {

        private readonly DatosContext _Espacios_ParqueoContexto;

        public Espacios_ParqueoController(DatosContext Espacios_ParqueoContexto)
        {
            _Espacios_ParqueoContexto = Espacios_ParqueoContexto;
        }

        //Ver todas las sucusales.
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Espacio_de_parqueo()
        {
            List<Espacios_Parqueo> ListaParqueos = (from e in _Espacios_ParqueoContexto.Espacios_Parqueo
                                                select e).ToList();
            if (ListaParqueos.Count == 0)
            {
                return NotFound();
            }
            return Ok(ListaParqueos);
        }

        //Agregar nuevo Espacio de parqueo 
        [HttpPost]
        [Route("add")]
        public IActionResult GuardarEspacio_de_Parque([FromBody] Espacios_Parqueo espacio_de_parqueo)
        {
            try
            {
                _Espacios_ParqueoContexto.Espacios_Parqueo.Add(espacio_de_parqueo);
                _Espacios_ParqueoContexto.SaveChanges();
                return Ok(espacio_de_parqueo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Actualizar Espacios de parqueo
        [HttpPost]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizarEspacios_de_paqueo(int id, [FromBody] Espacios_Parqueo espacios_Parqueo_Modificar)
        {
            var EspacioActual = _Espacios_ParqueoContexto.Espacios_Parqueo.Find(id);

            if (EspacioActual == null) { return NotFound(); }

            EspacioActual.sucursalId = espacios_Parqueo_Modificar.sucursalId;
            EspacioActual.Numero = espacios_Parqueo_Modificar.Numero;
            EspacioActual.Ubicacion = espacios_Parqueo_Modificar.Ubicacion;
            EspacioActual.Costo_la_hora = espacios_Parqueo_Modificar.Costo_la_hora;
            EspacioActual.Estado = espacios_Parqueo_Modificar.Estado;

            _Espacios_ParqueoContexto.Entry(EspacioActual).State = EntityState.Modified;
            _Espacios_ParqueoContexto.SaveChanges();
            return Ok(espacios_Parqueo_Modificar);
        }

        //Eliminar espacio de parqueo
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarEspacio_de_parqueo(int id)
        {
            var Espacio = _Espacios_ParqueoContexto.Espacios_Parqueo.Find(id);

            if (Espacio == null)
            {
                return NotFound();
            }
            _Espacios_ParqueoContexto.Remove(Espacio);
            _Espacios_ParqueoContexto.SaveChanges();
            return Ok(Espacio);
        }


        //Registrar nuevos espacios.
        [HttpPost]
        [Route("Registrar")]
        public IActionResult RegistrarEspacio_de_Parqueo([FromBody] Espacios_Parqueo espacio_de_parqueo)
        {
            if ( espacio_de_parqueo== null)
            {
                return BadRequest("Datos inválidos");
            }

            // Validar si la sucursal existe
            var sucursal = _Espacios_ParqueoContexto.Sucursales.Find(espacio_de_parqueo.sucursalId);
            if (sucursal == null)
            {
                return NotFound("Esta sucursal no existe, ingrese otra por favor.");
            }

            // Validar si ya existe un espacio con el mismo número en la sucursal
            var existeEspacio = _Espacios_ParqueoContexto.Sucursales
                .Any(e => e.SucursalId== espacio_de_parqueo.sucursalId && e.SucursalId == espacio_de_parqueo.Numero);

            if (existeEspacio)
            {
                return Conflict("Ya existe un espacio con ese número en la sucursal.");
            }

            // Agregar el nuevo espacio
            _Espacios_ParqueoContexto.Espacios_Parqueo.Add(espacio_de_parqueo);
             _Espacios_ParqueoContexto.SaveChanges();

            return CreatedAtAction(nameof(RegistrarEspacio_de_Parqueo), new { id = espacio_de_parqueo.Espacio_parqueoId }, espacio_de_parqueo);
        }

    }
}
