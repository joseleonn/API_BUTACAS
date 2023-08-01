using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Model.DTO;
using Model.Models;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    
    public class TokenService : ITokenService
    {
        private readonly EstudioDeDanzasContext _context;

        public TokenService(EstudioDeDanzasContext context)
        {
            _context = context;
        }

        public string createToken(short ticketCount)
        {
            string token = Guid.NewGuid().ToString(); // Genera un token único

            var newToken = new Tokens
            {
                TokenCreated = token,
                CreateDate = DateTime.Now,
                AssignedTickets = ticketCount,
                UsedTickets = 0,
                ValidToken = true,
            };

            _context.Tokens.Add(newToken);
            _context.SaveChanges();
            return token;
        }

        public TokenDTO ValidateToken(string tok)
        {
         

        Tokens tokenExist = _context.Tokens.FirstOrDefault(u=> u.TokenCreated == tok);

            if (tokenExist!=null)
            {
                List<Reservas> reservas = _context.Reservas.Where(u => u.TokenCreated == tok).ToList();

                TokenDTO token = new TokenDTO
                {
                    TokenCreated = tokenExist.TokenCreated,
                    AssignedTickets = tokenExist.AssignedTickets,
                    UsedTickets = tokenExist.UsedTickets,
                    Reservas = new List<ReservasDTO>()

                };

                foreach (var reserva in reservas)
                {
                    Butacas butaca = _context.Butacas.FirstOrDefault(b => b.IdButaca == reserva.IdButaca);

                    if (butaca != null)
                    {
                        ReservasDTO reservaDTO = new ReservasDTO
                        {
                            IdReserva = reserva.IdReserva,
                            IdButaca = reserva.IdButaca,
                            Fila = butaca.Fila,
                            NroButaca = butaca.NroButaca
                        };

                        token.Reservas.Add(reservaDTO);
                    }
                }



                return token;

            }
            else
            {
                return null;
            }
        }

        public List<Tokens> MostrarTokens()
        {

            List<Tokens> tokens =_context.Tokens.ToList();

            if (tokens != null)
            {
                return tokens;
            }
            else
            {
                return null;
            }
        }

        public bool EliminarToken(string token)
        {
            Tokens tokenExiste = _context.Tokens.FirstOrDefault(u => u.TokenCreated == token);

            if (tokenExiste != null)
            {
                _context.Tokens.Remove(tokenExiste);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
