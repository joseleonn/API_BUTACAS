using Model.DTO;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface ISeatService
    {
        List<Butacas> ObtenerButacasPorPlateaYFuncion(int idPlatea, int idFuncion);

        bool IsSeatReserved(int seatId);
        List<string> VerifyReserved(string token, List<SeatDTO> seats);

        void DeleteAllFromReservas();

    }
}
