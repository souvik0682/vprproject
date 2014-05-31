using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using System.Data;

namespace VPR.Entity
{
    public class VesselEntity
    {
        public int VesselId { get; set; }
        public string VesselName { get; set; }
        public int VesselPrefix { get; set; }
        public int BerthId { get; set; }
        public decimal LOA { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? BerthDate { get; set; }
        public DateTime? ETC { get; set; }
        public string Owner { get; set; }
        public int AgentId { get; set; }
        public int PrevPortId { get; set; }
        public int NextPortId { get; set; }
        public int JobID { get; set; }

        public string Remarks { get; set; }
        public int PortId { get; set; }
        public string Activity { get; set; }
        public string VoyageNo { get; set; }
        public string VPRorPAS { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string PortName { get; set; }
        public string BerthName { get; set; }
        public string AgentName { get; set; }
        public string NominatingCo { get; set; }
        public string AppointingCo { get; set; }
        public string Shipper { get; set; }
        public string ActivityName { get; set; }
        public DateTime ETA { get; set; }
        public DateTime? ETB { get; set; }

        public VesselEntity()
        {
            this.VPRorPAS = "V";
        }

        public VesselEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "pk_vesselID"))
                if (reader["pk_vesselID"] != DBNull.Value)
                    this.VesselId = Convert.ToInt32(reader["pk_vesselID"]);

            if (ColumnExists(reader, "VPRorPAS"))
                this.VPRorPAS = Convert.ToString(reader["VPRorPAS"]);

            if (ColumnExists(reader, "Activity"))
                if (reader["Activity"] != DBNull.Value)
                    this.Activity = Convert.ToString(reader["Activity"]);

            if (ColumnExists(reader, "fk_PortID"))
                if (reader["fk_PortID"] != DBNull.Value)
                    this.PortId = Convert.ToInt32(reader["fk_PortID"]);

            if (ColumnExists(reader, "VesselName"))
                this.VesselName = Convert.ToString(reader["VesselName"]);

            if (ColumnExists(reader, "fk_BerthID"))
                if (reader["fk_BerthID"] != DBNull.Value)
                    this.BerthId = Convert.ToInt32(reader["fk_BerthID"]);

            if (ColumnExists(reader, "LOA"))
                if (reader["LOA"] != DBNull.Value)
                    this.LOA = Convert.ToDecimal(reader["LOA"]);

            if (ColumnExists(reader, "ArrivalDate"))
                if (reader["ArrivalDate"] != DBNull.Value)
                    this.ArrivalDate = Convert.ToDateTime(reader["ArrivalDate"]);


            if (ColumnExists(reader, "ETA"))
                if (reader["ETA"] != DBNull.Value)
                    this.ETA = Convert.ToDateTime(reader["ETA"]);

            if (ColumnExists(reader, "BerthDate"))
                if (reader["BerthDate"] != DBNull.Value)
                    this.BerthDate = Convert.ToDateTime(reader["BerthDate"]);

            if (ColumnExists(reader, "ETC"))
                if (reader["ETC"] != DBNull.Value)
                    this.ETC = Convert.ToDateTime(reader["ETC"]);

            if (ColumnExists(reader, "ETB"))
                if (reader["ETB"] != DBNull.Value)
                    this.ETB = Convert.ToDateTime(reader["ETB"]);

            if (ColumnExists(reader, "Owner"))
                this.Owner = Convert.ToString(reader["Owner"]);

            if (ColumnExists(reader, "fk_AgentID"))
                if (reader["fk_AgentID"] != DBNull.Value)
                    this.AgentId = Convert.ToInt32(reader["fk_AgentID"]);

            if (ColumnExists(reader, "fk_PrevPort"))
                if (reader["fk_PrevPort"] != DBNull.Value)
                    this.PrevPortId = Convert.ToInt32(reader["fk_PrevPort"]);

            if (ColumnExists(reader, "fk_NextPort"))
                if (reader["fk_NextPort"] != DBNull.Value)
                    this.NextPortId = Convert.ToInt32(reader["fk_NextPort"]);

            if (ColumnExists(reader, "Remarks"))
                if (reader["Remarks"] != DBNull.Value)
                    this.Remarks = Convert.ToString(reader["Remarks"]);

            if (ColumnExists(reader, "fk_UserAdded"))
                if (reader["fk_UserAdded"] != DBNull.Value)
                    this.CreatedBy = Convert.ToInt32(reader["fk_UserAdded"]);

            if (ColumnExists(reader, "fk_UserLastEdited"))
                if (reader["fk_UserLastEdited"] != DBNull.Value)
                    this.ModifiedBy = Convert.ToInt32(reader["fk_UserLastEdited"]);

            if (ColumnExists(reader, "AddedOn"))
                if (reader["AddedOn"] != DBNull.Value)
                    this.CreatedOn = Convert.ToDateTime(reader["AddedOn"]);

            if (ColumnExists(reader, "EditedOn"))
                if (reader["EditedOn"] != DBNull.Value)
                    this.ModifiedOn = Convert.ToDateTime(reader["EditedOn"]);

            if (ColumnExists(reader, "VoyageNo"))
                if (reader["VoyageNo"] != DBNull.Value)
                    this.VoyageNo = Convert.ToString(reader["VoyageNo"]);

            if (ColumnExists(reader, "PortName"))
                if (reader["PortName"] != DBNull.Value)
                    this.PortName = Convert.ToString(reader["PortName"]);

            if (ColumnExists(reader, "BerthName"))
                if (reader["BerthName"] != DBNull.Value)
                    this.BerthName = Convert.ToString(reader["BerthName"]);

            if (ColumnExists(reader, "AgentName"))
                if (reader["AgentName"] != DBNull.Value)
                    this.AgentName = Convert.ToString(reader["AgentName"]);

            if (ColumnExists(reader, "NominatingCO"))
                if (reader["NominatingCO"] != DBNull.Value)
                    this.NominatingCo = Convert.ToString(reader["NominatingCO"]);

            if (ColumnExists(reader, "AppointingCO"))
                if (reader["AppointingCO"] != DBNull.Value)
                    this.AppointingCo = Convert.ToString(reader["AppointingCO"]);
            
            if (ColumnExists(reader, "Shipper"))
                if (reader["Shipper"] != DBNull.Value)
                    this.Shipper = Convert.ToString(reader["Shipper"]);

            if (ColumnExists(reader, "fk_JobID"))
                if (reader["fk_JobID"] != DBNull.Value)
                    this.JobID = Convert.ToInt32(reader["fk_JobID"]);

            if (ColumnExists(reader, "ActivityName"))
                if (reader["ActivityName"] != DBNull.Value)
                    this.ActivityName = Convert.ToString(reader["ActivityName"]);

            if (ColumnExists(reader, "VesselPrefix"))
                if (reader["VesselPrefix"] != DBNull.Value)
                    this.VesselPrefix = Convert.ToInt32(reader["VesselPrefix"]);
        }

        public bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i) == columnName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
