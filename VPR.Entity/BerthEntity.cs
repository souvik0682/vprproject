using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using System.Data;

namespace VPR.Entity
{
    public class BerthEntity
    {
        public string PortName { get; set; }
        public string BerthName { get; set; }
        public int BerthId { get; set; }
        public int PortId { get; set; }

        public BerthEntity()
        {

        }

        public BerthEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "pk_BerthID"))
                if (reader["pk_BerthID"] != DBNull.Value)
                    this.BerthId = Convert.ToInt32(reader["pk_BerthID"]);

            if (ColumnExists(reader, "PortName"))
                this.PortName = Convert.ToString(reader["PortName"]);

            if (ColumnExists(reader, "BerthName"))
                this.BerthName = Convert.ToString(reader["BerthName"]);

            if (ColumnExists(reader, "fk_PortID"))
                if (reader["fk_PortID"] != DBNull.Value)
                    this.PortId = Convert.ToInt32(reader["fk_PortID"]);
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
