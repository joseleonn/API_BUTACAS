using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.DTO;
using Model.Models;
using Service.IServices;
using Service.Service;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AdminService : IAdminService
    {
        public readonly EstudioDeDanzasContext _dbcontext;

        private readonly string secretKey;

        private readonly IMailService _mailService;

        public AdminService(EstudioDeDanzasContext context, IConfiguration config, IMailService mailService)
        {
            _dbcontext = context;
            secretKey = config.GetSection("settings").GetSection("secretkey").ToString();
            _mailService = mailService;
        }

        public void RegisterUser(LoginDTO login)
        {

     

            var usuarioEntity = new Admin
            {
                // Asigna las propiedades de la entidad basadas en los datos del DTO
                Usuario = login.Usuario,
                Contrasena = UtilityService.ConvertHA256(login.Contrasena),

            };

           

            //AGREGAMOS USUARIO A LA BASE DE DATOS
            _dbcontext.Admin.Add(usuarioEntity);
            _dbcontext.SaveChanges();
        }
        public string LoginAdmin(LoginDTO login)
        {

            Admin usuarioAuth = _dbcontext.Admin.FirstOrDefault(u => u.Usuario == login.Usuario && u.Contrasena == UtilityService.ConvertHA256(login.Contrasena));

            if (usuarioAuth != null)
            {
             


                    var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                    var claims = new ClaimsIdentity();

                    //recibimos el correo para darle permisos atravez del mismo

                    //creamos el token
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddDays(60),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)

                    };

                    //lectura del token 
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                    string tokenCreate = tokenHandler.WriteToken(tokenConfig);

                    return tokenCreate;
            }
            else
            {
                throw new Exception("Credenciales de inicio de sesión no válidas");
            }



        }

        public List<Alumnos> verTodosLosAlumnos()
        {
            List<Alumnos> alumnos = _dbcontext.Alumnos.ToList();

            if(alumnos != null)
            {
                return alumnos;
            }
            else
            {
                return null;
            }
        }

        public bool CargarAlumno(Alumnos alumno)
        {
            _dbcontext.Alumnos.Add(alumno);
            _dbcontext.SaveChanges();

            EnviarMailDTO emailDTO = new EnviarMailDTO()
            { 
                mail = alumno.MailAlumno,
                disciplina = alumno.DisciplinaQueCursa
                
            };
            _mailService.EnviarMailmethod(emailDTO);
            return true;
        }

        public bool EliminarAlumno(short id)
        {
            Alumnos alumnoExiste = _dbcontext.Alumnos.FirstOrDefault(u => u.IdAlumno == id);

            if (alumnoExiste != null)
            {
                _dbcontext.Alumnos.Remove(alumnoExiste);
                _dbcontext.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
