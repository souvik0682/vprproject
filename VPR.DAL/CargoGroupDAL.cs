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
    public sealed class CargoGroupDAL
    {
        private CargoGroupDAL()
        {
        }

        public static int SaveCargo(ICargoGroup Cargo)
        {
            int CargoId = 0;
            string strExecution = "[admin].[prcAddEditCargo]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                if (Cargo.pk_CargoID > 0)
                    oDq.AddIntegerParam("@pk_CargoId", Cargo.pk_CargoID);
                else
                    oDq.AddIntegerParam("@pk_CargoId", 0);
                oDq.AddIntegerParam("@fk_CargoGroupID", Cargo.pk_CargoGroupID);
                oDq.AddVarcharParam("@CargoName", 500, Cargo.CargoName);
                oDq.AddIntegerParam("@fk_CargoSubGroupID", Cargo.pk_CargoSubGroupID);

                //oDq.AddBooleanParam("@CargoStatus", Cargo.CargoStatus);
                oDq.AddIntegerParam("@CreatedBy", Cargo.CreatedBy);
                ///oDq.AddDateTimeParam("@CreatedOn", EmailGroup.CreatedOn);
                oDq.AddIntegerParam("@ModifiedBy", Cargo.ModifiedBy);
                //oDq.AddDateTimeParam("@ModifiedOn", EmailGroup.ModifiedOn);

                CargoId = Convert.ToInt32(oDq.GetScalar());
                return CargoId;
            }
        }

        public static bool IsCargoExists(string CargoName)
        {
            string strExecution = "usp_CargoExists";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@CargoName", 500, CargoName);

                return Convert.ToBoolean(oDq.GetScalar());
            }
        }

        public static ICargoGroup GetCargo(int CargoId)
        {
            string strExecution = "[admin].[prcGetCargo]";
            ICargoGroup objGroup = new CargoGroupEntity();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@pk_CargoId", CargoId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    objGroup = new CargoGroupEntity(reader);
                }
                reader.Close();
            }
            return objGroup;
        }
        
        public static DataTable GetAllCargoSubGroup(int CargoGroupID)
        {
            string strExecution = "[dbo].[uspGetAllCargoSubGroups]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@fk_CargoGroupId", CargoGroupID);
                return oDq.GetTable();
            }
        }


    }
}
