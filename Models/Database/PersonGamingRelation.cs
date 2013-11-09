using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EixoX.Restrictions;
using EixoX.Data;
using EixoX.Interceptors;
using EixoX.UI;

namespace CampeonatosNParty.Models.Database
{
    [DatabaseTable]
    public class PersonGamingRelation : NPartyDbModel<PersonGamingRelation>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public int Id { get; set; }

        [DatabaseColumn]
        [UIHidden]
        public int PersonId1 { get; set; }

        [DatabaseColumn]
        [UIHidden]
        public int PersonId2 { get; set; }

        [DatabaseColumn]
        public bool isPSN { get; set; }

        [DatabaseColumn]
        public bool isLive { get; set; }

        [DatabaseColumn]
        public bool isMiiverse { get; set; }

        [DatabaseColumn]
        public bool isFriendCode { get; set; }

        public static PersonGamingRelation getPersonGamingRelations(Usuarios user1, Usuarios user2)
        {
            if (user1.Id == user2.Id)
                return null;

            PersonGamingRelation relation = null;

            int relationId = Convert.ToInt32(NPartyDb<ClassificacaoPorJogador>.Instance.Database.ExecuteScalarText(
                    "SELECT ISNULL([dbo].[GetGamingRelationId] (" + user1.Id + "," + user2.Id + "), 0)"));

            if (relationId > 0)
            {
                relation = PersonGamingRelation.WithIdentity(relationId);
            }

            return relation;
        }
    }
}