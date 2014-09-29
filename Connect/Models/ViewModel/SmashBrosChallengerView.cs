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
            result = search.ToResult();
        }
    }
}