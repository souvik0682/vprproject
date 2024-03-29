﻿using System;
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
                if (!ReferenceEquals(searchCriteria, null))
                {
                    oDq.AddVarcharParam("@VesselActivity", 10, searchCriteria.ActivityName);
                    oDq.AddIntegerParam("@Port", searchCriteria.portID);

                    //oDq.AddVarcharParam("@ActivityDate", 10, searchCriteria.UserName);
                }

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
                oDq.AddIntegerParam("@CargoSubGroupID", criteria.SubGroupID);
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
                oDq.AddIntegerParam("@SubGroup", criteria.SubGroupID);
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

        public static DataTable GetAllMailGroup(int SubGroupID)
        {
            DataTable dt = new DataTable();

            string strExecution = "prcGetOnlyMailGroup";
            //DateTime dt1 = string.IsNullOrEmpty(StockDate) ? DateTime.Now : Convert.ToDateTime(StockDate);
            //DataSet ds = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))

            //using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@SubGroupID", SubGroupID);
                dt = oDq.GetTable();
            }

            return dt;
        }

        public static DataTable GetAllCargoSubGroup(int GroupID)
        {
            DataTable dt = new DataTable();

            string strExecution = "prcGetCargoSubGroup";
            //DateTime dt1 = string.IsNullOrEmpty(StockDate) ? DateTime.Now : Convert.ToDateTime(StockDate);
            //DataSet ds = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))

            //using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@GroupID", GroupID);
                dt = oDq.GetTable();
            }

            return dt;
        }

        public static DataTable GetAllCargo(int GroupID, int SubGroupID)
        {
            DataTable dt = new DataTable();

            string strExecution = "prcGetOnlyCargo";
            //DateTime dt1 = string.IsNullOrEmpty(StockDate) ? DateTime.Now : Convert.ToDateTime(StockDate);
            //DataSet ds = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))

            //using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CargoGroupID", GroupID);
                oDq.AddIntegerParam("@SubGroupID", SubGroupID);
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

        public static DataTable GetPASExcelReport(DateTime StartDate, DateTime EndDate, int CargoID, int PortID, string CountryAbbr, string Status)
        {
            string strExecution = "[dbo].[usp_RptDailyPASReport]";
            DataTable dt = new DataTable();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddDateTimeParam("@PASDate", StartDate);
                oDq.AddDateTimeParam("@PASDate1", EndDate);
                oDq.AddIntegerParam("@CargoID", CargoID);
                oDq.AddIntegerParam("@PortID", PortID);
                oDq.AddVarcharParam("@CountryAbbr", 2, CountryAbbr);
                oDq.AddVarcharParam("@Status", 1, Status);
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

        public static DataTable GetEmailIDs(ReportCriteria criteria)
        {
            string strExecution = "[dbo].[GetMailIDFromEmailGroup]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@EmailGroupID", criteria.EmailGroupID);
                oDq.AddIntegerParam("@CountryId", criteria.CountryId);
                oDq.AddIntegerParam("@CargoGroupId", criteria.CargoGroupId);
                oDq.AddIntegerParam("@CargoSubGroupID", criteria.SubGroupID);


                myDataTable = oDq.GetTable();
            }
            return myDataTable;
        }

        public static void SendForceEmail(int EmailGroupId, string EmailIds, string AttachmentFileName)
        {
            string strExecution = "[dbo].[usp_SendForceEmail]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@EmailGroupId", EmailGroupId);
                oDq.AddVarcharParam("@EmailIds", 2000, EmailIds);
                oDq.AddVarcharParam("@AttachmentFile", 500, AttachmentFileName);

                oDq.RunActionQuery();
            }
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
