using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.Common
{
    public class Proof : BusinessObject<Proof>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Proof()
        {
            DatabaseTableName = "PROOF_MASTER";
        }

        public void TrimData()
        {
            if (Name == null)
                Name = "";
                        
            Name = Name.Trim().ToUpper();
        }

        #region Validations

        public string CheckValidation()
        {
            TrimData();

            string result = "";

            if (Id < 0)
                Id = 0;

            if (Name.Length == 0)
                result += "<li>Name is mandatory.</li>";
            
            result = result.TrimEnd().TrimStart().Trim();

            if (result != "")
                result = "<ul>" + result + "</ul>";

            return result.ToString();
        }

        #endregion Validations
    }
}
