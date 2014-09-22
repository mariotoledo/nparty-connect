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
    public class PokemonTradeOfferItem : NPartyDbModel<PokemonTradeOfferItem>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public int Id { get; set; }

        [DatabaseColumn]
        public int PersonId { get; set; }

        [DatabaseColumn]
        public string PersonName { get; set; }

        [DatabaseColumn]
        public int PokemonId { get; set; }

        [DatabaseColumn]
        public string Name { get; set; }

        [DatabaseColumn]
        public string PokedexNumber { get; set; }

        [DatabaseColumn]
        public int PokemonLevel { get; set; }

        [DatabaseColumn]
        public string PokemonNature { get; set; }

        [DatabaseColumn]
        public int PokemonNatureId { get; set; }

        [DatabaseColumn]
        public string PersonMessage { get; set; }

        [DatabaseColumn]
        public bool IsEgg { get; set; }

        [DatabaseColumn]
        public bool HasIvHp { get; set; }

        [DatabaseColumn]
        public bool HasIvAttack { get; set; }

        [DatabaseColumn]
        public bool HasIvDefense { get; set; }

        [DatabaseColumn]
        public bool HasIvSpAttack { get; set; }

        [DatabaseColumn]
        public bool HasIvSpDefense { get; set; }

        [DatabaseColumn]
        public bool HasIvSpeed { get; set; }

        [DatabaseColumn]
        public int HpEv { get; set; }

        [DatabaseColumn]
        public int AttackEv { get; set; }

        [DatabaseColumn]
        public int DefenseEv { get; set; }

        [DatabaseColumn]
        public int SpAttackEv { get; set; }

        [DatabaseColumn]
        public int SpDefenseEv { get; set; }

        [DatabaseColumn]
        public int SpeedEv { get; set; }

        [DatabaseColumn]
        public string ShinyValue { get; set; }

        [DatabaseColumn]
        public bool IsShiny { get; set; }

        public string getImageUrl()
        {
            return "http://www.serebii.net/xy/pokemon/" + PokedexNumber + ".png";
        }

        public string getIvsString()
        {
            string iv = "";
            if (HasIvHp)
                iv = iv + "Hp, ";
            if (HasIvAttack)
                iv = iv + "Attack, ";
            if (HasIvDefense)
                iv = iv + "Defense, ";
            if (HasIvSpAttack)
                iv = iv + "Sp. Attack, ";
            if (HasIvSpDefense)
                iv = iv + "Sp. Defense, ";
            if (HasIvSpeed)
                iv = iv + "Speed, ";

            if (string.IsNullOrEmpty(iv))
                iv = "Nenhum";
            else
            {
                iv = iv.Substring(0, iv.Length - 2);
            }

            return iv;
        }
    }
}