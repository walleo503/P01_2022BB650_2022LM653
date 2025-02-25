using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using P01_2022BB650_2022LM653.Models;


namespace P01_2022BB650_2022LM653.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DatosContext _UsuarioContexto;

        public UsuariosController(DatosContext UsuarioContexto)
        {
            _UsuarioContexto = UsuarioContexto;
        }

        //Ver todos los usuarios
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Usuarios()
        {
            List<Usuario> ListaUsuarios = (from e in _UsuarioContexto.Usuario
                                                    select e).ToList();
            if (ListaUsuarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(ListaUsuarios);
        }

        //Agregar ususario 
        [HttpPost]
        [Route("Agregar Usuario")]
        public IActionResult AgregarUsuario([FromBody] Usuario nuevoUsuario)
        {
            if (nuevoUsuario == null)
            {
                return BadRequest("Los datos no son validos.");
            }

            // Validar usuario
            var usuarioExistente = _UsuarioContexto.Usuario.FirstOrDefault(u => u.Correo == nuevoUsuario.Correo);

            if (usuarioExistente != null)
            {
                return Conflict("El correo ya está registrado.");
            }

            _UsuarioContexto.Usuario.Add(nuevoUsuario);
            _UsuarioContexto.SaveChanges();

            return CreatedAtAction(nameof(AgregarUsuario), new { id = nuevoUsuario.UsuarioId }, nuevoUsuario);
        }


        //Actualizar usuario
        [HttpPost]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizaUsuario(int id, [FromBody] Usuario Usuariomodificar)
        {
            var UsuarioActual = _UsuarioContexto.Usuario.Find(id);

            if (UsuarioActual == null) { return NotFound(); }

            UsuarioActual.Nombre = Usuariomodificar.Nombre;
            UsuarioActual.Correo = Usuariomodificar.Correo;
            UsuarioActual.Telefono =Usuariomodificar.Telefono;
            UsuarioActual.Contraseña = Usuariomodificar.Contraseña;
            UsuarioActual.Rol = Usuariomodificar.Rol;

            _UsuarioContexto.Entry(UsuarioActual).State = EntityState.Modified;
            _UsuarioContexto.SaveChanges();
            return Ok(Usuariomodificar);
        }

        //Eliminar sucursal 
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            var Sucursal = _UsuarioContexto.Usuario.Find(id);

            if (Sucursal == null)
            {
                return NotFound();
            }
            _UsuarioContexto.Remove(Sucursal);
            _UsuarioContexto.SaveChanges();
            return Ok(Sucursal);
        }

        //Validacion de usuarios.
        [HttpPost]
        [Route("Login")]
        public IActionResult IniciarSesion([FromBody] Usuarios ValidarUsuario)
        {
            if (ValidarUsuario == null || string.IsNullOrEmpty(ValidarUsuario.Correo) || string.IsNullOrEmpty(V.Contraseña))
            {
                return BadRequest("(Obligatorios) Por favor ingresar Correo y contraseña .");
            }

            var usuario = _UsuarioContexto.Usuarios.FirstOrDefault(u => u.Correo == ValidarUsuario.Correo);

            if (usuario == null)
            {
                return Unauthorized("El correo o la contraseña son incorrectos.");
            }

            // Comparar la contraseña en texto plano (esto no es seguro, se recomienda usar hashing)
            if (usuario.Contraseña !=ValidarUsuario.Contraseña)
            {
                return Unauthorized("Correo o contraseña incorrectos.");
            }

            return Ok(new { mensaje = "Inicio de sesión exitoso", usuario });
        }


    }
}
