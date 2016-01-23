using NParty.Www.Controllers;
using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.Nintendo.Controllers
{
    public class PokemonController : NintendoController
    {
        //
        // GET: /Nintendo/Pokemon/

        public ActionResult Index(int? page)
        {
            ViewData["DailyMusicVideoId"] = GetDailyPokemonMusic()[DateTime.Now.Day];
            return OpenPageByLabel("Pokémon", page);
        }

        public ActionResult Timeline() { return View(); }

        private Dictionary<int, string> GetDailyPokemonMusic()
        {
            Dictionary<int, string> youtubeVideoIdByDay = new Dictionary<int, string>();

            youtubeVideoIdByDay.Add(1, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(2, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(3, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(4, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(5, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(6, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(7, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(8, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(9, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(10, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(11, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(12, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(13, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(14, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(15, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(16, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(17, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(18, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(19, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(20, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(21, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(22, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(23, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(24, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(25, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(26, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(27, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(28, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(29, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(30, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(31, "vXJgQLknzfI");

            return youtubeVideoIdByDay;
        }

    }
}
