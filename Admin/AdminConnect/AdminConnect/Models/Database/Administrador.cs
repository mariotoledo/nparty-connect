using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EixoX.Restrictions;
using EixoX.Data;
using EixoX.Interceptors;
using EixoX.UI;
using CampeonatosNParty.Helpers;
using AdminConnect.Models.Attributes;

namespace CampeonatosNParty.Models.Database
{
    [DatabaseTable]
    public class Administrador : NPartyDbModel<Administrador>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        [UIHidden]
        public int Id { get; set; }

        [DatabaseColumn]
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        [UISingleline]
        public string Nome { get; set; }

        [DatabaseColumn]
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        [UISingleline]
        public string Sobrenome { get; set; }

        [DatabaseColumn]
        [MaxLength(50)]
        [Required]
        [UISingleline]
        [Email]
        public string Email { get; set; }

        [DatabaseColumn]
        [MaxLength(255)]
        [MinLength(6)]
        [UIPassword]
        [Required]
        public string Senha { get; set; }

        [DatabaseColumn]
        public bool PodeCriarEventos { get; set; }

        [DatabaseColumn]
        public bool PodeEditarEventos { get; set; }

        [DatabaseColumn]
        public bool PodeDeletarEventos { get; set; }

        [DatabaseColumn]
        public bool AdminSupremo { get; set; }

        [DatabaseColumn]
        public bool PodeInserirUsuario { get; set; }

        [DatabaseColumn]
        public bool PodeEditarUsuario { get; set; }

        [DatabaseColumn]
        public bool PodeRemoverUsuario { get; set; }

        [DatabaseColumn]
        public bool PodeCriarCampeonato { get; set; }

        [DatabaseColumn]
        public bool PodeEditarCampeonato { get; set; }

        [DatabaseColumn]
        public bool PodeDeletarCampeonato { get; set; }

        [DatabaseColumn]
        public long IdOrganizador { get; set; }

        public string getFullName()
        {
            return Nome + " " + Sobrenome;
        }

        public bool CheckPermision(AccessType accessType)
        {
            if (AdminSupremo)
                return true;

            if (accessType == AccessType.CanCreateEvent && PodeCriarEventos)
                return true;

            if (accessType == AccessType.CanEditEvent && PodeEditarEventos)
                return true;

            if (accessType == AccessType.CanRemoveEvent && PodeDeletarEventos)
                return true;

            if (accessType == AccessType.CanCreateUser && PodeInserirUsuario)
                return true;

            if (accessType == AccessType.CanEditUser && PodeEditarUsuario)
                return true;

            if (accessType == AccessType.CanRemoveUser && PodeRemoverUsuario)
                return true;

            if (accessType == AccessType.CanCreateChampionship && PodeCriarCampeonato)
                return true;

            if (accessType == AccessType.CanEditChampionship && PodeEditarEventos)
                return true;

            if (accessType == AccessType.CanRemoveChampionship && PodeDeletarCampeonato)
                return true;

            return false;
        }
    }
}