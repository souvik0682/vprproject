using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VPR.Entity
{
    [Serializable]
    public class VesselPosition
    {
        public string VesselName { get; set; }
        public string BerthName { get; set; }
        public string Names { get; set; }
        public string ArrivalDate { get; set; }
        public string BerthDate { get; set; }
        public string ETC { get; set; }
        public string Remarks { get; set; } 
        public string PortName { get; set; }
        public string ActivityStatus { get; set; }
        public Int32 PortID { get; set; }
        public string Activity { get; set; }
        public string ActivityName { get; set; }
        public string Owner { get; set; }
        public string PrevPort { get; set; }
        public string NextPort { get; set; }
        public string AgentName { get; set; }
        public decimal LOA { get; set; }
        public string ETA { get; set; }

        public VesselPosition(DataTableReader reader)
        {
            this.VesselName = Convert.ToString(reader["VesselName"]);
            this.BerthName = Convert.ToString(reader["BerthName"]);
            this.Names = Convert.ToString(reader["CargoPlanned"]);
            
            if (ColumnExists(reader, "ArrivalDate"))
                if (reader["ArrivalDate"] != DBNull.Value)
                    this.ArrivalDate = Convert.ToString(reader["ArrivalDate"]);

            this.BerthDate = Convert.ToString(reader["BerthDate"]);
            if (ColumnExists(reader, "ETA"))
                if (reader["ETA"] != DBNull.Value)
                    this.ETA = Convert.ToString(reader["ETA"]);
            if (ColumnExists(reader, "ETC"))
                if (reader["ETC"] != DBNull.Value)
                    this.ETC = Convert.ToString(reader["ETC"]);
            if (ColumnExists(reader, "Remarks"))
                if (reader["Remarks"] != DBNull.Value)
                    this.Remarks = Convert.ToString(reader["Remarks"]);
            if (ColumnExists(reader, "PortName"))
                if (reader["PortName"] != DBNull.Value)
                    this.PortName = Convert.ToString(reader["PortName"]);
            //if (ColumnExists(reader, "ActivityStatus"))
            //    if (reader["ActivityStatus"] != DBNull.Value)
            this.ActivityStatus = Convert.ToString(reader["ActivityStatus"]);
            if (ColumnExists(reader, "ActivityName"))
                if (reader["ActivityName"] != DBNull.Value)
                    this.ActivityName = Convert.ToString(reader["ActivityName"]);

            if (ColumnExists(reader, "Activity"))
                if (reader["Activity"] != DBNull.Value)
                    this.Activity = Convert.ToString(reader["Activity"]);

            if (ColumnExists(reader, "fk_PortID"))
                if (reader["fk_PortID"] != DBNull.Value)
                    this.PortID = Convert.ToInt32(reader["fk_PortID"]);

            if (ColumnExists(reader, "Owner"))
                if (reader["Owner"] != DBNull.Value)
                    this.Owner = Convert.ToString(reader["Owner"]);

            if (ColumnExists(reader, "PrevPort"))
                if (reader["PrevPort"] != DBNull.Value)
                    this.PrevPort = Convert.ToString(reader["PrevPort"]);

            if (ColumnExists(reader, "NextPort"))
                if (reader["NextPort"] != DBNull.Value)
                    this.NextPort = Convert.ToString(reader["NextPort"]);

            if (ColumnExists(reader, "AgentName"))
                if (reader["AgentName"] != DBNull.Value)
                    this.AgentName = Convert.ToString(reader["AgentName"]);

            if (ColumnExists(reader, "LOA"))
                if (reader["LOA"] != DBNull.Value)
                    this.LOA = Convert.ToDecimal(reader["LOA"]);

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
