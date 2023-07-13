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
    public class SeatController : Controller
    {
        private readonly ISeatService _service;

        public SeatController(ISeatService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("Reservar")]
        public IActionResult Reserved([FromBody] ReservedDTO reser)
        {
            try
            {
                List<string> results = _service.VerifyReserved(reser.token, reser.seats);
                return Ok(results);

            }
            catch (Exception ex)
            {
                string mensajeError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = mensajeError });
            }

        }

        [HttpDelete]
        [Route("ElimiarTodasLasReservas")]

        public IActionResult DeleteAll()
        {
            try {

                _service.DeleteAllFromReservas();
                return Ok();
            
            } catch
            {
                return BadRequest("No se pudo elimiar las reservas");
            }
        }


        [HttpGet]
        [Route("MostrarPlataBajaFuncion1")]
        public IActionResult ShowPlataBajaFuncion1()
        {
            try
            {
                List<Butacas> response = _service.ObtenerButacasPorPlateaYFuncion(1, 1);
                return Ok(response);
            }
            catch (Exception ex)
            {
                string mensajeError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = mensajeError });
            }
        }

        [HttpGet]
        [Route("MostrarPlataAltaFuncion1")]
        public IActionResult ShowPlateaAltaFuncion1()
        {
            try
            {
                List<Butacas> response = _service.ObtenerButacasPorPlateaYFuncion(2, 1);
                return Ok(response);
            }
            catch (Exception ex)
            {
                string mensajeError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = mensajeError });
            }
        }

        [HttpGet]
        [Route("MostrarPlataBajaFuncion2")]
        public IActionResult ShowPlataBajaFuncion2()
        {
            try
            {
                List<Butacas> response = _service.ObtenerButacasPorPlateaYFuncion(3, 2);
                return Ok(response);
            }
            catch (Exception ex)
            {
                string mensajeError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = mensajeError });

                //[HttpPost]
                //[Route("ReservarPlateaBajaFuncion1")]
                //public IActionResult ReservarPlateaBajaFuncion1([FromBody] ReservarDTO reser)
                //{
                //    try
                //    {
                //        List<string> results = _service.VerifyReservedPlateaAltaFuncion1(reser.token, reser.seats);
                //        return Ok(results);

                //    }
                //    catch (Exception ex)
                //    {
                //        string mensajeError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                //        return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = mensajeError });
                //    }

                //}
            }
        }

        [HttpGet]
        [Route("MostrarPlataAltaFuncion2")]
        public IActionResult ShowPlataAltaFuncion2()
        {
            try
            {
                List<Butacas> response = _service.ObtenerButacasPorPlateaYFuncion(4, 2);
                return Ok(response);
            }
            catch (Exception ex)
            {
                string mensajeError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = mensajeError });

            }
        }
    }
}
    
