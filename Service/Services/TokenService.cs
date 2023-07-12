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

        public bool ValidateToken(string tok)
        {

        Tokens tokenExist = _context.Tokens.FirstOrDefault(u=> u.TokenCreated == tok);

            if (tokenExist!=null)
            {
                return true;

            }
            else
            {
                return false;
            }
        }
    }
}
