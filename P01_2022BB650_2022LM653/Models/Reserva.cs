using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace P01_2022BB650_2022LM653.Models
{
    public class Reserva
    {
        [Key]
        public int ReservaId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int Espacio_parqueoId { get; set; }

        [Required]
        public DateTime Fecha_Hora_Inicio { get; set; }

        [Required]
        public int CantidadHoras { get; set; }

        [Required]
        [StringLength(10)]
        public string Estado { get; set; } = "Activa"; // Activa o Cancelada

        // Relaciones
        [ForeignKey("UsuarioId")]
        public Usuarios Usuario { get; set; }

        [ForeignKey("EspacioId")]
        public Espacios_Parqueo EspacioParqueo { get; set; }

    }
}
