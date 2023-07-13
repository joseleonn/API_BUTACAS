using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class TokenDTO
    {
        public string TokenCreated { get; set; }
        public short AssignedTickets { get; set; }
        public short UsedTickets { get; set; }

        public List<ReservasDTO> Reservas { get; set; }

    }
}
