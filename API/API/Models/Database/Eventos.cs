using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Database
{
    [DatabaseTable]
    public class Eventos
    {
        [DatabaseColumn]
        public int Id { get; set; }

        [DatabaseColumn]
        public string Nome { get; set; }
    }
}