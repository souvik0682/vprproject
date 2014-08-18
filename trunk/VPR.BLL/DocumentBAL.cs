using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.DAL;
using VPR.Common;
using VPR.Entity;
using VPR.Utilities.ResourceManager;
using VPR.Utilities;
using VPR.Utilities.Cryptography;
using System.Net.Mail;
using System.Data;
using System.Web.UI.WebControls;


namespace VPR.BLL
{
    public class DocumentBAL
    {
        public static int SaveDocument(int DocumentType, string DocumentName, int PortID, string LinkedFileName, int UploadedBy, string Scope)
        {
            return DocumentDAL.SaveDocument(DocumentType, DocumentName, PortID, LinkedFileName, UploadedBy, Scope);
        }

        public static int EditSaveDocument(int DocumentType, string DocumentName, int PortID, string Scope, int DocID)
        {
            return DocumentDAL.EditSaveDocument(DocumentType, DocumentName, PortID, Scope, DocID);
        }

        public static bool DeleteDocument(int DocumentID)
        {
            return DocumentDAL.DeleteDocument(DocumentID);
        }
        public static DataSet GetDocuments(int? DocumentType = null, string DocumentName = null, int? PortID = null, int? DocumentID = null)
        {
            return DocumentDAL.GetDocuments(DocumentType, DocumentName, PortID, DocumentID);
        }
        public static DataSet GetDocumentType(int? DocumentTypeID = null, string DocumentType = null)
        {
            return DocumentDAL.GetDocumentType(DocumentTypeID, DocumentType);
        }
    }
}
