using CampeonatosNParty.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.ViewModel
{
    public class UsuariosDetailView
    {
        public Usuarios usuario { get; set; }

        public UsuariosDetailView(int userId)
        {
            usuario = Usuarios.WithIdentity(userId);
            if (usuario == null)
                usuario = new Usuarios();
        }
    }
}