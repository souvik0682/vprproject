using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using System.Data;
using System.Xml.Serialization;

namespace VPR.Entity
{
    [Serializable]
    public class EmailImport : IEmailImport
    {
        #region EmailImport Members

        
        public string Name
        {
            get;
            set;
        }

        public string EmailID
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public EmailImport()
        {

        }

        public EmailImport(DataTableReader reader)
        {
            
            this.Name = Convert.ToString(reader["Name"]);
            this.EmailID = Convert.ToString(reader["EmailID"]);
            
        }

        #endregion
    }
}
