using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_2022BB650_2022LM653.Models
{
    public class Reservas
    {
        [Key]
        public int ReservaId { get; set; }
        public int UsuarioId { get; set; }

        [ForeignKey("EspacioParqueo")]
        public int Espacio_parqueoId { get; set; }
        public DateTime Fecha_Hora_Inicio { get; set; }
        public int CantidadHoras { get; set; }

        [StringLength(10)]
        public string Estado { get; set; } = "Activa"; // Activa o Cancelada
        public Usuario Usuario { get; set; }
        public Espacios_Parqueo EspacioParqueo { get; set; }

    }
}
