using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.Models;
using Service.IServices;

namespace API_BUTACAS.Controllers
{
    [EnableCors("RulesCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }



        //[HttpPost]
        //[Route("Registrar")]
        //public IActionResult RegistrarAdmin([FromBody] LoginDTO login)
        //{
        //    try {
        //    _service.RegisterUser(login);
        //        return Ok();
        //    } catch {
        //        return BadRequest();
        //    }

        //}

        [HttpPost]
        [Route("IniciarSesionAdmin")]
        public IActionResult IniciarSesionAdmin([FromBody] LoginDTO login)
        {
            if (login.Usuario == "")
            {
                return BadRequest("Ingrese un correo");
            }

            if (login.Contrasena == "")
            {
                return BadRequest("Ingrese una contraseña");
            }
            try
            {


                string token = _service.LoginAdmin(login);



                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Has entrado con exito!", response = token });


            }
            catch (Exception ex)
            {
                string mensajeError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = mensajeError });

            }
        }


        [HttpGet]
        [Route("VerAlumnos")]

        public IActionResult GetAlumnos()
        {
            try
            {
                List<Alumnos> alumnos = _service.verTodosLosAlumnos();

                return Ok(alumnos);
            }catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("CargarAlumnos")]

        public IActionResult CargarAlumno([FromBody] Alumnos alumno)
        {
            try{
                    _service.CargarAlumno(alumno);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("EliminarAlumno/{id}")]

        public IActionResult EliminarAlumno(short id)
        {
            try
            {
                _service.EliminarAlumno(id);

                return Ok();
            }catch {
            return BadRequest();
            }
        }
    }
}
