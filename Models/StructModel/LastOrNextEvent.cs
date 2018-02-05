using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.StructModel
{
    public class LastOrNextEvent
    {
        public string eventDate {get; set;}
        public bool isNextEvent { get; set; }
        public string eventName { get; set; }
        public string eventLocation { get; set; }
        public string eventImageUrl { get; set; }
        public string eventUrl { get; set; }
        public string eventType { get; set; }
    }
}