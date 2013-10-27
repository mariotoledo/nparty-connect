using CampeonatosNParty.Models.Database;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.ViewModel
{
    public enum SocialGamingButtonState
    {
        None, Enabled, Disabled
    }
    public class UsuariosDetailView
    {
        public Usuarios usuario { get; set; }
        public Cidade cidade { get; set; }
        public Estado estado { get; set; }
        public int Pontuacao { get; set; }
        public SocialGamingButtonState PSNButtonState { get; set; }
        public SocialGamingButtonState LiveButtonState { get; set; }
        public SocialGamingButtonState NintendoNetworkButtonState { get; set; }

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

        public void setPersonGamingRelation(Usuarios CurrentPerson){
            if (CurrentPerson == null || usuario == null || CurrentPerson.Id == usuario.Id)
            {
                PSNButtonState = SocialGamingButtonState.None;
                LiveButtonState = SocialGamingButtonState.None;
                NintendoNetworkButtonState = SocialGamingButtonState.None;
            }
            else
            {
                PersonGamingRelation relation = PersonGamingRelation.getPersonGamingRelations(CurrentPerson, usuario);

                if (relation == null)
                {
                    PSNButtonState = String.IsNullOrEmpty(CurrentPerson.PsnId) ||
                                     String.IsNullOrEmpty(usuario.PsnId) ?
                                     SocialGamingButtonState.None : SocialGamingButtonState.Enabled;

                    LiveButtonState = String.IsNullOrEmpty(CurrentPerson.LiveId) ||
                                     String.IsNullOrEmpty(usuario.LiveId) ?
                                     SocialGamingButtonState.None : SocialGamingButtonState.Enabled;

                    NintendoNetworkButtonState = String.IsNullOrEmpty(CurrentPerson.NintendoNetworkId) ||
                                     String.IsNullOrEmpty(usuario.NintendoNetworkId) ?
                                     SocialGamingButtonState.None : SocialGamingButtonState.Enabled;
                }
                else
                {
                    PSNButtonState = relation.isPSN ?
                                     SocialGamingButtonState.Disabled : SocialGamingButtonState.Enabled;

                    LiveButtonState = relation.isLive ?
                                     SocialGamingButtonState.Disabled : SocialGamingButtonState.Enabled;

                    NintendoNetworkButtonState = relation.isNintendoNetwork ?
                                     SocialGamingButtonState.Disabled : SocialGamingButtonState.Enabled;
                }
            }
        }
    }
}