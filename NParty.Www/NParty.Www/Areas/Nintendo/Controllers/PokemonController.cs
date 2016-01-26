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

        public ActionResult PokePartyDay() { return View(); }

        private Dictionary<int, string> GetDailyPokemonMusic()
        {
            Dictionary<int, string> youtubeVideoIdByDay = new Dictionary<int, string>();

            youtubeVideoIdByDay.Add(1, "E_Xa6XECzSg");
            youtubeVideoIdByDay.Add(2, "hLcJois9yTs");
            youtubeVideoIdByDay.Add(3, "JNJJ-QkZ8cM");
            youtubeVideoIdByDay.Add(4, "7bRctqZpTM8");
            youtubeVideoIdByDay.Add(5, "e_5TVp1wSTU");
            youtubeVideoIdByDay.Add(6, "wbRe5a7Uz4I");
            youtubeVideoIdByDay.Add(7, "LW_AA63X5ko");
            youtubeVideoIdByDay.Add(8, "8UOphzqvKYo");
            youtubeVideoIdByDay.Add(9, "46LXLha2FEY");
            youtubeVideoIdByDay.Add(10, "pNFFwrlJ8mI");
            youtubeVideoIdByDay.Add(11, "jThq5D86jBM");
            youtubeVideoIdByDay.Add(12, "L3fiaPBDfv8");
            youtubeVideoIdByDay.Add(13, "dKW-reR_jUQ");
            youtubeVideoIdByDay.Add(14, "4C330hWmES4");
            youtubeVideoIdByDay.Add(15, "l0yo32iVGA0");
            youtubeVideoIdByDay.Add(16, "DJUxTo0tTOs");
            youtubeVideoIdByDay.Add(17, "5mbt-VCp_KQ");
            youtubeVideoIdByDay.Add(18, "Uir_5l6zeZw");
            youtubeVideoIdByDay.Add(19, "SE5dBmWml34");
            youtubeVideoIdByDay.Add(20, "rXefFHRgyE0");
            youtubeVideoIdByDay.Add(21, "465uaHc-r6A");
            youtubeVideoIdByDay.Add(22, "qk4Xfqvph8U");
            youtubeVideoIdByDay.Add(23, "rgLsfd5uplQ");
            youtubeVideoIdByDay.Add(24, "qDZygFP1iNQ");
            youtubeVideoIdByDay.Add(25, "llnXhrCn9Yo");
            youtubeVideoIdByDay.Add(26, "Wj24k5Vt-y0");
            youtubeVideoIdByDay.Add(27, "GIn8_Q27WFY");
            youtubeVideoIdByDay.Add(28, "9sX_54hHcEE");
            youtubeVideoIdByDay.Add(29, "WxPOSrrWjJY");
            youtubeVideoIdByDay.Add(30, "L3fiaPBDfv8");
            youtubeVideoIdByDay.Add(31, "5mbt-VCp_KQ");

            return youtubeVideoIdByDay;
        }

    }
}
