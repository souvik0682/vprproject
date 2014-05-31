using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using System.Data;

namespace VPR.Entity
{
    public class WeeklyReportEntity
    {
        public string ActivityName { get; set; }
        public decimal Quantity { get; set; }
        public string PortName { get; set; }
        public string CargoName { get; set; }
        public int Vessels { get; set; }

        public WeeklyReportEntity()
        {

        }

        public WeeklyReportEntity(DataTableReader reader)
        {

            if (ColumnExists(reader, "ActivityName"))
                if (reader["ActivityName"] != DBNull.Value)
                    this.ActivityName = Convert.ToString(reader["ActivityName"]);

            if (ColumnExists(reader, "Quantity"))
                if (reader["Quantity"] != DBNull.Value)
                    this.Quantity = Convert.ToDecimal(reader["Quantity"]);

            if (ColumnExists(reader, "PortName"))
                if (reader["PortName"] != DBNull.Value)
                    this.PortName = Convert.ToString(reader["PortName"]);

            if (ColumnExists(reader, "CargoName"))
                if (reader["CargoName"] != DBNull.Value)
                    this.CargoName = Convert.ToString(reader["CargoName"]);

            if (ColumnExists(reader, "Vessels"))
                if (reader["Vessels"] != DBNull.Value)
                    this.Vessels = Convert.ToInt32(reader["Vessels"]);

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
