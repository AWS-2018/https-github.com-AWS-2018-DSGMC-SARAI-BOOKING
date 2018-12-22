using ClosedXML.Excel;
using LinqToExcel;
using LinqToExcel.Domain;
using LinqToExcel.Query;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace FrameWork.Core
{
    public class MobileNumberData
    {
        public string Name { get; set; }
        public string DeviceId { get; set; }
        public bool IsActive { get; set; }
    }

    public static class Utility
    {
        private static string EncryptionKey = "<->[A/k@a!lW*3b%S0f$t]_[W3bScH0o!M@n@G3r]<->";

        private const long OneKb = 1024;
        private const long OneMb = OneKb * 1024;
        private const long OneGb = OneMb * 1024;
        private const long OneTb = OneGb * 1024;

        public static string ToPrettySize(this int value, int decimalPlaces = 0)
        {
            return ((long)value).ToPrettySize(decimalPlaces);
        }

        public static string ToPrettySize(this long value, int decimalPlaces = 0)
        {
            var asTb = Math.Round((double)value / OneTb, decimalPlaces);
            var asGb = Math.Round((double)value / OneGb, decimalPlaces);
            var asMb = Math.Round((double)value / OneMb, decimalPlaces);
            var asKb = Math.Round((double)value / OneKb, decimalPlaces);
            string chosenValue = asTb > 1 ? string.Format("{0}Tb", asTb)
                : asGb > 1 ? string.Format("{0}Gb", asGb)
                : asMb > 1 ? string.Format("{0}Mb", asMb)
                : asKb > 1 ? string.Format("{0}Kb", asKb)
                : string.Format("{0}B", Math.Round((double)value, decimalPlaces));
            return chosenValue;
        }

        public static decimal ToPrettySize(this int value)
        {
            var asKb = Math.Round((double)value / OneKb);
            return (decimal)asKb;
        }

        public static int ToInteger(this object obj)
        {
            int value = 0;
            if (obj != null)
            {
                value = int.Parse(obj.ToString());
            }
            return value;
        }

        public static decimal ToDecimal(this object obj)
        {
            decimal value = 0;
            if (obj != null)
            {
                value = decimal.Parse(obj.ToString());
            }
            return value;
        }

        public static string GetCRCValue(this object obj)
        {
            string securityKey = EncryptionKey;

            IDictionary<string, object> properties = obj.GetType().GetProperties().Where(p => p.CanRead).ToDictionary(p => p.Name, p => p.GetValue(obj, null));

            foreach (var item in properties)
            {
                if (item.Key != "CRCValue" && item.Value != null && item.Value.ToString() != string.Empty && item.Value.ToString() != "0")
                {
                    securityKey += "|" + item.Value.ToString();
                }
            }

            securityKey = Crypto.GenerateHashString(securityKey, FrameWork.Core.Crypto.Algorithm.SHA512, Crypto.EncodingType.HEX);

            return securityKey;
        }

        public static string EncryptData(string toEncrypt)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(EncryptionKey));
            //Always release the resources and flush data
            // of the Cryptographic service provide. Best Practice

            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string DecryptData(string cipherString)
        {
            byte[] keyArray;
            //get the byte code of the string

            cipherString = cipherString.Replace(' ', '+');

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            //if hashing was used get the hash code with regards to your key
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(EncryptionKey));
            //release any resource held by the MD5CryptoServiceProvider

            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        private static string GetExcelConnectionString(string fileName, string fileExtension)
        {
            string connectionString = "";

            if (fileExtension == ".xlsx")
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0'";

            else
                connectionString = "Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";

            return connectionString;
        }

        public static DataTable ReadFromExcel(string filePath, string fileName, string sheetName, string fileExtension)
        {
            //Create a new DataTable.
            DataTable dt = new DataTable();

            using (XLWorkbook wb = new XLWorkbook(System.IO.Path.Combine(filePath, fileName)))
            {
                IXLWorksheet workSheet = wb.Worksheet(sheetName);

                //Loop through the Worksheet rows.
                bool firstRow = true;

                foreach (IXLRow row in workSheet.Rows())
                {
                    //Use the first row to add columns to DataTable.
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }

                        firstRow = false;
                    }
                    else
                    {
                        //Add rows to DataTable.
                        dt.Rows.Add();

                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = row.Cell(i + 1).Value.ToString();
                        }
                    }
                }
            }

            return dt;
        }

        public static SortedDictionary<string, string> SortNameValueCollection(this NameValueCollection nvc)
        {
            SortedDictionary<string, string> sortedDict = new SortedDictionary<string, string>();
            foreach (String key in nvc.AllKeys)
                sortedDict.Add(key, nvc[key]);
            return sortedDict;
        }

        public static string GenerateSHA512String(this string inputString)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i <= hash.Length - 1; i++)
            {
                stringBuilder.Append(hash[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> collection, string tableName)
        {
            DataTable tbl = ToDataTable(collection);
            tbl.TableName = tableName;
            return tbl;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
        {
            DataTable dt = new DataTable();
            Type t = typeof(T);
            PropertyInfo[] pia = t.GetProperties();
            object temp;
            DataRow dr;

            for (int i = 0; i < pia.Length; i++)
            {
                dt.Columns.Add(pia[i].Name, Nullable.GetUnderlyingType(pia[i].PropertyType) ?? pia[i].PropertyType);
                dt.Columns[i].AllowDBNull = true;
            }

            //Populate the table
            foreach (T item in collection)
            {
                dr = dt.NewRow();
                dr.BeginEdit();

                for (int i = 0; i < pia.Length; i++)
                {
                    temp = pia[i].GetValue(item, null);
                    if (temp == null || (temp.GetType().Name == "Char" && ((char)temp).Equals('\0')))
                    {
                        dr[pia[i].Name] = (object)DBNull.Value;
                    }
                    else
                    {
                        dr[pia[i].Name] = temp;
                    }
                }

                dr.EndEdit();
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static void ExportToExcel<T>(this IEnumerable<T> collection, string fileName)
        {
            DataTable dt = collection.ToDataTable<T>();

            using (XLWorkbook wb = new XLWorkbook())
            {
                dt.TableName = "Sheet1";

                IXLWorksheet workSheet = wb.Worksheets.Add(dt);

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Charset = "";
                System.Web.HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename= " + fileName);

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(System.Web.HttpContext.Current.Response.OutputStream);
                    System.Web.HttpContext.Current.Response.Flush();
                    System.Web.HttpContext.Current.Response.End();
                }
            }
        }

        public static void ExportToExcel(this DataSet ds, string[] sheetName, string fileName)
        {

            for (int i = 0; i < ds.Tables.Count; i++)
            {
                ds.Tables[i].TableName = sheetName[i];
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (DataTable dt in ds.Tables)
                {
                    //Add DataTable as Worksheet.
                    wb.Worksheets.Add(dt);
                }

                //Export the Excel file.
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Charset = "";
                System.Web.HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xlsx", fileName));
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(System.Web.HttpContext.Current.Response.OutputStream);
                    System.Web.HttpContext.Current.Response.Flush();
                    System.Web.HttpContext.Current.Response.End();
                }
            }
        }

        public static string ConvertNumberToWord(decimal value)
        {
            string words = "";

            string amount = value.ToString();

            amount = amount.Replace(", ", "");

            string[] sides = amount.Split('.');

            words = Rupees(Int64.Parse(sides[0]));

            if (sides.Count() > 1)
                if (Int64.Parse(sides[1]) > 0)
                    words += " And " + Rupees(Int64.Parse(sides[1])) + " Paisa";

            words = words + " Only";

            return words.Trim();
        }

        private static string Rupees(Int64 rup)
        {
            string result = "";
            Int64 res;

            if ((rup / 10000000) > 0)
            {
                res = rup / 10000000;
                rup = rup % 10000000;
                result = result + ' ' + RupeesToWords(res) + " Crore";
            }

            if ((rup / 100000) > 0)
            {
                res = rup / 100000;
                rup = rup % 100000;
                result = result + ' ' + RupeesToWords(res) + " Lac";
            }

            if ((rup / 1000) > 0)
            {
                res = rup / 1000;
                rup = rup % 1000;
                result = result + ' ' + RupeesToWords(res) + " Thousand";
            }

            if ((rup / 100) > 0)
            {
                res = rup / 100;
                rup = rup % 100;
                result = result + ' ' + RupeesToWords(res) + " Hundred";
            }

            if ((rup % 10) >= 0)
            {
                res = rup % 100;
                result = result + " " + RupeesToWords(res);
            }

            return result.Trim();
        }

        private static string RupeesToWords(Int64 rup)
        {
            string result = "";

            if ((rup >= 1) && (rup <= 10))
            {
                if ((rup % 10) == 1) result = "One";
                if ((rup % 10) == 2) result = "Two";
                if ((rup % 10) == 3) result = "Three";
                if ((rup % 10) == 4) result = "Four";
                if ((rup % 10) == 5) result = "Five";
                if ((rup % 10) == 6) result = "Six";
                if ((rup % 10) == 7) result = "Seven";
                if ((rup % 10) == 8) result = "Eight";
                if ((rup % 10) == 9) result = "Nine";
                if ((rup % 10) == 0) result = "Ten";
            }

            if (rup > 9 && rup < 20)
            {
                if (rup == 11) result = "Eleven";
                if (rup == 12) result = "Twelve";
                if (rup == 13) result = "Thirteen";
                if (rup == 14) result = "Fourteen";
                if (rup == 15) result = "Fifteen";
                if (rup == 16) result = "Sixteen";
                if (rup == 17) result = "Seventeen";
                if (rup == 18) result = "Eighteen";
                if (rup == 19) result = "Nineteen";
            }

            if (rup >= 20 && (rup / 10) == 2 && (rup % 10) == 0) result = "Twenty";
            if (rup > 20 && (rup / 10) == 3 && (rup % 10) == 0) result = "Thirty";
            if (rup > 20 && (rup / 10) == 4 && (rup % 10) == 0) result = "Forty";
            if (rup > 20 && (rup / 10) == 5 && (rup % 10) == 0) result = "Fifty";
            if (rup > 20 && (rup / 10) == 6 && (rup % 10) == 0) result = "Sixty";
            if (rup > 20 && (rup / 10) == 7 && (rup % 10) == 0) result = "Seventy";
            if (rup > 20 && (rup / 10) == 8 && (rup % 10) == 0) result = "Eighty";
            if (rup > 20 && (rup / 10) == 9 && (rup % 10) == 0) result = "Ninety";

            if (rup > 20 && (rup / 10) == 2 && (rup % 10) != 0)
            {
                if ((rup % 10) == 1) result = "Twenty One";
                if ((rup % 10) == 2) result = "Twenty Two";
                if ((rup % 10) == 3) result = "Twenty Three";
                if ((rup % 10) == 4) result = "Twenty Four";
                if ((rup % 10) == 5) result = "Twenty Five";
                if ((rup % 10) == 6) result = "Twenty Six";
                if ((rup % 10) == 7) result = "Twenty Seven";
                if ((rup % 10) == 8) result = "Twenty Eight";
                if ((rup % 10) == 9) result = "Twenty Nine";
            }

            if (rup > 20 && (rup / 10) == 3 && (rup % 10) != 0)
            {
                if ((rup % 10) == 1) result = "Thirty One";
                if ((rup % 10) == 2) result = "Thirty Two";
                if ((rup % 10) == 3) result = "Thirty Three";
                if ((rup % 10) == 4) result = "Thirty Four";
                if ((rup % 10) == 5) result = "Thirty Five";
                if ((rup % 10) == 6) result = "Thirty Six";
                if ((rup % 10) == 7) result = "Thirty Seven";
                if ((rup % 10) == 8) result = "Thirty Eight";
                if ((rup % 10) == 9) result = "Thirty Nine";
            }

            if (rup > 20 && (rup / 10) == 4 && (rup % 10) != 0)
            {
                if ((rup % 10) == 1) result = "Forty One";
                if ((rup % 10) == 2) result = "Forty Two";
                if ((rup % 10) == 3) result = "Forty Three";
                if ((rup % 10) == 4) result = "Forty Four";
                if ((rup % 10) == 5) result = "Forty Five";
                if ((rup % 10) == 6) result = "Forty Six";
                if ((rup % 10) == 7) result = "Forty Seven";
                if ((rup % 10) == 8) result = "Forty Eight";
                if ((rup % 10) == 9) result = "Forty Nine";
            }

            if (rup > 20 && (rup / 10) == 5 && (rup % 10) != 0)
            {
                if ((rup % 10) == 1) result = "Fifty One";
                if ((rup % 10) == 2) result = "Fifty Two";
                if ((rup % 10) == 3) result = "Fifty Three";
                if ((rup % 10) == 4) result = "Fifty Four";
                if ((rup % 10) == 5) result = "Fifty Five";
                if ((rup % 10) == 6) result = "Fifty Six";
                if ((rup % 10) == 7) result = "Fifty Seven";
                if ((rup % 10) == 8) result = "Fifty Eight";
                if ((rup % 10) == 9) result = "Fifty Nine";
            }

            if (rup > 20 && (rup / 10) == 6 && (rup % 10) != 0)
            {
                if ((rup % 10) == 1) result = "Sixty One";
                if ((rup % 10) == 2) result = "Sixty Two";
                if ((rup % 10) == 3) result = "Sixty Three";
                if ((rup % 10) == 4) result = "Sixty Four";
                if ((rup % 10) == 5) result = "Sixty Five";
                if ((rup % 10) == 6) result = "Sixty Six";
                if ((rup % 10) == 7) result = "Sixty Seven";
                if ((rup % 10) == 8) result = "Sixty Eight";
                if ((rup % 10) == 9) result = "Sixty Nine";
            }

            if (rup > 20 && (rup / 10) == 7 && (rup % 10) != 0)
            {
                if ((rup % 10) == 1) result = "Seventy One";
                if ((rup % 10) == 2) result = "Seventy Two";
                if ((rup % 10) == 3) result = "Seventy Three";
                if ((rup % 10) == 4) result = "Seventy Four";
                if ((rup % 10) == 5) result = "Seventy Five";
                if ((rup % 10) == 6) result = "Seventy Six";
                if ((rup % 10) == 7) result = "Seventy Seven";
                if ((rup % 10) == 8) result = "Seventy Eight";
                if ((rup % 10) == 9) result = "Seventy Nine";
            }

            if (rup > 20 && (rup / 10) == 8 && (rup % 10) != 0)
            {
                if ((rup % 10) == 1) result = "Eighty One";
                if ((rup % 10) == 2) result = "Eighty Two";
                if ((rup % 10) == 3) result = "Eighty Three";
                if ((rup % 10) == 4) result = "Eighty Four";
                if ((rup % 10) == 5) result = "Eighty Five";
                if ((rup % 10) == 6) result = "Eighty Six";
                if ((rup % 10) == 7) result = "Eighty Seven";
                if ((rup % 10) == 8) result = "Eighty Eight";
                if ((rup % 10) == 9) result = "Eighty Nine";
            }

            if (rup > 20 && (rup / 10) == 9 && (rup % 10) != 0)
            {
                if ((rup % 10) == 1) result = "Ninety One";
                if ((rup % 10) == 2) result = "Ninety Two";
                if ((rup % 10) == 3) result = "Ninety Three";
                if ((rup % 10) == 4) result = "Ninety Four";
                if ((rup % 10) == 5) result = "Ninety Five";
                if ((rup % 10) == 6) result = "Ninety Six";
                if ((rup % 10) == 7) result = "Ninety Seven";
                if ((rup % 10) == 8) result = "Ninety Eight";
                if ((rup % 10) == 9) result = "Ninety Nine";
            }

            return result;
        }

        public static bool IsInternetConnected()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsMobileNumberBlockedForOTP(string MobileNumber)
        {
            bool IsBlocked = false;

            var jsonText = File.ReadAllText(Localizer.ServerRootPath + "\\RestrictedMobileNumberListForOTP.txt");
            IList<MobileNumberData> data = JsonConvert.DeserializeObject<IList<MobileNumberData>>(jsonText);

            if (data.Where(m => m.DeviceId == MobileNumber && m.IsActive == true).Count() > 0)
                IsBlocked = true;

            return IsBlocked;
        }
        
        public static bool IsDeviceIdBlockedForOTP(string deviceNumber)
        {
            bool IsBlocked = false;

            var jsonText = File.ReadAllText(Localizer.ServerRootPath + "\\RestrictedDeviceListForOTP.txt");
            IList<MobileNumberData> data = JsonConvert.DeserializeObject<IList<MobileNumberData>>(jsonText);

            if (data.Where(m => m.DeviceId == deviceNumber && m.IsActive == true).Count() > 0)
                IsBlocked = true;

            return IsBlocked;
        }

        public static List<T> ToList<T>(this DataTable dt)
        {
            List<T> data = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }

            return data;
        }

        public static T ToSingle<T>(this DataTable dt)
        {
            T data = Activator.CreateInstance<T>();

            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data = item;
            }

            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)

                        pro.SetValue(obj, dr[column.ColumnName] == DBNull.Value ? null : dr[column.ColumnName], null);
                    else
                        continue;
                }
            }

            return obj;
        }

        /*
        public static void MaintainLogFile(string log)
        {
            StreamWriter sm = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/Access_Log.txt"), true);
            sm.WriteLine(DateTime.Now.ToString("MMM dd, yyyy hh:mm:ss tt") + ":-> " + log);
            sm.WriteLine("");
            sm.Flush();
            sm.Close();
        }
        */

        public static bool IsValidMobileNumber(this string number)
        {
            return Regex.Match(number, @"(?<!\d)\d{10}(?!\d)").Success;
        }

        public static bool IsValidEmailId(this string email)
        {
            return Regex.Match(email, @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$").Success;
        }

        public static bool IsValidPositiveInteger(this string number)
        {
            return Regex.Match(number, @"^(0|[1-9][0-9]*)$").Success;
        }

        public static bool IsValidName(this string name)
        {
            return Regex.Match(name, @"[a-zA-Z0-9]*[^!@%~?:#$%^&*()0']*").Success;
        }

        public static bool IsValidAadhaarNumber(this string aadhaarNumber)
        {
            return Regex.Match(aadhaarNumber, @"^\d{4}\d{4}\d{4}$").Success;
        }

        public static bool IsValidDateTime(this string dateTime)
        {
            string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                                "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss",
                                "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
                                "M/d/yyyy h:mm", "M/d/yyyy h:mm",
                                "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm",
                                "MM/dd/yyyy"
                                };

            DateTime parsedDateTime;
            return DateTime.TryParseExact(dateTime, formats, new CultureInfo("en-US"), DateTimeStyles.None, out parsedDateTime);
        }
    }
}