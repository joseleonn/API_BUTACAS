using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class AlumnoDTO
    {
        public short IdAlumno { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Domicilio { get; set; }
        public string Barrio { get; set; }
        public string Celular { get; set; }
        public string FechaDeNacimiento { get; set; }
        public string Dni { get; set; }
        public short Edad { get; set; }
        public string ObraSocial { get; set; }
        public string GrupoSanguineo { get; set; }
        public string ObservacionesFisicas { get; set; }
        public string NombreDeMadre { get; set; }
        public string DireccionMadre { get; set; }
        public string CelularMadre { get; set; }
        public string MailMadre { get; set; }
        public string NombreDePadre { get; set; }
        public string DireccionPadre { get; set; }
        public string CelularPadre { get; set; }
        public string MailPadre { get; set; }
        public string EnCasoDeEmergenciaA { get; set; }
        public string Telefono { get; set; }
        public string CelularEmergencia { get; set; }
        public string Parentesco { get; set; }
        public string DisciplinaQueCursa { get; set; }
        public string AnioDeComienzo { get; set; }
    }
}
