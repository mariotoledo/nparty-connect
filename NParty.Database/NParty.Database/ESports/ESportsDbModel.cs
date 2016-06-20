using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NParty.Database.ESports
{
    [Serializable]
    public abstract class ESportsDbModel<T>
    {
        public static ClassSelect<T> Select()
        {
            return ESportsDb<T>.Instance.Select();
        }

        public static ClassSelect<T> Search(string filter)
        {
            return ESportsDb<T>.Instance.Search(filter);
        }

        public static T WithIdentity(object identity)
        {
            return ESportsDb<T>.Instance.WithIdentity(identity);
        }

        public static T WithMember(string memberName, object memberValue)
        {
            return ESportsDb<T>.Instance.WithMember(memberName, memberValue);
        }

    }
}
