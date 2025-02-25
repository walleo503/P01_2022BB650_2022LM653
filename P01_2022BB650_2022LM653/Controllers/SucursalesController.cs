using Microsoft.AspNetCore.Mvc;
using P01_2022BB650_2022LM653.Models;
using Microsoft.EntityFrameworkCore;


namespace P01_2022BB650_2022LM653.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly DatosContext _SucursalContexto;

        public SucursalesController(DatosContext SucursalContexto)
        {
            _SucursalContexto = SucursalContexto;
        }


    }
}
