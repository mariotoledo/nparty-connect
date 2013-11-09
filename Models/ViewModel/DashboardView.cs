using CampeonatosNParty.Models.Database;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.ViewModel
{
    public class DashboardView
    {
        public Usuarios usuario { get; set; }
        public List<Notificacoes> notificacoes {get; set;}

        public DashboardView(Usuarios usuario)
        {
            this.usuario = usuario;

            notificacoes = NPartyDb<Notificacoes>.Instance.Select()
                                                .Where("PersonId", usuario.Id)
                                                .Or("PersonId", 0)
                                                .OrderBy("DateSent", SortDirection.Descending)
                                                .Take(3)
                                                .ToList();
        }
    }
}