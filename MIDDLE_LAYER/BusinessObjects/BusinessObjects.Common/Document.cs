using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.Common
{
    public class Document : BusinessObject<Document>
    {
        public int Id { get; set; }
        public string FullDocumentName { get; set; }
        public string DocumentName { get; set; }
        public string DocumentExtension { get; set; }

        public Document()
        {
            DatabaseTableName = "DOCUMENT_MASTER";
        }

        public void TrimData()
        {
            if (FullDocumentName == null)
                FullDocumentName = "";

            if (DocumentName == null)
                DocumentName = "";

            if (DocumentExtension == null)
                DocumentExtension = "";

            if (Remarks == null)
                Remarks = "";

            FullDocumentName = FullDocumentName.Trim();
            DocumentName = DocumentName.Trim();
            DocumentExtension = DocumentExtension.Trim();
            Remarks = Remarks.Trim();
        }

        #region Validations

        public string CheckValidation()
        {
            TrimData();

            string result = "";

            if (RowVersion <= 0 && Id > 0)
                result += "<li>Invalid Row Version</li>";

            if (Id < 0)
                Id = 0;

            if (FullDocumentName.Length == 0)
                result += "<li>Full Document Name is mandatory.</li>";

            if (DocumentName.Length == 0)
                result += "<li>Document Name is mandatory.</li>";
            
            if (DocumentExtension.Length == 0)
                result += "<li>Document Extension is mandatory.</li>";

            result = result.TrimEnd().TrimStart().Trim();

            if (result != "")
                result = "<ul>" + result + "</ul>";

            return result.ToString();
        }

        #endregion Validations
    }
}
