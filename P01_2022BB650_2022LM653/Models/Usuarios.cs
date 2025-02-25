using System.ComponentModel.DataAnnotations;


namespace P01_2022BB650_2022LM653.Models
{
    public class Usuarios
    {
        [Key]
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Contraseña { get; set; }
        public string Rol {  get; set; }
    }
}
