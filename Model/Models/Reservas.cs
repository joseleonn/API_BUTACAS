﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Reservas
    {
        public short IdReserva { get; set; }
        public short IdButaca { get; set; }
        public string TokenCreated { get; set; }
        public short IdFuncion { get; set; }

        public virtual Butacas IdButacaNavigation { get; set; }
        public virtual Funcion IdFuncionNavigation { get; set; }
        public virtual Tokens TokenCreatedNavigation { get; set; }
    }
}