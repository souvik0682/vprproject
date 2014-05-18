using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace VPR.WebApp.DailyReport
{
    public partial class ShowReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string filename = string.Empty;

            if (!ReferenceEquals(Request.QueryString["ReportName"], null))
            {
                filename = Request.QueryString["ReportName"].ToString();
                string fileDate = filename.Substring(15, 10);

                if (DateTime.Now.Date > Convert.ToDateTime(fileDate))
                {
                    lblMessage.Text = "The link has been expired!";
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["EmailReportLink"].ToString() + filename);
                }
                
            }
        }
    }
}