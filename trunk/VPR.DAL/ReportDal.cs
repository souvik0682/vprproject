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
                oDq.AddVarcharParam("@ActivityStatus", 10, searchCriteria.ActivityName);
                oDq.AddIntegerParam("@Port", searchCriteria.portID);

                //oDq.AddVarcharParam("@ActivityDate", 10, searchCriteria.UserName);
                
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
                oDq.AddVarcharParam("@CountryName", 2, criteria.CountryName);
                oDq.AddIntegerParam("@CargoGroupID", criteria.CargoGroupId);
                oDq.AddIntegerParam("@PortID", criteria.PortId);
                oDq.AddVarcharParam("@Activity", 1, criteria.Activity);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    lstEntity.Add(new CargoReportEntity(reader));
                }

                reader.Close();
            }

            return lstEntity;
          
        }

        public static List<WeeklyReportEntity> GetWeeklyCargoReport(ReportCriteria criteria)
        {
            string strExecution = "[usp_RptCargoWeekly]";
            List<WeeklyReportEntity> lstEntity = new List<WeeklyReportEntity>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddDateTimeParam("@StartDate", criteria.FromDate);
                oDq.AddDateTimeParam("@EndDate", criteria.ToDate);
                oDq.AddIntegerParam("@CargoID", criteria.CargoId);
                oDq.AddVarcharParam("@CountryName", 2, criteria.CountryName);
                oDq.AddIntegerParam("@CargoGroupID", criteria.CargoGroupId);
                oDq.AddIntegerParam("@PortID", criteria.PortId);
                oDq.AddVarcharParam("@Activity", 1, criteria.Activity);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    lstEntity.Add(new WeeklyReportEntity(reader));
                }

                reader.Close();
            }

            return lstEntity;

        }


        public static DataTable GetAllCargoGroup()
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

        public static DataTable GetAllCargo(int GroupID)
        {
            DataTable dt = new DataTable();

            string strExecution = "prcGetOnlyCargo";
            //DateTime dt1 = string.IsNullOrEmpty(StockDate) ? DateTime.Now : Convert.ToDateTime(StockDate);
            //DataSet ds = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))

            //using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CargoGroupID", GroupID);
                dt = oDq.GetTable();
            }

            return dt;
        }

        public static DataTable GetAllPorts(string Country)
        {
            DataTable dt = new DataTable();

            string strExecution = "prcGetAllPorts";
            //DateTime dt1 = string.IsNullOrEmpty(StockDate) ? DateTime.Now : Convert.ToDateTime(StockDate);
            //DataSet ds = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))

            //using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@CountryName", 2, Country);
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

        public static DataTable GetPASExcelReport(DateTime StartDate, int CargoID, int PortID, string CountryAbbr)
        {
            string strExecution = "[dbo].[usp_RptDailyPASReport]";
            DataTable dt = new DataTable();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddDateTimeParam("@PASDate", StartDate);
                oDq.AddIntegerParam("@CargoID", CargoID);
                oDq.AddIntegerParam("@PortID", PortID);
                oDq.AddVarcharParam("@CountryAbbr", 2, CountryAbbr);
                dt = oDq.GetTable();
            }
            return dt;
        }

        public static List<VesselPosition> GetVPR(ReportCriteria criteria)
        {
            string strExecution = "[usp_RptVPR]";
            List<VesselPosition> lstEntity = new List<VesselPosition>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {

                oDq.AddIntegerParam("@PortNo", criteria.PortId);
                oDq.AddVarcharParam("@ActivityStatus", 20, criteria.Activity);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    lstEntity.Add(new VesselPosition(reader));
                }

                reader.Close();
            }

            return lstEntity;

        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
