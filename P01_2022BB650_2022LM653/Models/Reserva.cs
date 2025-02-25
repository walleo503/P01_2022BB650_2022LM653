using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace P01_2022BB650_2022LM653.Models
{
    public class Reserva
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int EspacioId { get; set; }

        [Required]
        public DateTime FechaHoraInicio { get; set; }

        [Required]
        public int CantidadHoras { get; set; }

        [Required]
        [StringLength(10)]
        public string Estado { get; set; } = "Activa"; // Activa o Cancelada

        // Relaciones
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        [ForeignKey("EspacioId")]
        public EspacioParqueo EspacioParqueo { get; set; }

    }
}
