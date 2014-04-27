using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using System.Data;


namespace VPR.Entity
{
    public class VesselStatus
    {
        public int StatusId { get; set; }
        public int VesselId { get; set; }
        public string Activity { get; set; }
        public string Vessel { get; set; }
        public string LOA { get; set; }
        public int BerthId { get; set; }
        public DateTime? ETA { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? BerthDate { get; set; }
        public DateTime? ETC { get; set; }
        public DateTime? SailingDate { get; set; }
        public string Cargo { get; set; }

        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public VesselStatus()
        {
        }

        public VesselStatus(DataTableReader reader)
        {
            if (ColumnExists(reader, "pk_StatusID"))
                if (reader["pk_StatusID"] != DBNull.Value)
                    this.StatusId = Convert.ToInt32(reader["pk_StatusID"]);

            if (ColumnExists(reader, "pk_vesselID"))
                if (reader["pk_vesselID"] != DBNull.Value)
                    this.VesselId = Convert.ToInt32(reader["pk_vesselID"]);

            if (ColumnExists(reader, "fk_BerthID"))
                if (reader["fk_BerthID"] != DBNull.Value)
                    this.BerthId = Convert.ToInt32(reader["fk_BerthID"]);

            if (ColumnExists(reader, "Vact"))
                this.Activity = Convert.ToString(reader["Vact"]);

            if (ColumnExists(reader, "VesselName"))
                this.Vessel = Convert.ToString(reader["VesselName"]);

            if (ColumnExists(reader, "LOA"))
                this.LOA = Convert.ToString(reader["LOA"]);

            if (ColumnExists(reader, "ArrivalDate"))
                if (reader["ArrivalDate"] != DBNull.Value)
                    this.ArrivalDate = Convert.ToDateTime(reader["ArrivalDate"]);

            if (ColumnExists(reader, "BerthDate"))
                if (reader["BerthDate"] != DBNull.Value)
                    this.BerthDate = Convert.ToDateTime(reader["BerthDate"]);

            if (ColumnExists(reader, "ETC"))
                if (reader["ETC"] != DBNull.Value)
                    this.ETC = Convert.ToDateTime(reader["ETC"]);

            if (ColumnExists(reader, "ETA"))
                if (reader["ETA"] != DBNull.Value)
                    this.ETA = Convert.ToDateTime(reader["ETA"]);

            if (ColumnExists(reader, "SailingDate"))
                if (reader["SailingDate"] != DBNull.Value)
                    this.SailingDate = Convert.ToDateTime(reader["SailingDate"]);

            if (ColumnExists(reader, "Names"))
                this.Cargo = Convert.ToString(reader["Names"]);

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
