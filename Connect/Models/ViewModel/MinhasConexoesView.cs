using CampeonatosNParty.Models.Database;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.ViewModel
{
    public class MinhasConexoesView
    {
        public Usuarios currentUsuario { get; set; }
        public ClassSelectResult<UserConnections> result { get; set; }

        public MinhasConexoesView(Usuarios currentUsuario, string currentPage, int filter)
        {
            this.currentUsuario = currentUsuario;

            int page = 0;
            int.TryParse(currentPage, out page);

            ClassSelect<UserConnections> search = null;

            if (currentUsuario != null)
            {
                if(filter == 1)
                    search = UserConnections.Select().Where("PersonId1", currentUsuario.Id);
                else if(filter == 2)
                    search = UserConnections.Select().Where("PersonId2", currentUsuario.Id);
                else
                    search = UserConnections.Select().Where("PersonId1", currentUsuario.Id).Or("PersonId2", currentUsuario.Id);

                search.Page(18, page);

                result = search.ToResult();
            }
        }
    }
}