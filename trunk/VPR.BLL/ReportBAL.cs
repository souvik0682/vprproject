﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using VPR.Entity;
using VPR.DAL;
namespace VPR.BLL
{
    public class ReportBAL
    {
        public List<VesselPosition> GetVesselPosition(SearchCriteria searchCriteria)
        {
                return  new ReportDal().GetVesselPosition(searchCriteria);
            
        }
        public List<VesselPosition> GetDistinctPortsFromVesselPosition(List<VesselPosition> vesselPositions)
        {
            return vesselPositions.GroupBy(x => x.PortName).Select(y => y.First()).ToList(); //vesselPositions.Select(x => x.PortName).Distinct().ToList();
        }

        //public List<CargoReport> GetCargoDetails(List<CargoReport> CargoDetails)
        //{
        //    //return CargoDetails.GroupBy(x => x.PortName).Select(y => y.First()).ToList();
        //    return CargoDetails.ToList();
        //}

        public static List<CargoReportEntity> GetCargoReport(ReportCriteria criteria)
        {
            return ReportDal.GetCargoReport(criteria); //  GetCargoReport(criteria);
            //return ReportDAL.GetCargoReport(criteria);
        }

        public static List<WeeklyReportEntity> GetWeeklyCargoReport(ReportCriteria criteria)
        {
            return ReportDal.GetWeeklyCargoReport(criteria); //  GetCargoReport(criteria);
            //return ReportDAL.GetCargoReport(criteria);
        }

        public DataTable GetAllCargoGroup()
        {
            return ReportDal.GetAllCargoGroup();
        }

        public DataTable GetAllCargo(int GroupID)
        {
            return ReportDal.GetAllCargo(GroupID);
        }

        public DataTable GetAllPorts(string Country)
        {
            return ReportDal.GetAllPorts(Country);
        }

        public DataTable GetAllCountry()
        {
            return ReportDal.GetAllCountry();
        }

        public DataTable GetPASExcelReport(DateTime TranDate, int CargoID, int PortID, string CountryAbbr)
        {
            return ReportDal.GetPASExcelReport(TranDate, CargoID, PortID, CountryAbbr);
        }

        public static List<VesselPosition> GetVPR(ReportCriteria criteria)
        {
            return ReportDal.GetVPR(criteria);
            //return ReportDal.GetVPR(criteria);
            //return ReportDAL.GetCargoReport(criteria);
        }
    }
}
