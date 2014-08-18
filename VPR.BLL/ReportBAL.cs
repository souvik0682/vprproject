using System;
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

        public DataTable GetEmailIDs(ReportCriteria criteria)
        {
            return ReportDal.GetEmailIDs(criteria);
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

        public DataTable GetAllCargoSubGroup(int GroupID)
        {
            return ReportDal.GetAllCargoSubGroup(GroupID);
        }

        public DataTable GetAllMailGroup(int SubGroupID)
        {
            return ReportDal.GetAllMailGroup(SubGroupID);
        }

        public DataTable GetAllCargo(int GroupID, int SubGroupID)
        {
            return ReportDal.GetAllCargo(GroupID, SubGroupID);
        }

        public DataTable GetAllPorts(string Country)
        {
            return ReportDal.GetAllPorts(Country);
        }

        public DataTable GetAllCountry()
        {
            return ReportDal.GetAllCountry();
        }

        public DataTable GetPASExcelReport(DateTime TranDate, DateTime TranDate1, int CargoID, int PortID, string CountryAbbr, string Status)
        {
            return ReportDal.GetPASExcelReport(TranDate, TranDate1, CargoID, PortID, CountryAbbr, Status);
        }

        public static List<VesselPosition> GetVPR(ReportCriteria criteria)
        {
            return ReportDal.GetVPR(criteria);
            //return ReportDal.GetVPR(criteria);
            //return ReportDAL.GetCargoReport(criteria);
        }

        public void SendForceEmail(int EmailGroupId, string EmailIds, string AttachmentFileName)
        {
            ReportDal.SendForceEmail(EmailGroupId, EmailIds, AttachmentFileName);
        }
    }
}
