using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Database
{
    public class NPartyDb<T> : DatabaseStorage<T>
    {
        private static NPartyDb<T> _instance;
        public static NPartyDb<T> Instance
        {
            get { return _instance ?? (_instance = new NPartyDb<T>()); }
        }

        private NPartyDb()
            : base(new SqlServer(System.Configuration.ConfigurationManager.ConnectionStrings["NPartyCampeonatos"].ConnectionString), DatabaseAspect<T>.Instance)
        {

        }
    }
}