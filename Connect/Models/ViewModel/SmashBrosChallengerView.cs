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

            search.Page(18, page);
            search.OrderBy("NomeUsuario", SortDirection.Ascending);
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

                    if (relation == null)
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
        }
    }
}