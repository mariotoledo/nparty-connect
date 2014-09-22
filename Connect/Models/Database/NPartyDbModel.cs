using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EixoX.Data;

namespace CampeonatosNParty.Models.Database
{
    public abstract class NPartyDbModel<T>
    {
        public static ClassSelect<T> Select()
        {
            return NPartyDb<T>.Instance.Select();
        }

        public static ClassSelect<T> Search(string filter)
        {
            return NPartyDb<T>.Instance.Search(filter);
        }

        public static T WithIdentity(object identity)
        {
            return NPartyDb<T>.Instance.WithIdentity(identity);
        }

        public static T WithMember(string memberName, object memberValue)
        {
            return NPartyDb<T>.Instance.WithMember(memberName, memberValue);
        }
    }
}