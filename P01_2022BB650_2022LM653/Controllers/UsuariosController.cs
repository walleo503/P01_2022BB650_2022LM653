﻿using Microsoft.AspNetCore.Http;
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
            List<Usuarios> ListaUsuarios = (from e in _UsuarioContexto.Usuarios
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
        public IActionResult AgregarUsuario([FromBody] Usuarios nuevoUsuario)
        {
            if (nuevoUsuario == null)
            {
                return BadRequest("Los datos no son validos.");
            }

            // Validar usuario
            var usuarioExistente = _UsuarioContexto.Usuarios.FirstOrDefault(u => u.Correo == nuevoUsuario.Correo);

            if (usuarioExistente != null)
            {
                return Conflict("El correo ya está registrado.");
            }

            _UsuarioContexto.Usuarios.Add(nuevoUsuario);
            _UsuarioContexto.SaveChanges();

            return CreatedAtAction(nameof(AgregarUsuario), new { id = nuevoUsuario.UsuarioId }, nuevoUsuario);
        }


        //Actualizar usuario
        [HttpPost]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizaUsuario(int id, [FromBody] Usuarios Usuariomodificar)
        {
            var UsuarioActual = _UsuarioContexto.Usuarios.Find(id);

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
            var Sucursal = _UsuarioContexto.Usuarios.Find(id);

            if (Sucursal == null)
            {
                return NotFound();
            }
            _UsuarioContexto.Remove(Sucursal);
            _UsuarioContexto.SaveChanges();
            return Ok(Sucursal);
        }
    }
}
