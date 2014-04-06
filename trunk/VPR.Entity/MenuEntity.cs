using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VPR.Common;

namespace VPR.Entity
{
    public class MenuEntity : IMenu
    {
        #region IMenu Members

        public int MenuID
        {
            get;
            set;
        }

        public int MainID
        {
            get;
            set;
        }

        public int SubID
        {
            get;
            set;
        }

        public int SubSubID
        {
            get;
            set;
        }

        public string MenuName
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        #endregion
    }
}
