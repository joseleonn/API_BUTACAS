﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Butacas
    {
        public Butacas()
        {
            Reservas = new HashSet<Reservas>();
        }

        public short IdButaca { get; set; }
        public short Fila { get; set; }
        public short NroButaca { get; set; }
        public short IdPlatea { get; set; }
        public short IdFuncion { get; set; }
        public bool? Reservado { get; set; }

        public virtual Funcion IdFuncionNavigation { get; set; }
        public virtual Platea IdPlateaNavigation { get; set; }
        public virtual ICollection<Reservas> Reservas { get; set; }
    }
}