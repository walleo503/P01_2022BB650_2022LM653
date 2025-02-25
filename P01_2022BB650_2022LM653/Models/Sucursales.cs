using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace P01_2022BB650_2022LM653.Models
{
    public class Sucursales
    {
        [Key]
        public int SucursalId { get; set; }
        public string Nombre_Sucursal { get; set; }
        public string Direccion { get; set; }
        public string Telefono {  get; set; }
        public int? usuarioId { get; set; } 
        public int Espacios_disponibles { get; set; }
     }
}
