using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPR.DAL.DBManager
{
    /// <summary>
    /// Acts as a base class for all Data Access Layer classes.
    /// </summary>
    /// <createdby>Amit Kumar Chandra</createdby>
    /// <createdate>27-Feb-2010</createdate>
    public class DataServiceBase
    {
        #region Public Static Properties

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createdate>27-Feb-2010</createdate>
        public static string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the culture.
        /// </summary>
        /// <value>The name of the culture.</value>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createdate>14-Jun-2011</createdate>
        public static string CultureName
        {
            get;
            set;
        }

        #endregion
    }
}
