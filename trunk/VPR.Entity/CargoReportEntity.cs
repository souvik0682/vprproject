using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using System.Data;

namespace VPR.Entity
{
    public class CargoReportEntity
    {
        public int CargoVesselId { get; set; }
        public string ActivityName { get; set; }
        public string VesselName { get; set; }
        public decimal Quantity { get; set; }
        public string PortName { get; set; }
        public string PrevPort { get; set; }
        public string NextPort { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime BerthDate { get; set; }
        public DateTime SailingDate { get; set; }
        public string Owner { get; set; }
        public string CargoName { get; set; }
        public string GroupName { get; set; }
        //public bool IsDeleted { get; set; }
        //public bool IsNew { get; set; }

        public CargoReportEntity()
        {

        }

        public CargoReportEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "pk_cargoVesselID"))
                if (reader["pk_cargoVesselID"] != DBNull.Value)
                    this.CargoVesselId = Convert.ToInt32(reader["pk_cargoVesselID"]);

            if (ColumnExists(reader, "VesselName"))
                if (reader["VesselName"] != DBNull.Value)
                    this.VesselName = Convert.ToString(reader["VesselName"]);

            if (ColumnExists(reader, "ActivityName"))
                if (reader["ActivityName"] != DBNull.Value)
                    this.ActivityName = Convert.ToString(reader["ActivityName"]);

            if (ColumnExists(reader, "Quantity"))
                if (reader["Quantity"] != DBNull.Value)
                    this.Quantity = Convert.ToDecimal(reader["Quantity"]);

            if (ColumnExists(reader, "PortName"))
                if (reader["PortName"] != DBNull.Value)
                    this.PortName = Convert.ToString(reader["PortName"]);

            if (ColumnExists(reader, "Prevport"))
                if (reader["Prevport"] != DBNull.Value)
                    this.PrevPort = Convert.ToString(reader["Prevport"]);

            if (ColumnExists(reader, "NextPort"))
                if (reader["NextPort"] != DBNull.Value)
                    this.NextPort = Convert.ToString(reader["NextPort"]);

            if (ColumnExists(reader, "ArrivalDate"))
                if (reader["ArrivalDate"] != DBNull.Value)
                    this.ArrivalDate = Convert.ToDateTime(reader["ArrivalDate"]);

            if (ColumnExists(reader, "BerthDate"))
                if (reader["BerthDate"] != DBNull.Value)
                    this.BerthDate = Convert.ToDateTime(reader["BerthDate"]);

            if (ColumnExists(reader, "SailingDate"))
                if (reader["SailingDate"] != DBNull.Value)
                    this.SailingDate = Convert.ToDateTime(reader["SailingDate"]);

            if (ColumnExists(reader, "Owner"))
                if (reader["Owner"] != DBNull.Value)
                    this.Owner = Convert.ToString(reader["Owner"]);

            if (ColumnExists(reader, "CargoName"))
                if (reader["CargoName"] != DBNull.Value)
                    this.CargoName = Convert.ToString(reader["CargoName"]);

            if (ColumnExists(reader, "GroupName"))
                if (reader["GroupName"] != DBNull.Value)
                    this.GroupName = Convert.ToString(reader["GroupName"]);

            //this.IsDeleted = false;
            //this.IsNew = false;
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
