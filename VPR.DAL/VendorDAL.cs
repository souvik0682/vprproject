using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using VPR.DAL.DbManager;
using VPR.Entity;
using System.Data;

namespace VPR.DAL
{
    public sealed class VendorDAL
    {
        private VendorDAL()
        { }


        public static int AddEditVndor(IVendor Vendor)
        {
            string strExecution = "[admin].[spAddEditAgent]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@pk_AgentID", Vendor.VendorId);
                //oDq.AddIntegerParam("@CompanyID", Vendor.CompanyID);
                //oDq.AddIntegerParam("@LocationID", Vendor.LocationID);
                oDq.AddNVarcharParam("@AgentName", Vendor.VendorName);
                oDq.AddVarcharParam("@AgentAddress1", 300, Vendor.VendorAddress1);
                oDq.AddVarcharParam("@AgentAddress2", 300, Vendor.VendorAddress2);
                //oDq.AddIntegerParam("@VendorSalutation", Vendor.VendorSalutation);
                oDq.AddVarcharParam("@AgentCity", 50, Vendor.City);
                oDq.AddVarcharParam("@AgentState", 50, Vendor.State);
                oDq.AddIntegerParam("@fk_Countryid", Vendor.fk_CountryID);
                //oDq.AddVarcharParam("@Acno", 30, Vendor.Phone);
                oDq.AddVarcharParam("@AgentMobile", 30, Vendor.Mobile);
                oDq.AddVarcharParam("@AgentMailID", 50, Vendor.EmailID);
                oDq.AddVarcharParam("@AgentPhone", 30, Vendor.Phone);
                //oDq.AddVarcharParam("@IEC", 20, Vendor.IEC);
                //oDq.AddVarcharParam("@CP", 50, Vendor.CP);

                oDq.AddBooleanParam("@AgentStatus", Vendor.VendorActive);
                if (Vendor.VendorId <= 0)
                {
                    oDq.AddIntegerParam("@CreatedBy", Vendor.CreatedBy);
                    oDq.AddDateTimeParam("@CreatedOn", Vendor.CreatedOn);
                }
                oDq.AddIntegerParam("@ModifiedBy", Vendor.ModifiedBy);
                oDq.AddDateTimeParam("@ModifiedOn", Vendor.ModifiedOn);
                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@RESULT"));
            }
            return Result;
        }

        public static List<IVendor> GetVendor(SearchCriteria searchCriteria, int ID)
        {
            string strExecution = "[admin].[spGetVendor]";
            List<IVendor> lstVnd = new List<IVendor>();

            //using (DbQuery oDq = new DbQuery(strExecution))
            //{
            //    //=========================================================
                
            //    oDq.AddIntegerParam("@VendorId", ID);
            //    oDq.AddVarcharParam("@SchName", 100, searchCriteria.LocAbbr);
            //    oDq.AddVarcharParam("@SchLoc", 100, searchCriteria.LocName);
            //    oDq.AddVarcharParam("@SortExpression", 4, searchCriteria.SortExpression);
            //    oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
            //    DataTable dtAdd = oDq.GetTable();
            //}



            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@VendorId", ID);
                oDq.AddVarcharParam("@SchName", 100, searchCriteria.LocAbbr);
                oDq.AddVarcharParam("@SchLoc", 100, searchCriteria.LocName);
                oDq.AddVarcharParam("@SortExpression", 10, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IVendor Vnd = new VendorEntity(reader);
                    lstVnd.Add(Vnd);
                }
                reader.Close();  
            }
            return lstVnd;
        }

        public static IVendor GetVendor(int ID)
        {
            string strExecution = "[admin].[spGetVendor]";
            //List<IVendor> lstVnd = new List<IVendor>();
            IVendor Vnd = null;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@VendorId", ID);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    Vnd = new VendorEntity(reader);
                }
                reader.Close();
            }
            return Vnd;
        }



        public static int DeleteVndor(int VendorId)
        {
            string strExecution = "[admin].[spDeleteVendor]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@VendorId", VendorId);
                Result = oDq.RunActionQuery();
            }
            return Result;
        }
    }
}
