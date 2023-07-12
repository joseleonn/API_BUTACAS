using Model.Models;
using Model.DTO;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Service.Services
{
    public class SeatService : ISeatService
    {
        private readonly EstudioDeDanzasContext _context;

        public SeatService(EstudioDeDanzasContext context)
        {
            _context = context;  
        }

        public List<Butacas> ObtenerButacasPorPlateaYFuncion(int idPlatea, int idFuncion)
        {
            var butacas = _context.Butacas
                .Where(b => b.IdPlatea == idPlatea && b.IdFuncion == idFuncion)
                .ToList();

            return butacas;
        }


      

        public bool IsSeatReserved(int seatId)
        {
            bool isReserved = _context.Reservas.Any(r => r.IdButaca == seatId);
            return isReserved;
        }


        public List<string> VerifyReserved(string token, List<SeatDTO> seats)
        {
            Tokens tokenExist = _context.Tokens.FirstOrDefault(u => u.TokenCreated == token);

            if (tokenExist != null)
            {
                if (tokenExist.AssignedTickets != 0)
                {
                    List<string> results = new List<string>();
                    List<Reservas> nuevasReservas = new List<Reservas>();

                    foreach (var seat in seats)
                    {
                        // Obtener la butaca correspondiente al asiento
                        Butacas butacaExist = _context.Butacas.FirstOrDefault(b => b.IdButaca == seat.Id);
                        Reservas butacaReserved = _context.Reservas.FirstOrDefault(b => b.IdButaca == seat.Id);

                        if (butacaExist != null && butacaReserved != null)
                        {
                            results.Add($"El asiento {seat.Id} ya está reservado, elija otro.");
                        }
                        else
                        {
                            results.Add($"El asiento {seat.Id} se ha reservado a tu nombre.");

                            // Se reserva la butaca
                            butacaExist.Reservado = true;

                            // Se descuenta 1 entrada del token
                            tokenExist.AssignedTickets -= 1;

                            // Se suma 1 entrada al token
                            tokenExist.UsedTickets += 1;

                            // Se crea una nueva reserva y se agrega a la lista de nuevas reservas
                            Reservas reserva = new Reservas();
                            reserva.IdButaca = butacaExist.IdButaca; // Asignar el Id de la butaca reservada
                            reserva.TokenCreated = tokenExist.TokenCreated; // Asignar el Id del token

                            reserva.IdFuncion = butacaExist.IdFuncion;
                            nuevasReservas.Add(reserva);
                        }
                    }

                    // Agregar todas las nuevas reservas a la tabla "Reservations"
                    foreach (var reserva in nuevasReservas)
                    {
                        Reservas reservaExistente = _context.Reservas.Find(reserva.IdButaca);
                        if (reservaExistente == null)
                        {
                            _context.Reservas.Add(reserva);
                        }
                    }

                    // Guardar los cambios en la base de datos
                    _context.SaveChanges();

                    return results;
                }
                else
                {
                    throw new Exception("No tienes tickets disponibles");
                }
            }
            else
            {
                throw new Exception("El token no es válido.");
            }
        }







        //public List<Seat> showSeats()
        //{
        //    List<Seat> allSeats = _context.Seats.ToList();

        //    return allSeats;
        //}


        //public List<string> VerifyReservedPlateaAltaFuncion1(string token, List<PlateaBajaFuncion1DTO> seats)
        //{
        //    Token tokenExist = _context.Tokens.FirstOrDefault(u => u.TokenCreated == token);

        //    if (tokenExist != null)
        //    {
        //        if (tokenExist.ValidToken == true)
        //        {
        //            if (tokenExist.AssignedTickets != 0)
        //            {
        //                List<string> results = new List<string>();
        //                List<Reservation> nuevasReservas = new List<Reservation>();


        //                foreach (var seat in seats)
        //                {
        //                    PlateaBajaFuncion1 seatReserved = _context.PlateaBajaFuncion1s.FirstOrDefault(s => s.Id == seat.Id);

        //                    if (seatReserved != null && seatReserved.Reservado == true)
        //                    {
        //                        results.Add($"El asiento {seat.Id} ya está reservado, elija otro.");
        //                    }
        //                    else
        //                    {
        //                        results.Add($"El asiento {seat.Id} se ha reservado a tu nombre.");

        //                        // Se reserva el asiento
        //                        seatReserved.Reservado = true;

        //                        // Se descuenta 1 entrada del token
        //                        tokenExist.AssignedTickets -= 1;

        //                        //se suma 1 a tickes usados
        //                        tokenExist.UsedTickets += 1;

        //                        //// Se crea una nueva reserva y se agrega a la lista de nuevas reservas
        //                        //Reservation reserva = new Reservation();
        //                        //reserva.SeatId = seatReserved.Id; // Asignar el Id del asiento reservado
        //                        //reserva.TokenId = tokenExist.Id; // Asignar el Id del token
        //                        //reserva.TokenCreated = tokenExist.TokenCreated;
        //                        //nuevasReservas.Add(reserva);
        //                        // Guardar los cambios en la base de datos
        //                    }
        //                }

        //                // Agregar todas las nuevas reservas a la tabla "Reservations"
        //                //_context.Reservations.AddRange(nuevasReservas);
        //                // Guardar los cambios en la base de datos
        //                _context.SaveChanges();

        //                return results;

        //            }
        //            else
        //            {
        //                throw new Exception("No tienes tickes disponibles");
        //            }
        //        }
        //        else
        //        {
        //            throw new Exception("El token ya no es mas valido");
        //        }


        //    }
        //    else
        //    {
        //        throw new Exception("El token no es válido.");
        //    }
        //}

    }
}
