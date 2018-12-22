using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Core
{
    public class Common
    {
        public Common()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static SortedDictionary<string, string> SortNameValueCollection(NameValueCollection nvc)
        {
            SortedDictionary<string, string> sortedDict = new SortedDictionary<string, string>();
            foreach (String key in nvc.AllKeys)
                sortedDict.Add(key, nvc[key]);
            return sortedDict;
        }

        public static Crypto.Algorithm GetConfigAlgorithm(string key)
        {
            return GetConfigAlgorithm(key, "");
        }

        /// <summary>
        /// Supported Algorithm: SHA1, SHA256, SHA384, MD5 and SHA512
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Crypto.Algorithm GetConfigAlgorithm(string key, string defaultValue)
        {
            string ConfigValue = "sha512";
            Crypto.Algorithm algorithm = new Crypto.Algorithm();
            if (!string.IsNullOrEmpty(ConfigValue))
            {
                switch (ConfigValue.ToLower())
                {
                    case "sha1":
                        algorithm = Crypto.Algorithm.SHA1;
                        break;
                    case "sha256":
                        algorithm = Crypto.Algorithm.SHA256;
                        break;
                    case "sha384":
                        algorithm = Crypto.Algorithm.SHA384;
                        break;
                    case "sha512":
                        algorithm = Crypto.Algorithm.SHA512;
                        break;
                    case "md5":
                        algorithm = Crypto.Algorithm.MD5;
                        break;
                    default:
                        throw new ArgumentException("Invalid algorithm configured in configuration", "Algorithm");
                }
            }
            else
                throw new ArgumentException("Invalid algorithm configured in configuration", "Algorithm");
            return algorithm;
        }

        /// <summary>
        /// Method used to Check the Row Version
        /// </summary>
        /// <param name="Id">Record Id</param>
        /// <param name="RowVersion">Row version of Record</param>
        /// <param name="TableName">Name of the Table to which the record belogs</param>
        /// <returns></returns>
        public static int MonthsBetween(DateTime FirstDate, DateTime SecondDate)
        {
            return 0;
        }

        public static string GetStudentOrderBy(string orderBy)
        {
            string lstOrderBy = string.Empty;

            if (orderBy == "RollNumber")
            {
                lstOrderBy = "FY_SM.ROLL_NUMBER, M.NAME, M.FATHER_NAME";
            }

            if (orderBy == "AdmissionNumber")
            {
                lstOrderBy = "M.ADMISSION_NUMBER, FY_SM.ROLL_NUMBER, M.NAME, M.FATHER_NAME";
            }

            if (orderBy == "Name")
            {
                lstOrderBy = "M.NAME, M.FATHER_NAME, FY_SM.ROLL_NUMBER, M.ADMISSION_NUMBER";
            }

            return lstOrderBy;
        }
    }
}