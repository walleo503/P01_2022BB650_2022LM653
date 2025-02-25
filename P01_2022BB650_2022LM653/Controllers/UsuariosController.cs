using Microsoft.AspNetCore.Http;
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


    }
}
