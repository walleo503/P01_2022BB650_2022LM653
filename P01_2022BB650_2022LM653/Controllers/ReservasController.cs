using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P01_2022BB650_2022LM653.Models;

namespace P01_2022BB650_2022LM653.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {

        private readonly DatosContext _contexto;

        public ReservasController(DatosContext contexto)
        {
            _contexto = contexto;
        }

        // Obtener todas las reservas
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            var reservas = _contexto.Reservas.ToList(); 
            if (!reservas.Any()) return NotFound();
            return Ok(reservas);
        }

        // Crear una reserva
        [HttpPost]
        [Route("Add")]
        public IActionResult CrearReserva([FromBody] Reservas reserva)
        {
            var espacio = _contexto.Espacios_Parqueo.Find(reserva.Espacio_parqueoId);
            if (espacio == null || espacio.Estado != "Disponible")
                return BadRequest("El espacio no está disponible");

            espacio.Estado = "Ocupado";
            _contexto.Reservas.Add(reserva);
            _contexto.SaveChanges();
            return Ok(reserva);
        }

        // Actualizar una reserva
        [HttpPut]
        [Route("Update/{id}")]
        public IActionResult ActualizarReserva(int id, [FromBody] Reservas reservaModificada)
        {
            var reserva = _contexto.Reservas.Find(id);
            if (reserva == null) return NotFound();

            reserva.Fecha_Hora_Inicio = reservaModificada.Fecha_Hora_Inicio;
            reserva.CantidadHoras = reservaModificada.CantidadHoras;

            _contexto.Entry(reserva).State = EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(reserva);
        }

        // Eliminar una reserva
        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult EliminarReserva(int id)
        {
            var reserva = _contexto.Reservas.Find(id);
            if (reserva == null) return NotFound();

            _contexto.Reservas.Remove(reserva);
            _contexto.SaveChanges();
            return Ok(reserva);
        }

        // Cancelar una reserva antes de su uso
        [HttpPut]
        [Route("Cancel/{id}")]
        public IActionResult CancelarReserva(int id)
        {
            var reserva = _contexto.Reservas.Find(id);
            if (reserva == null) return NotFound();

            if (reserva.Fecha_Hora_Inicio <= DateTime.Now)
                return BadRequest("No se puede cancelar una reserva en curso o pasada");

            reserva.Estado = "Cancelada";
            var espacio = _contexto.Espacios_Parqueo.Find(reserva.Espacio_parqueoId);
            if (espacio != null) espacio.Estado = "Disponible";

            _contexto.SaveChanges();
            return Ok(reserva);
        }

        // Obtener reservas activas de un usuario
        [HttpGet]
        [Route("UserActiveReservations/{usuarioId}")]
        public IActionResult ObtenerReservasUsuario(int usuarioId)
        {
            var reservas = _contexto.Reservas.Where(r => r.UsuarioId == usuarioId && r.Estado == "Activa").ToList();
            if (!reservas.Any()) return NotFound();
            return Ok(reservas);
        }

        // Obtener espacios reservados por día en todas las sucursales
        [HttpGet]
        [Route("ReservationsByDay/{fecha}")]
        public IActionResult ReservasPorDia(DateTime fecha)
        {
            var reservas = _contexto.Reservas
                .Where(r => r.Fecha_Hora_Inicio.Date == fecha.Date)
                .GroupBy(r => new { r.EspacioParqueo, r.EspacioParqueo.sucursalId })
                .Select(g => new { SucursalId = g.Key.sucursalId, EspacioId = g.Key.EspacioParqueo, TotalReservas = g.Count() })
                .ToList();

            if (!reservas.Any()) return NotFound();
            return Ok(reservas);
        }

        // Obtener espacios reservados segun  la fechas en una sucursal específica
        [HttpGet]
        [Route("ReservationsByRange/{sucursalId}/{fechaInicio}/{fechaFin}")]
        public IActionResult ReservasEntreFechas(int sucursalId, DateTime fechaInicio, DateTime fechaFin)
        {
            var reservas = _contexto.Reservas
                .Where(r => r.EspacioParqueo.sucursalId == sucursalId && r.Fecha_Hora_Inicio >= fechaInicio && r.Fecha_Hora_Inicio <= fechaFin)
                .ToList();

            if (!reservas.Any()) return NotFound();
            return Ok(reservas);
        }

    }
}
