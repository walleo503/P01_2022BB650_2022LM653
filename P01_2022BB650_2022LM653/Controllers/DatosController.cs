using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using P01_2022BB650_2022LM653.Models;


namespace P01_2022BB650_2022LM653.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosController : ControllerBase
    {
        private readonly DatosContext _DatosContexto;

        public DatosController(DatosContext DatosContexto)
        {
            _DatosContexto = DatosContexto;
        }


    }
}
