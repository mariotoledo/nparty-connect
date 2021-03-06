﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EixoX.Restrictions;
using EixoX.Data;
using EixoX.Interceptors;
using EixoX.UI;

namespace CampeonatosNParty.Models.StructModel
{
    public class EditarEvento
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        [UIHidden]
        public int Id { get; set; }

        [DatabaseColumn]
        [Required]
        [MaxLength(50)]
        [UISingleline]
        public string Nome { get; set; }

        [DatabaseColumn]
        [Required]
        public int TipoEvento { get; set; }

        [DatabaseColumn]
        [MaxLength(50)]
        [UISingleline]
        public string Local { get; set; }

        [DatabaseColumn]
        public int IdCidade { get; set; }

        [DatabaseColumn]
        public int IdEstado { get; set; }

        [DatabaseColumn]
        [Required]
        [UIDatepicker]
        public DateTime DataEventoInicio { get; set; }

        [DatabaseColumn]
        [UIDatepicker]
        public DateTime DataEventoFim { get; set; }

        [DatabaseColumn]
        [UISingleline]
        public string ImagemURL { get; set; }

    }
}