using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace P01_2022BB650_2022LM653.Models
{
    public class Espacios_Parqueo
    {
        [Key]
        public int Espacio_parqueoId { get; set; }
        public int? sucursalId { get; set; }
        public int Numero {  get; set; }
        public string Ubicacion { get; set; }
        public decimal Costo_la_hora { get; set; }
        public string Estado {  get; set; }
    }
}
