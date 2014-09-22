using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.Database
{
    [DatabaseTable]
    public class SuperSmashBrosPersonagem
    {
        [DatabaseColumn]
        public long Id { get; set; }

        [DatabaseColumn]
        public string Nome { get; set; }

        [DatabaseColumn]
        public bool TemCorBranco { get; set; }

        [DatabaseColumn]
        public bool TemCorPreto { get; set; }

        [DatabaseColumn]
        public bool TemCorAzul { get; set; }

        [DatabaseColumn]
        public bool TemCorAmarelo { get; set; }

        [DatabaseColumn]
        public bool TemCorVerde { get; set; }

        [DatabaseColumn]
        public bool TemCorRosa { get; set; }

        [DatabaseColumn]
        public bool TemCorRoxo { get; set; }

        [DatabaseColumn]
        public bool TemCorVermelho { get; set; }

        [DatabaseColumn]
        public bool TemCorLaranja { get; set; }
    }
}