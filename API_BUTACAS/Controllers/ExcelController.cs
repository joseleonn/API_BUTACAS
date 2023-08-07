using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using ClosedXML.Excel;
using Model.Models;
using Model.DTO;
using Microsoft.AspNetCore.Authorization;

namespace API_BUTACAS.Controllers
{
    [EnableCors("RulesCors")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ExcelController : Controller
    {
        private readonly string cadenaSQL;

        public ExcelController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("csvm");
        }

        [HttpPost]
        [Route("DescargarExcel")]
        public IActionResult DescargarExcel([FromBody] DescargarExcelDTO fechas)
        {
            DataTable tabla_alumnos = new DataTable();

            using (var conexion = new SqlConnection(cadenaSQL))
            {
                conexion.Open();
                using (var adapter = new SqlDataAdapter())
                {
                    adapter.SelectCommand = new SqlCommand("sp_reporte_alumno", conexion);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechas.FechaInicio);
                    adapter.SelectCommand.Parameters.AddWithValue("@FechaFin", fechas.FechaFin);

                    adapter.Fill(tabla_alumnos);

                }
            }

            using (var libro = new XLWorkbook())
            {
                tabla_alumnos.TableName = "Alumnos";
                var hoja = libro.Worksheets.Add(tabla_alumnos);
                hoja.ColumnsUsed().AdjustToContents();

                using ( var memoria = new MemoryStream())
                {
                    libro.SaveAs(memoria);

                    var nombreExcel = string.Concat("Reporte Alumnos", DateTime.Now.ToString(), ".xlsx");
                    return File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);                }
            }
        }
    }
}
