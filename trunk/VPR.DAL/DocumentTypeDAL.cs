using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VPR.Common;
using VPR.DAL.DbManager;
using VPR.Entity;

namespace VPR.DAL
{
    public sealed class DocumentDAL
    {
        private DocumentDAL()
        {
        }

        #region Document


        public static int SaveDocument(int DocumentType, string DocumentName, int PortID, string LinkedFileName,int UploadedBy)
        {
            string strExecution = "[dbo].[CreateDocuments]";
            int result = 0;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@PortID", PortID);
                oDq.AddVarcharParam("@DocumentName", 255, DocumentName);
                oDq.AddIntegerParam("@DocumentTypeID", DocumentType);
                oDq.AddVarcharParam("@LinkedFileName", 255, LinkedFileName);
                oDq.AddDateTimeParam("@UploadDate", System.DateTime.Now);
                oDq.AddIntegerParam("@UploadedBy", UploadedBy);
                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Return);
                oDq.RunActionQuery();
                result = Convert.ToInt32(oDq.GetParaValue("@Result"));
            }

            return result;
        }

        public static bool DeleteDocument(int DocumentID)
        {
            string strExecution = "DeleteDocument";
            int result = 0;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@DocumentID", DocumentID);
                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Return);
                oDq.RunActionQuery();
                result = Convert.ToInt32(oDq.GetParaValue("@Result"));
                return result == 0;
            }
            return false;
        }


        public static DataSet GetDocuments(int? DocumentType = null, string DocumentName = null, int? PortID = null, int? DocumentID = null)
        {
            DataSet ds = new DataSet();
            using (DbQuery dq = new DbQuery("GetDocuments"))
            {  
                dq.AddIntegerParam("@DocumentType", DocumentType);
                dq.AddVarcharParam("@DocumentName",255, DocumentName);
                dq.AddIntegerParam("@PortID", PortID);
                dq.AddIntegerParam("@DocumentID", @DocumentID);
                ds = dq.GetTables();
            }
            return ds;
        }
        public static DataSet GetDocumentType(int? DocumentTypeID = null, string DocumentType = null)
        {
            DataSet ds = new DataSet();
            using (DbQuery dq = new DbQuery("GetDocumentType"))
            {
                dq.AddIntegerParam("@DocumentTypeID", DocumentTypeID);
                dq.AddVarcharParam("@DocumentType",255, DocumentType);
                ds = dq.GetTables();
            }
            return ds;
        }
        #endregion

        
    }
}
