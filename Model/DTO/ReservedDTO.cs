using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class ReservedDTO
    {
        public string token { get; set; }

        public List<SeatDTO> seats { get; set; }
       
    }
}
