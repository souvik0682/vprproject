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
    public class ReportDal : IDisposable
    {
        public List<VesselPosition> GetVesselPosition(SearchCriteria searchCriteria)
        {
            string strExecution = "[dbo].[usp_RptVesselPosition]";
            List<VesselPosition> lstVesselPosition = new List<VesselPosition>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                //oDq.AddBooleanParam("@IsActiveOnly", isActiveOnly);
                //oDq.AddVarcharParam("@SchUserName", 10, searchCriteria.UserName);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {

                    lstVesselPosition.Add(new VesselPosition(reader));
                }

                reader.Close();
            }

            return lstVesselPosition;
        }

        public static List<CargoReportEntity> GetCargoReport(ReportCriteria criteria)
        {
            string strExecution = "[usp_RptCargoHandled]";
            List<CargoReportEntity> lstEntity = new List<CargoReportEntity>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddDateTimeParam("@StartDate", criteria.FromDate);
                oDq.AddDateTimeParam("@EndDate", criteria.ToDate);
                oDq.AddIntegerParam("@CargoID", criteria.CargoId);
                oDq.AddIntegerParam("@CountryID", criteria.CountryId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    lstEntity.Add(new CargoReportEntity(reader));
                }

                reader.Close();
            }

            return lstEntity;
          
        }

        public static DataTable GetAllCargo()
        {
            DataTable dt = new DataTable();

            string strExecution = "prcGetOnlyCargoGroup";
            //DateTime dt1 = string.IsNullOrEmpty(StockDate) ? DateTime.Now : Convert.ToDateTime(StockDate);
            //DataSet ds = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))

            //using (DbQuery oDq = new DbQuery(strExecution))
            {
                  dt = oDq.GetTable();
            }

            return dt;
        }

        public static DataTable GetAllCountry()
        {
            DataTable dt = new DataTable();

            string strExecution = "prcGetOnlyCountry";
            //DateTime dt1 = string.IsNullOrEmpty(StockDate) ? DateTime.Now : Convert.ToDateTime(StockDate);
            //DataSet ds = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))

            //using (DbQuery oDq = new DbQuery(strExecution))
            {
                dt = oDq.GetTable();
            }

            return dt;
        }
        public void Dispose()
        {
            this.Dispose();
        }
    }
}
