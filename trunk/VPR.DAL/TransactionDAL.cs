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

        public static DataTable GetAllLocation()
        {
            string ProcName = "GetAllLocation";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            return dquery.GetTable();
        }

        public static DataTable GetPortWithTransaction()
        {
            string ProcName = "usp_getPortWithTransaction";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            return dquery.GetTable();
        }

        public static DataTable GetAllVesselPrefix()
        {
            string ProcName = "usp_GetAllVesselPrefix";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            return dquery.GetTable();
        }

        public static DataTable GetAllJob()
        {
            string ProcName = "usp_GetAllJob";
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

        public static string GetOnlyPortNameById(Int64 PortId)
        {
            string strExecution = "uspGetPortNameOnlyByPortId";
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

        public static VesselEntity GetPASVessel(int VesselId)
        {
            string strExecution = "usp_GetPASVessel";
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

        public static VesselEntity GetPASList(int VesselId)
        {
            string strExecution = "usp_GetPASVessel";
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
                oDq.AddIntegerParam("@VesselPrefix", o.VesselPrefix);
                oDq.AddVarcharParam("@Activity", 2, o.Activity);
                oDq.AddIntegerParam("@PortId", o.PortId);
                oDq.AddVarcharParam("@VesselName", 50, o.VesselName);
                oDq.AddIntegerParam("@BerthId", o.BerthId);
                oDq.AddDecimalParam("@LOA", 12, 2, o.LOA);
                oDq.AddDateTimeParam("@ETA", o.ETA);
                oDq.AddDateTimeParam("@ETB", o.BerthDate);
                oDq.AddDateTimeParam("@ETC", o.ETC);
                oDq.AddDateTimeParam("@SailDate", o.SailDate);
                oDq.AddVarcharParam("@Owner", 50, o.Owner);
                oDq.AddIntegerParam("@AgentId", o.AgentId);
                oDq.AddIntegerParam("@PrevPortId", o.PrevPortId);
                oDq.AddIntegerParam("@NxtPortId", o.NextPortId);
                oDq.AddVarcharParam("@Remarks", 5000, o.Remarks);
                oDq.AddVarcharParam("@VoyageNo", 50, o.VoyageNo);
                oDq.AddIntegerParam("@LocID", o.LocID);
                oDq.AddIntegerParam("@CreatedBy", o.CreatedBy);
                oDq.AddIntegerParam("@ModifiedBy", o.ModifiedBy);

                vesselId = Convert.ToInt32(oDq.GetScalar());
                return vesselId;
            }
        }

        public static int SavePASVessel(VesselEntity o)
        {
            int vesselId = 0;
            string strExecution = "usp_SavePASVessel";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                if (o.VesselId > 0)
                    oDq.AddIntegerParam("@VesselId", o.VesselId);

                oDq.AddVarcharParam("@VPRorPAS", 1, o.VPRorPAS);
                oDq.AddVarcharParam("@Activity", 2, o.Activity);
                oDq.AddIntegerParam("@PortId", o.PortId);
                oDq.AddIntegerParam("@PrevPortId", o.PrevPortId);
                oDq.AddIntegerParam("@NxtPortId", o.NextPortId);
                oDq.AddVarcharParam("@Owner", 50, o.Owner);
                oDq.AddVarcharParam("@VesselName", 50, o.VesselName);
                oDq.AddDateTimeParam("@ETA", o.ETA);
                oDq.AddDateTimeParam("@ETB", o.ETB);
                oDq.AddDateTimeParam("@ETC", o.ETC);
                oDq.AddDateTimeParam("@ArrivalDate", o.ArrivalDate);
                oDq.AddDateTimeParam("@BerthDate", o.BerthDate);
                oDq.AddDateTimeParam("@SailDate", o.SailDate);
                oDq.AddVarcharParam("@NominatingCo", 50, o.NominatingCo);
                oDq.AddVarcharParam("@AppointingCo", 50, o.AppointingCo);
                oDq.AddVarcharParam("@Shipper", 50, o.Shipper);
                oDq.AddVarcharParam("@Remarks", 5000, o.Remarks);
                oDq.AddIntegerParam("@JobId", o.JobID);
                oDq.AddIntegerParam("@NomCountryID", o.NominatingCountry);
                oDq.AddIntegerParam("@AppCountryID", o.AppointingCountry);
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

        public static List<VesselEntity> GetPASList(SearchCriteria searchCriteria)
        {
            string strExecution = "uspGetPASList";
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

        public static List<VesselEntity> GetVessles(SearchCriteria searchCriteria)
        {
            string strExecution = "uspGetVesselList";
            List<VesselEntity> lstEg = new List<VesselEntity>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@VesselName", 500, searchCriteria.VesselName);
                oDq.AddVarcharParam("@Port", 200, searchCriteria.Port);
                oDq.AddVarcharParam("@Agent", 200, searchCriteria.AgentName);
                oDq.AddVarcharParam("@VesselStatus", 1, searchCriteria.VesselStatus);
                oDq.AddIntegerParam("@PortID", searchCriteria.portID);
                //oDq.AddIntegerParam("@LocationID", searchCriteria.LocationID);
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

        public static List<VesselEntity> GetPASVessels(SearchCriteria searchCriteria)
        {
            string strExecution = "uspGetPASVesselList";
            List<VesselEntity> lstEg = new List<VesselEntity>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@VesselName", 500, searchCriteria.VesselName);
                oDq.AddVarcharParam("@Port", 200, searchCriteria.Port);
                oDq.AddVarcharParam("@ActivityName", 200, searchCriteria.ActivityName);
                oDq.AddVarcharParam("@VesselStatus", 200, searchCriteria.StringOption1);
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

        public static List<VesselMovementEntity> GetPASVesselMovement(SearchCriteria searchCriteria)
        {
            string strExecution = "getPASVesselMovementList";
            List<VesselMovementEntity> lstEg = new List<VesselMovementEntity>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@pk_PASTranID", 0);
                oDq.AddVarcharParam("@SchVesselName", 500, searchCriteria.VesselName);
                oDq.AddVarcharParam("@SchPortName", 200, searchCriteria.Port);
                oDq.AddVarcharParam("@SchVesselActivity", 200, searchCriteria.ActivityName);
                oDq.AddVarcharParam("@SortExpression", 100, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 100, searchCriteria.SortDirection);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    VesselMovementEntity eg = new VesselMovementEntity(reader);
                    lstEg.Add(eg);
                }
                reader.Close();
            }
            return lstEg;
        }

        //public static List<PASEntity> GetVessles(SearchCriteria searchCriteria)
        //{
        //    string strExecution = "uspGetPAS";
        //    List<PASEntity> lstEg = new List<PASEntity>();

        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddVarcharParam("@VesselName", 500, searchCriteria.VesselName);
        //        oDq.AddVarcharParam("@Port", 200, searchCriteria.Port);
        //        oDq.AddVarcharParam("@Agent", 200, searchCriteria.Agent);

        //        oDq.AddVarcharParam("@SortExpression", 100, searchCriteria.SortExpression);
        //        oDq.AddVarcharParam("@SortDirection", 100, searchCriteria.SortDirection);

        //        DataTableReader reader = oDq.GetTableReader();

        //        while (reader.Read())
        //        {
        //            PASEntity eg = new PASEntity(reader);
        //            lstEg.Add(eg);
        //        }
        //        reader.Close();
        //    }
        //    return lstEg;
        //}

        public static void DeleteVessel(int vesselId)
        {
            string strExecution = "usp_DeletePASVessel";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@VesselId", vesselId);
                oDq.RunActionQuery();
            }
        }

        public static void DeletePASMovement(int PASMovementId)
        {
            string strExecution = "usp_DeletePASMovement";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@PASTranId", PASMovementId);
                oDq.RunActionQuery();
            }
        }

        public static DataTable GetBerths(int VesselId)
        {
            string strExecution = "usp_GetBerths";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@VesselId", VesselId);
                return oDq.GetTable();
            }
        }

        public static List<VesselStatus> GetListVesselPosition(string vesselStatus, int UserPort)
        {
            string strExecution = "usp_GetVesselPosition";
            List<VesselStatus> lstVessel = new List<VesselStatus>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@VesselStatus", 10, vesselStatus);
                oDq.AddIntegerParam("@UserLoc", UserPort);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    VesselStatus vessel = new VesselStatus(reader);
                    lstVessel.Add(vessel);
                }

                reader.Close();
            }

            return lstVessel;
        }

        public static int PromoteVessel(VesselStatus vessel)
        {
            int statusId = 0;
            string strExecution = "usp_SaveStatus";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@fk_VesselID", vessel.VesselId);

                if (vessel.Activity == "E")
                    oDq.AddDateTimeParam("@StatusDate", vessel.ArrivalDate);
                else if (vessel.Activity == "A")
                    oDq.AddDateTimeParam("@StatusDate", vessel.BerthDate);
                else
                    oDq.AddDateTimeParam("@StatusDate", DateTime.Now);

                oDq.AddDateTimeParam("@BerthDate", vessel.BerthDate);
                oDq.AddVarcharParam("@Activity", 1, vessel.Activity);
                oDq.AddIntegerParam("@BerthID", vessel.BerthId);
                oDq.AddIntegerParam("@UserID", vessel.CreatedBy);


                statusId = Convert.ToInt32(oDq.GetScalar());
                return statusId;
            }
        }

        public static int RevertStatus(int statusId)
        {
            int id = 0;
            string strExecution = "usp_RevertStatus";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@pk_StatusID", statusId);

                id = Convert.ToInt32(oDq.GetScalar());
                return id;
            }
        }

        public static void SaveETCorWTA(int vesselId, DateTime dt, bool isETA)
        {
            string strExecution = "usp_SaveETCorETA";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@vesselId", vesselId);
                oDq.AddBooleanParam("@isETA", isETA);
                oDq.AddDateTimeParam("@dt", dt);

                oDq.RunActionQuery();
            }
        }

        public static DataTable GetPASVesselList()
        {
            string ProcName = "uspGetPASVesselList";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddVarcharParam("@SortExpression", 10, "");
            dquery.AddVarcharParam("@SortDirection", 10, "");
            return dquery.GetTable();
        }

        public static DataTable GetMovementList()
        {
            string ProcName = "uspGetMovementList";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            //dquery.AddVarcharParam("@SortExpression", 10, "");
            //dquery.AddVarcharParam("@SortDirection", 10, "");
            return dquery.GetTable();
        }

        public static VesselMovementEntity GetPASMovement(int MovementId)
        {
            string strExecution = "GetPASMovement";
            VesselMovementEntity o = new VesselMovementEntity();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@pk_PASTranID", MovementId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    o = new VesselMovementEntity(reader);
                }
                reader.Close();
            }
            return o;
        }

        public static DataSet GetPortNameByVesselID(Int64 VesselId, int PASTranID)
      
        {

            string ProcName = "uspGetPortNameByVesselId";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddBigIntegerParam("@VesselId", VesselId);
            dquery.AddBigIntegerParam("@PASTranId", PASTranID);

            return dquery.GetTables();

        }

        public static int SavePAS(VesselMovementEntity o)
        {
            int vesselId = 0;
            string strExecution = "usp_SavePAS";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                if (o.PASTranID > 0)
                    oDq.AddIntegerParam("@PassTranId", o.PASTranID);

                //oDq.AddVarcharParam("@VesselActivity", 2, o.VesselActivity);
                oDq.AddIntegerParam("@VesselID", o.VesselId);
                oDq.AddVarcharParam("@Movement", 1, o.Movement);
                oDq.AddVarcharParam("@MovementType", 1, o.MovementType);
                oDq.AddDateTimeParam("@MovementDate", o.MovementDate);
                oDq.AddIntegerParam("@CreatedBy", o.CreatedBy);
                oDq.AddIntegerParam("@ModifiedBy", o.ModifiedBy);

                vesselId = Convert.ToInt32(oDq.GetScalar());
                return vesselId;
            }
        }

        public static int SaveCargoPAS(CargoDetails o, int type)
        {
            int cargoVesselId = 0;
            string strExecution = "usp_SaveCargoPAS";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CargoVesselID", o.CargoVesselId);
                oDq.AddIntegerParam("@PASTranID", o.PASTranID);
                oDq.AddIntegerParam("@CargoId", o.CargoId);
                oDq.AddDecimalParam("@Quantity", 12, 2, o.Quantity);
                oDq.AddVarcharParam("@ActType", 1, o.ActType);
                oDq.AddIntegerParam("@Type", type);

                cargoVesselId = Convert.ToInt32(oDq.GetScalar());
                return cargoVesselId;
            }
        }
    }
}
