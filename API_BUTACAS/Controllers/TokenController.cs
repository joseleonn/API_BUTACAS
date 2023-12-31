﻿using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.Models;
using Service.IServices;
using System.Reflection.Metadata.Ecma335;

namespace API_BUTACAS.Controllers
{
    [EnableCors("RulesCors")]
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
            TokenDTO response = _service.ValidateToken(token);

            if (response != null)
            {   
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Token admitido!.", response});

            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Token no encontrado." });
            }



        }


        [HttpGet]
        [Route("MostrarTokens")]
        public IActionResult MostrarTokens() {
            try
            {
                List<Tokens> response = _service.MostrarTokens();
                return Ok(response);
            }
            catch {
                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("/EliminarToken/{token}")]

        public IActionResult EliminarAlumno(string token)
        {
            try
            {
                _service.EliminarToken(token);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
