using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using System.Data;

namespace VPR.Entity
{
    [Serializable]
    public class CargoDetails
    {
        public int CargoVesselId { get; set; }
        public int CargoId { get; set; }
        public int VesselId { get; set; }
        public decimal Quantity { get; set; }
        public string ActType { get; set; }
        public int PASTranID { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsNew { get; set; }

        public CargoDetails()
        {
        }

        public CargoDetails(DataTableReader reader)
        {
            if (ColumnExists(reader, "pk_cargoVesselID"))
                if (reader["pk_cargoVesselID"] != DBNull.Value)
                    this.CargoVesselId = Convert.ToInt32(reader["pk_cargoVesselID"]);

            if (ColumnExists(reader, "fk_vesselID"))
                if (reader["fk_vesselID"] != DBNull.Value)
                    this.VesselId = Convert.ToInt32(reader["fk_vesselID"]);

            if (ColumnExists(reader, "fk_PASTranID"))
                if (reader["fk_PASTranID"] != DBNull.Value)
                    this.PASTranID = Convert.ToInt32(reader["fk_PASTranID"]);

            if (ColumnExists(reader, "fk_cargoID"))
                if (reader["fk_cargoID"] != DBNull.Value)
                    this.CargoId = Convert.ToInt32(reader["fk_cargoID"]);

            if (ColumnExists(reader, "quantity"))
                if (reader["quantity"] != DBNull.Value)
                    this.Quantity = Convert.ToDecimal(reader["quantity"]);

            if (ColumnExists(reader, "ActType"))
                if (reader["ActType"] != DBNull.Value)
                    this.ActType = Convert.ToString(reader["ActType"]);

            this.IsDeleted = false;
            this.IsNew = false;
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
