using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VPR.Common;
using VPR.DAL.DbManager;
using VPR.Entity;

namespace VPR.DAL
{
    public static class Transaction
    {
        //private Transaction() { }

        public static DataTable GetAllCargo()
        {
            string ProcName = "uspGetAllCargo";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            return dquery.GetTable();
        }
    }
}
