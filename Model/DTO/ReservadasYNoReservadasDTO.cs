using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class ReservadasYNoReservadasDTO
    {
        public List<int> reservadas { get; set; }
        public List<int> noReservadas { get; set; }
    }
}
