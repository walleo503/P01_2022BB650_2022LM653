using Microsoft.AspNetCore.Mvc;
using P01_2022BB650_2022LM653.Models;
using Microsoft.EntityFrameworkCore;


namespace P01_2022BB650_2022LM653.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Espacios_ParqueoController : ControllerBase
    {

        private readonly DatosContext _Espacios_ParqueoContexto;

        public Espacios_ParqueoController(DatosContext Espacios_ParqueoContexto)
        {
            _Espacios_ParqueoContexto = Espacios_ParqueoContexto;
        }
    }
}
