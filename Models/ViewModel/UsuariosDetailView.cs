using CampeonatosNParty.Models.Database;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.ViewModel
{
    public class UsuariosDetailView
    {
        public Usuarios usuario { get; set; }
        public Cidade cidade { get; set; }
        public Estado estado { get; set; }
        public int Pontuacao { get; set; }

        public IEnumerable<ClassificacaoPorJogador[]> campeonatos { get; set; }

        public UsuariosDetailView(int userId)
        {
            usuario = Usuarios.WithIdentity(userId);
            if (usuario != null)
            {
                cidade = Cidade.WithIdentity(usuario.Id_Cidade);
                estado = Estado.WithIdentity(usuario.Id_Estado);
            }

            Pontuacao = JogadoresItem.WithMember("IdUsuario", userId).Pontos;

            campeonatos = ClassificacaoPorJogador.Select().Where("IdUsuario", userId).OrderBy("DataCampeonato", EixoX.Data.SortDirection.Descending).Segment(4);
        }
    }
}