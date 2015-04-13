using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPR.BLL;
using VPR.Common;
using VPR.Entity;
using VPR.Utilities;
using VPR.Utilities.ResourceManager;
using System.Data;

namespace VPR.WebApp.MasterModule
{
    public partial class ImportData : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _roleId = 0;
        private int _locId = 0;
        private bool _hasEditAccess = true;
        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            _userId = UserBLL.GetLoggedInUserId();
            //CheckUserAccess();
            //RetrieveParameters();
            //SetAttributes();

            if (!IsPostBack)
            {
                //rblTag.SelectedValue = "1";
                //AllowUntagging(true);
                //PopulateYear();
                //SetDefaultSearchCriteria();
                //PopulateCustomer();
                //PopulateSalesExecutive();
                //LoadShipSoftData();
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            //LoadShipSoftData();
            //upArea.Update();

            if (fuShipSoft.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(fuShipSoft.FileName);
                List<EmailImport> lstEmail = new List<EmailImport>();
                //List<ShipSoftEntity> lstShipSoft = new List<ShipSoftEntity>();

                if (fileExt.ToUpper() == ".TXT")
                {
                    using (StreamReader reader = new StreamReader(fuShipSoft.FileContent, Encoding.Default))
                    {
                        while (!reader.EndOfStream)
                        {
                            string str = reader.ReadLine();

                            string[] abc = str.Split(',');

                            if (abc.Length > 8)
                            {
                                try
                                {
                                    
                                    EmailImport Email = new EmailImport();
                                    Email.CompanyAbbr = Convert.ToString(abc[4]);
                                    Email.Name = Convert.ToString(abc[2]);
                                    Email.EmailID = Convert.ToString(abc[0]);

                                    lstEmail.Add(Email);

                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                    }

                    if (lstEmail.Count > 0)
                    {
                        int dupCount = 0;
                        int rowsAffected = 0;
                        new EmailBLL().SaveBulkEmail(lstEmail, _userId, out rowsAffected, out dupCount);
                        string message = "Total Number of Records Processed: " + Convert.ToString(lstEmail.Count) + "\\r\\n";
                        message += "Total Number of Records Up-loaded: " + rowsAffected.ToString() + "\\r\\n";
                        message += "Total Number of Records Rejected: " + dupCount.ToString();
                        GeneralFunctions.RegisterAlertScript(this, message);
                    }
                }
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, ResourceManager.GetStringWithoutName("ERR00052"));
            }
        }

        
        //private string test(string arg)
        //{
        //    int nInStrLen = 0;
        //    int cNextChar = 0;
        //    string cOutString = "";

        //    if (!string.IsNullOrEmpty(arg))
        //    {
        //        nInStrLen = arg.Length;

        //        for (int index = 1; index < nInStrLen; index++)
        //        {
        //            byte[] str = Encoding.ASCII.GetBytes(arg.Substring(index * 1, 1));
        //            cNextChar = str[0];
        //            cOutString = cOutString + (char)((cNextChar / 2));
        //        }
        //    }

        //    return cOutString;
        //}

        private string Descramble(string cInString)
        {
            int nInStrLen = 0;
            char cNextChar;
            string cOutString = string.Empty;

            if (!string.IsNullOrEmpty(cInString))
            {
                nInStrLen = cInString.Length;

                for (int nCounter = 0; nCounter < nInStrLen; nCounter++)
                {
                    cNextChar = Convert.ToChar(cInString.Substring(nCounter, 1));
                    cOutString = cOutString + Convert.ToString((char)(cNextChar - 96));
                }
            }

            return cInString;
        }

        private string ConvertUnicodeToAscii(string unicodeString)
        {
            // Create two different encodings.
            Encoding ascii = Encoding.ASCII;
            Encoding unicode = Encoding.UTF8;

            // Convert the string into a byte[].
            byte[] unicodeBytes = unicode.GetBytes(unicodeString);

            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

            // Convert the new byte[] into a char[] and then into a string.
            // This is a slightly different approach to converting to illustrate
            // the use of GetCharCount/GetChars.
            char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            string asciiString = new string(asciiChars);

            // Display the strings created before and after the conversion.
            return asciiString;
        }
        
        protected void btnExport_Click(object sender, EventArgs e)
        {
            EmailBLL cls = new EmailBLL();
            var path = Server.MapPath("~/ExpEmail/filename.txt");
            var newFileName = Server.MapPath("~/ExpEmail/EmailExport" + DateTime.Now.ToString("ddMMyyyymmhh") + ".txt");
            File.Copy(path, newFileName, true);
            new EmailBLL().ExportToCSV(newFileName);

            byte[] Content = File.ReadAllBytes(newFileName);
            Response.ContentType = "text/csv";
            Response.AddHeader("content-disposition", "attachment; filename=" + newFileName);
            Response.BufferOutput = true;;
            Response.OutputStream.Write(Content, 0, Content.Length);
            Response.End();
            

        }

        //protected void btnExport_Click(object sender, EventArgs e)
        //{
        //    EmailBLL cls = new EmailBLL();
        //    string strBasePath = @"D:\";
        //    string strFilename = @"filename.csv";
        //    string CSVConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + strBasePath + @"';Extended Properties='text; HDR=Yes;FMT=Delimited';";
        //    using (OleDbConnection CSVConnection = new OleDbConnection(CSVConnectionString))
        //    {
        //        try
        //        {
        //            DataTable dtEmail = new DataTable();
        //            dtEmail = cls.GetAllEmail();

        //            CSVConnection.Open();
        //            string strInsertCommand = @"INSERT INTO " + strFilename + @" (ReceiverNm, ValidemailID, Cat) VALUES (@Receiver, @email, @category)";
        //            OleDbCommand InsertCommanmd = CSVConnection.CreateCommand();
        //            InsertCommanmd.CommandText = strInsertCommand;
                   

        //            foreach (DataRow row in dtEmail.Rows)
        //            {
        //                InsertCommanmd.Parameters.Clear();
        //                InsertCommanmd.Parameters.AddWithValue("@Receiver", row["ReceiverName"].ToString());
        //                InsertCommanmd.Parameters.AddWithValue("@email", row["emailIDActive"].ToString());
        //                InsertCommanmd.Parameters.AddWithValue("@category", row["CompanyAbbr"].ToString());
        //                //Optional Line :Writing to console
        //                //Console.WriteLine("{0}\t{1}\t\t\b{2}", row["customerNumber"].ToString(), row["customerName"].ToString().PadRight(25), row["country"].ToString());

        //                InsertCommanmd.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("CSV Database Err: " + ex.Message);
        //        }
        //        finally
        //        {
        //            if (!CSVConnection.State.Equals(ConnectionState.Closed))
        //            {
        //                CSVConnection.Close();
        //                byte[] Content= File.ReadAllBytes(FilePath)
        //                Response.ContentType = "text/csv";
        //                Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".csv");
        //                Response.BufferOutput = true;;
        //                Response.OutputStream.Write(Content, 0, Content.Length);
        //                Response.End();
        //            }
        //        }
        //    }
        //}

        #endregion
    }
}