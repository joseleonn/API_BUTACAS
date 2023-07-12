using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class ReservarDTO
    {
        public string token { get; set; }

        public List<PlateaBajaFuncion1DTO> seats { get; set; }
    }
}
