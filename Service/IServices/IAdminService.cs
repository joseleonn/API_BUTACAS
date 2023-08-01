using Model.DTO;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IAdminService
    {
        void RegisterUser(LoginDTO login);
        string LoginAdmin(LoginDTO login);

        List<Alumnos> verTodosLosAlumnos();

        bool CargarAlumno(Alumnos alumno);

        bool EliminarAlumno(short id);
    }
}
