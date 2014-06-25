using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.DAL;
using VPR.Common;
using VPR.Entity;
using VPR.Utilities;
using System.Web;
using VPR.Utilities.ResourceManager;
using VPR.Utilities.Cryptography;
using System.Data;

namespace VPR.BLL
{
    public class TransactionBLL
    {
        public DataTable GetAllCargo()
        {
            return TransactionDAL.GetAllCargo();
        }

        public DataTable GetAllBerth()
        {
            return TransactionDAL.GetAllBerth();
        }

        public DataTable GetAllAgent()
        {
            return TransactionDAL.GetAllAgent();
        }

        public DataTable GetPortWithTransaction()
        {
            return TransactionDAL.GetPortWithTransaction();
        }

        public DataTable GetAllVesselPrefix()
        {
            return TransactionDAL.GetAllVesselPrefix();
        }

        public DataTable GetAllJob()
        {
            return TransactionDAL.GetAllJob();
        }

        public int GetPortId(string PortCode)
        {
            return TransactionDAL.GetPortId(PortCode);
        }

        public string GetPortNameById(Int64 PortId)
        {
            return TransactionDAL.GetPortNameById(PortId);
        }

        public List<CargoDetails> GetListOfCargo(int VesselId)
        {
            return TransactionDAL.GetListOfCargo(VesselId);
        }

        public VesselEntity GetVessel(int VesselId)
        {
            return TransactionDAL.GetVessel(VesselId);
        }

        public VesselEntity GetPASVessel(int VesselId)
        {
            return TransactionDAL.GetPASVessel(VesselId);
        }

        public void SaveVesselCargo(VesselEntity oVessel, List<CargoDetails> oList)
        {
            int VesselId = 0;
            VesselId = TransactionDAL.SaveVessel(oVessel);

            SaveCargo(VesselId, oList);
        }

        public void SaveVesselPASCargo(VesselEntity oVessel, List<CargoDetails> oList)
        {
            int VesselId = 0;
            VesselId = TransactionDAL.SavePASVessel(oVessel);

            SaveCargo(VesselId, oList);
        }

        private void SaveCargo(int VesselId, List<CargoDetails> oList)
        {
            foreach (CargoDetails c in oList)
            {
                int type = 0;

                if (c.IsNew)
                    type = 1;
                else if (c.IsDeleted)
                    type = 2;
                else
                    type = 3;
                
                c.VesselId = VesselId;

                TransactionDAL.SaveCargo(c, type);
            }
        }

        public List<VesselEntity> GetVessles(SearchCriteria searchCriteria)
        {
            return TransactionDAL.GetVessles(searchCriteria);
        }

        public List<VesselEntity> GetPASVessels(SearchCriteria searchCriteria)
        {
            return TransactionDAL.GetPASVessels(searchCriteria);
        }

        public List<VesselMovementEntity> GetPASVesselMovement(SearchCriteria searchCriteria)
        {
            return TransactionDAL.GetPASVesselMovement(searchCriteria);
        }

        public void DeleteVessel(int vesselId)
        {
            TransactionDAL.DeleteVessel(vesselId);
        }

        public void DeletePASMovement(int PASMovementId)
        {
            TransactionDAL.DeletePASMovement(PASMovementId);
        }

        public DataTable GetBerths(int VesselId)
        {
            return TransactionDAL.GetBerths(VesselId);
        }

        public List<VesselStatus> GetListVesselPosition(string vesselStatus)
        {
            return TransactionDAL.GetListVesselPosition(vesselStatus);
        }

        public void PromoteVessels(List<VesselStatus> lstVessel)
        {
            foreach (VesselStatus v in lstVessel)
            {
                int statusId = TransactionDAL.PromoteVessel(v);
            }
        }

        public void RevertVessels(List<int> lstStatus)
        {
            foreach (int s in lstStatus)
            {
                int statusId = TransactionDAL.RevertStatus(s);
            }
        }

        public void SaveETCorWTA(int vesselId, DateTime dt, bool isETA)
        {
            TransactionDAL.SaveETCorWTA(vesselId, dt, isETA);
        }

        public DataTable GetPASVesselList()
        {
            return TransactionDAL.GetPASVesselList();
        }

        public DataTable GetMovementList()
        {
            return TransactionDAL.GetMovementList();
        }
        public VesselMovementEntity GetPASMovement(int MovementId)
        {
            return TransactionDAL.GetPASMovement(MovementId);
        }

        public DataSet GetPortNameByVesselID(Int64 VesselId, int PASTranID)
        {
            return TransactionDAL.GetPortNameByVesselID(VesselId, PASTranID);
        }
        //public List<PASEntity> GetPAS(SearchCriteria searchCriteria)
        //{
        //    return TransactionDAL.GetVessles(searchCriteria);
        //}

        public void SavePAS(VesselMovementEntity oVessel, List<CargoDetails> oList)
        {
            int PASTranId = 0;
            PASTranId = TransactionDAL.SavePAS(oVessel);
            if (oVessel.Movement.ToInt() != 1 && oVessel.Movement.ToInt() != 2)
                SaveCargoPAS(PASTranId, oList);
        }

        private void SaveCargoPAS(int PASTranId, List<CargoDetails> oList)
        {

            foreach (CargoDetails c in oList)
            {
                int type = 0;

                if (c.IsNew)
                    type = 1;
                else if (c.IsDeleted)
                    type = 2;
                else
                    type = 3;

                c.PASTranID = PASTranId;

                TransactionDAL.SaveCargoPAS(c, type);
            }
        }
    }
}
