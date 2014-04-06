﻿using System;
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
            string strExecution = "[mst].[spAddEditVendor]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@VendorId", Vendor.VendorId);
                oDq.AddIntegerParam("@CompanyID", Vendor.CompanyID);
                oDq.AddIntegerParam("@LocationID", Vendor.LocationID);
                oDq.AddNVarcharParam("@VendorName", Vendor.VendorName);
                oDq.AddNVarcharParam("@VendorAddress", Vendor.VendorAddress);
                oDq.AddIntegerParam("@VendorSalutation", Vendor.VendorSalutation);
                oDq.AddVarcharParam("@VendorType", 5, Vendor.VendorType);
                oDq.AddVarcharParam("@CFSCode", 10, Vendor.CFSCode);
                oDq.AddIntegerParam("@Terminalid", Vendor.Terminalid);
                oDq.AddVarcharParam("@Acno", 20, Vendor.AcNo);
                oDq.AddVarcharParam("@AcType", 20, Vendor.AcType);
                oDq.AddVarcharParam("@PAN", 20, Vendor.PAN);
                oDq.AddVarcharParam("@TANo", 20, Vendor.TANo);
                oDq.AddVarcharParam("@BIN", 20, Vendor.BIN);
                oDq.AddVarcharParam("@Mobile", 20, Vendor.Mobile);
                oDq.AddVarcharParam("@EmailID", 50, Vendor.EmailID);
                oDq.AddVarcharParam("@BankName", 50, Vendor.BankName);
                oDq.AddVarcharParam("@IEC", 20, Vendor.IEC);
                oDq.AddVarcharParam("@CP", 50, Vendor.CP);

                oDq.AddBooleanParam("@VendorActive", Vendor.VendorActive);
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
            string strExecution = "[mst].[spGetVendor]";
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
            string strExecution = "[mst].[spGetVendor]";
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
            string strExecution = "[mst].[spDeleteVendor]";
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
