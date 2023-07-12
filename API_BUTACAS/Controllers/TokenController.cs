﻿using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Service.IServices;

namespace API_BUTACAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly ITokenService _service;

        public TokenController(ITokenService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("CrearToken")]

        public IActionResult createToken([FromBody]short ticketsCount) 
        {
            try
            {

                string token = _service.createToken(ticketsCount);
                return Ok(token);

            }catch (Exception ex)
            {
                string mensajeError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = mensajeError });
            }
        
        }

        [HttpGet]
        [Route("ValidarToken")]
        public IActionResult validToken(string token)
        {
            bool response = _service.ValidateToken(token);

            if (response == true)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Token admitido!." });

            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Token no encontrado." });
            }



        }

    }
}