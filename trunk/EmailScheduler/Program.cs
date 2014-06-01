using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net;
using System.IO;

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
            string dbConnectionString = string.Empty;
            string reportLink = string.Empty;
            string attachmentFileName = string.Empty;

            try
            {
                dbConnectionString = System.Configuration.ConfigurationSettings.AppSettings["connString"];
                reportLink = System.Configuration.ConfigurationSettings.AppSettings["reportLink"];
                attachmentFileName = GetReport();
            }
            catch (Exception e)
            {
                Console.WriteLine("APP Error:" + e.InnerException.ToString());
                Console.WriteLine(attachmentFileName);
                Console.ReadLine();
            }

            try
            {
                SqlConnection sqlConnection = new SqlConnection(dbConnectionString);
                SqlCommand command = new SqlCommand("usp_SendDailyEmail", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ReportLink", SqlDbType.VarChar).Value = reportLink;
                command.Parameters.Add("@AttachmentFile", SqlDbType.VarChar).Value = attachmentFileName;
                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error:" + ex.InnerException.ToString());
                Console.WriteLine(attachmentFileName);
                Console.ReadLine();
            }
        }

        private static string GetReport()
        {
            string reportLink = ConfigurationSettings.AppSettings["reportGeneratorLink"];
            string responseFromServer = string.Empty;
            string line;

            try
            {
                WebRequest request = WebRequest.Create(reportLink);
                request.Credentials = CredentialCache.DefaultCredentials;
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("lblResponse"))
                        responseFromServer = line;
                }

                responseFromServer = responseFromServer.Split('>')[1].Split('<')[0];

                reader.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
            }

            return responseFromServer;
        }
    }
}
