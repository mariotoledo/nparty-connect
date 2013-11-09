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
        public SocialGamingButtonState MiiverseButtonState { get; set; }
        public SocialGamingButtonState FriendCodeButtonState { get; set; }
        public List<UsuarioBadges> badges { get; set; }
        public bool hasGamingConnection;

        public IEnumerable<ClassificacaoPorJogador[]> campeonatos { get; set; }

        public PokemonFriendSafariType safariType;
        public List<PokemonFriendSafari> pokemonFriendSafari;

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

            badges = UsuarioBadges.Select().Where("PersonId", userId).ToList();

            PersonPokemonFriendSafari personSafari = PersonPokemonFriendSafari.Select().Where("PersonId", userId).SingleResult();

            if(personSafari != null)
            {
                safariType = PokemonFriendSafariType.WithIdentity(personSafari.PokemonTypeId);
                pokemonFriendSafari = PokemonFriendSafari.Select().Where("Id", personSafari.PokemonSlot1Id)
                                        .Or("Id", personSafari.PokemonSlot2Id)
                                        .Or("Id", personSafari.PokemonSlot3Id).ToList();
            }
        }

        public void setPersonGamingRelation(Usuarios CurrentPerson){
            if (CurrentPerson == null || usuario == null || CurrentPerson.Id == usuario.Id)
            {
                PSNButtonState = SocialGamingButtonState.None;
                LiveButtonState = SocialGamingButtonState.None;
                MiiverseButtonState = SocialGamingButtonState.None;
            }
            else
            {
                PersonGamingRelation relation = PersonGamingRelation.getPersonGamingRelations(CurrentPerson, usuario);

                if (!String.IsNullOrEmpty(CurrentPerson.PsnId) && !String.IsNullOrEmpty(usuario.PsnId))
                {
                    PSNButtonState = relation != null && relation.isPSN ?
                                     SocialGamingButtonState.Disabled :
                                     SocialGamingButtonState.Enabled;
                }
                else
                {
                    PSNButtonState = SocialGamingButtonState.None;
                }

                if (!String.IsNullOrEmpty(CurrentPerson.LiveId) && !String.IsNullOrEmpty(usuario.LiveId))
                {
                    LiveButtonState = relation != null && relation.isLive ?
                                     SocialGamingButtonState.Disabled :
                                     SocialGamingButtonState.Enabled;
                }
                else
                {
                    LiveButtonState = SocialGamingButtonState.None;
                }

                if (!String.IsNullOrEmpty(CurrentPerson.MiiverseId) && !String.IsNullOrEmpty(usuario.MiiverseId))
                {
                    MiiverseButtonState = relation != null && relation.isMiiverse ?
                                     SocialGamingButtonState.Disabled :
                                     SocialGamingButtonState.Enabled;
                }
                else
                {
                    MiiverseButtonState = SocialGamingButtonState.None;
                }

                if (!String.IsNullOrEmpty(CurrentPerson.FriendCode) && !String.IsNullOrEmpty(usuario.FriendCode))
                {
                    FriendCodeButtonState = relation != null && relation.isFriendCode ?
                                     SocialGamingButtonState.Disabled :
                                     SocialGamingButtonState.Enabled;
                }
                else
                {
                    FriendCodeButtonState = SocialGamingButtonState.None;
                }
            }

            hasGamingConnection = !String.IsNullOrEmpty(usuario.PsnId) ||
                                  !String.IsNullOrEmpty(usuario.LiveId) ||
                                  !String.IsNullOrEmpty(usuario.MiiverseId) ||
                                  !String.IsNullOrEmpty(usuario.FriendCode);
        }
    }
}