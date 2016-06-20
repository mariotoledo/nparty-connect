using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NParty.Database.ESports
{
    public class ESportsDb<T> : DatabaseStorage<T>
    {
        private static ESportsDb<T> _instance;
        public static ESportsDb<T> Instance
        {
            get { return _instance ?? (_instance = new ESportsDb<T>()); }
        }

        private ESportsDb()
            : base(new SqlServer(System.Configuration.ConfigurationManager.ConnectionStrings["ESportsDbSqlServer"].ConnectionString), DatabaseAspect<T>.Instance)
        {

        }
    }
}
