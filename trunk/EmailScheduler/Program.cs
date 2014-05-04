using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace EmailScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            SendEmail();
        }

        private static void SendEmail()
        {
            try
            {
                string dbConnectionString = ConfigurationSettings.AppSettings["connString"];
                string reportFileName = string.Empty;
                string attachmentFileName = string.Empty;

                SqlConnection sqlConnection = new SqlConnection(dbConnectionString);
                SqlCommand command = new SqlCommand("usp_SendDailyEmail", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ReportLink", SqlDbType.VarChar).Value = reportFileName;
                command.Parameters.Add("@AttachmentFile", SqlDbType.DateTime).Value = attachmentFileName;
                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
        }
    }
}
