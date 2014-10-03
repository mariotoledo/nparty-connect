using CampeonatosNParty.Models.Database;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.ViewModel
{
    public class SmashBrosChallengerView
    {
        public ClassSelectResult<SuperSmashBrosChallengerItem> result;
        public Usuarios usuario;
        public bool isLoggedIn;
        public SuperSmashBrosChallengerItem myChallengerItem;
        public Dictionary<int, bool> isButtonActive;
        public CampeonatosDetailView campeonatoDetailView;

        public SmashBrosChallengerView(Usuarios currentUsuario, string pageString, int characterId)
        {
            usuario = currentUsuario;
            if (usuario != null && usuario.Id > 0)
                isLoggedIn = true;

            if (usuario != null)
            {
                myChallengerItem = SuperSmashBrosChallengerItem.WithMember("IdUsuario", usuario.Id);
            }

            int page = 0;
            int.TryParse(pageString, out page);

            ClassSelect<SuperSmashBrosChallengerItem> search = null;
            isButtonActive = new Dictionary<int, bool>();

            search = SuperSmashBrosChallengerItem.Select();

            if (characterId > 0)
            {
                search = search
                    .Where("IdPersonagem1", characterId)
                    .Or("IdPersonagem2", characterId)
                    .Or("IdPersonagem3", characterId);
            }

            search.Page(12, page);
            search.OrderBy("DataCriacao", SortDirection.Descending);
            result = search.ToResult();

            if (currentUsuario == null)
            {
                isLoggedIn = false;
            }
            else
            {
                isLoggedIn = true;
                foreach (SuperSmashBrosChallengerItem item in result)
                {
                    PersonGamingRelation relation =
                        PersonGamingRelation.getPersonGamingRelationsFromId(currentUsuario.Id, (int)item.IdUsuario);

                    if (item.IdUsuario != currentUsuario.Id && relation == null)
                    {
                        try
                        {
                            isButtonActive.Add((int)item.IdUsuario, true);
                        }
                        catch (Exception e) { }
                    }
                    else
                    {
                        try
                        {
                            isButtonActive.Add((int)item.IdUsuario, !relation.isFriendCode);
                        }
                        catch (Exception e) { }
                    }
                }
            }

            Campeonatos campeonato = Campeonatos.Select().Where("IdJogo", 1).Or("IdJogo", 6).Or("IdJogo", 49).OrderBy("DataCampeonato", SortDirection.Descending).SingleResult();
            if(campeonato != null)
                campeonatoDetailView = new CampeonatosDetailView(campeonato.Id);
        }
    }
}