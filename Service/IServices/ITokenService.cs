﻿using Model.DTO;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface ITokenService
    {
        string createToken(short ticketCount);

        TokenDTO ValidateToken(string tok);

        List<Tokens> MostrarTokens();

        bool EliminarToken(string token);
    }
}
