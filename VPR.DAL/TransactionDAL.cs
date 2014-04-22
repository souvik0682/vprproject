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
    public sealed class TransactionDAL
    {
        private TransactionDAL() { }

        public static DataTable GetAllCargo()
        {
            string ProcName = "usp_GetAllCargo";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            return dquery.GetTable();
        }

        public static DataTable GetAllBerth()
        {
            string ProcName = "usp_GetAllBerth";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            return dquery.GetTable();
        }

        public static DataTable GetAllAgent()
        {
            string ProcName = "usp_GetAllAgent";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            return dquery.GetTable();
        }

        public static int GetPortId(string PortCode)
        {
            string strExecution = "uspGetPortIdByPortCode";
            int portId = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@PortCode", 6, PortCode);

                portId = Convert.ToInt32(oDq.GetScalar());
            }
            return portId;
        }

        public static string GetPortNameById(Int64 PortId)
        {
            string strExecution = "uspGetPortNameByPortId";
            string portName = string.Empty;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@PortId", PortId);

                portName = Convert.ToString(oDq.GetScalar());
            }

            return portName;
        }

        public static List<CargoDetails> GetListOfCargo(int VesselId)
        {
            string strExecution = "usp_GetCargoDetails";
            List<CargoDetails> lstCargo = new List<CargoDetails>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@VesselId", VesselId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    CargoDetails cargo = new CargoDetails(reader);
                    lstCargo.Add(cargo);
                }

                reader.Close();
            }

            return lstCargo;
        }

        public static VesselEntity GetVessel(int VesselId)
        {
            string strExecution = "usp_GetVessel";
            VesselEntity o = new VesselEntity();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@VesselId", VesselId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    o = new VesselEntity(reader);
                }
                reader.Close();
            }
            return o;
        }

        public static int SaveVessel(VesselEntity o)
        {
            int vesselId = 0;
            string strExecution = "usp_SaveVessel";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                if (o.VesselId > 0)
                    oDq.AddIntegerParam("@VesselId", o.VesselId);

                oDq.AddVarcharParam("@VPRorPAS", 1, o.VPRorPAS);
                oDq.AddVarcharParam("@Activity", 2, o.Activity);
                oDq.AddIntegerParam("@PortId", o.PortId);
                oDq.AddVarcharParam("@VesselName", 50, o.VesselName);
                oDq.AddIntegerParam("@BerthId", o.BerthId);
                oDq.AddDecimalParam("@LOA", 12, 2, o.LOA);
                oDq.AddDateTimeParam("@ArrivalDate", o.ArrivalDate);
                oDq.AddVarcharParam("@Owner", 50, o.Owner);
                oDq.AddIntegerParam("@AgentId", o.AgentId);
                oDq.AddIntegerParam("@PrevPortId", o.PrevPortId);
                oDq.AddIntegerParam("@NxtPortId", o.NextPortId);
                oDq.AddVarcharParam("@Remarks", 5000, o.Remarks);
                oDq.AddVarcharParam("@VoyageNo", 50, o.VoyageNo);

                oDq.AddIntegerParam("@CreatedBy", o.CreatedBy);
                oDq.AddIntegerParam("@ModifiedBy", o.ModifiedBy);

                vesselId = Convert.ToInt32(oDq.GetScalar());
                return vesselId;
            }
        }

        public static int SaveCargo(CargoDetails o, int type)
        {
            int cargoVesselId = 0;
            string strExecution = "usp_SaveCargo";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CargoVesselID", o.CargoVesselId);
                oDq.AddIntegerParam("@VesselId", o.VesselId);
                oDq.AddIntegerParam("@CargoId", o.CargoId);
                oDq.AddDecimalParam("@Quantity", 12, 2, o.Quantity);
                oDq.AddVarcharParam("@ActType", 1, o.ActType);
                oDq.AddIntegerParam("@Type", type);

                cargoVesselId = Convert.ToInt32(oDq.GetScalar());
                return cargoVesselId;
            }
        }

        public static List<VesselEntity> GetVessles(SearchCriteria searchCriteria)
        {
            string strExecution = "uspGetVesselList";
            List<VesselEntity> lstEg = new List<VesselEntity>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@VesselName", 500, searchCriteria.VesselName);
                oDq.AddVarcharParam("@Port", 200, searchCriteria.Port);
                oDq.AddVarcharParam("@Agent", 200, searchCriteria.Agent);

                oDq.AddVarcharParam("@SortExpression", 100, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 100, searchCriteria.SortDirection);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    VesselEntity eg = new VesselEntity(reader);
                    lstEg.Add(eg);
                }
                reader.Close();
            }
            return lstEg;
        }

        public static void DeleteVessel(int vesselId)
        {
            string strExecution = "usp_DeleteVessel";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@VesselId", vesselId);
                oDq.RunActionQuery();
            }
        }
    }
}
