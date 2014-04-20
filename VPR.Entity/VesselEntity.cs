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
        public int BerthId { get; set; }
        public decimal LOA { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime? BerthDate { get; set; }
        public DateTime? ETC { get; set; }
        public string Owner { get; set; }
        public int AgentId { get; set; }
        public int PrevPortId { get; set; }
        public int NextPortId { get; set; }
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

            if (ColumnExists(reader, "BerthDate"))
                if (reader["BerthDate"] != DBNull.Value)
                    this.BerthDate = Convert.ToDateTime(reader["BerthDate"]);

            if (ColumnExists(reader, "ETC"))
                if (reader["ETC"] != DBNull.Value)
                    this.ETC = Convert.ToDateTime(reader["ETC"]);

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
                this.VoyageNo = Convert.ToString(reader["VoyageNo"]);

            if (ColumnExists(reader, "PortName"))
                this.PortName = Convert.ToString(reader["PortName"]);

            if (ColumnExists(reader, "BerthName"))
                this.BerthName = Convert.ToString(reader["BerthName"]);
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
