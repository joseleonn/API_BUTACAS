using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class ReservasDTO
    {
        public short IdReserva { get; set; }

        public short IdButaca { get; set; }

        public short IdFuncion { get; set; }

        public short Fila { get; set; }
        public short NroButaca { get; set; }
    }
}
