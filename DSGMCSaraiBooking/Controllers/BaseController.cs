using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Data;
using System.Configuration;
using Facade.Common;

using System.Drawing;


using System.Threading.Tasks;
using SaraiBooking.App_Start;
using ClosedXML.Excel;

namespace SaraiBooking.Controllers
{
    [HandleError]
    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {

            Exception exception = filterContext.Exception;
            //Logging the Exception
            filterContext.ExceptionHandled = true;

            var Result = this.View("Error", new HandleErrorInfo(exception,
                filterContext.RouteData.Values["controller"].ToString(),
                filterContext.RouteData.Values["action"].ToString()));

            filterContext.Result = Result;
        }

     

        public ActionResult Disable(string Token)
        {

            if (Token == null || Token.ToString().Trim().Length <= 0)
                return RedirectToAction("Index");

            int id = Common.GetDataFromEncryptedToken(Token, "ID", 0);
            int pageNumber = Common.GetDataFromEncryptedToken(Token, "PAGE_NUMBER", 0);
            int rowVersion = Common.GetDataFromEncryptedToken(Token, "ROW_VERSION", 0);
            string tableName = Common.GetDataFromEncryptedToken(Token, "TABLE_NAME", "");
            string DatabaseName = Common.GetDataFromEncryptedToken(Token, "DATABASE", "");
            int MenuId = Common.GetDataFromEncryptedToken(Token, "MENU_ID", 0);
            string SortOrder = Common.GetDataFromEncryptedToken(Token, "SORT_ORDER", "");

            Common.CanUserChangeStatus(MenuId);

            new CommonFacade().UpdateRecordStatus(id, rowVersion, false, tableName, DatabaseName);

            return RedirectToAction("Index", new { Token = Common.EncryptData("MENU_ID=" + MenuId + "`PAGE_NUMBER=" + pageNumber + "`SORT_ORDER=" + SortOrder + "`RECORD_STATUS=S") });
        }

        public ActionResult Enable(string Token)
        {
            if (Token == null || Token.ToString().Trim().Length <= 0)
                return RedirectToAction("Index");

            int id = Common.GetDataFromEncryptedToken(Token, "ID", 0);
            int pageNumber = Common.GetDataFromEncryptedToken(Token, "PAGE_NUMBER", 0);
            int rowVersion = Common.GetDataFromEncryptedToken(Token, "ROW_VERSION", 0);
            string tableName = Common.GetDataFromEncryptedToken(Token, "TABLE_NAME", "");
            string DatabaseName = Common.GetDataFromEncryptedToken(Token, "DATABASE", "");
            int MenuId = Common.GetDataFromEncryptedToken(Token, "MENU_ID", 0);
            string SortOrder = Common.GetDataFromEncryptedToken(Token, "SORT_ORDER", "");

            Common.CanUserChangeStatus(MenuId);

            new CommonFacade().UpdateRecordStatus(id, rowVersion, true, tableName, DatabaseName);

            return RedirectToAction("Index", new { Token = Common.EncryptData("MENU_ID=" + MenuId + "`PAGE_NUMBER=" + pageNumber + "`SORT_ORDER=" + SortOrder + "`RECORD_STATUS=S") });
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            DateTime CurrentDate = DateTime.Now;

            base.Initialize(requestContext);

            ViewBag.Title = "Web School Manager";
            ViewBag.Indian = new System.Globalization.CultureInfo("hi-IN");

            if (System.Web.HttpContext.Current.Session[Session["APP_PREFIX"] + "_USER_MASTER_SESSION"] == null)
            {
                Response.Redirect("~/Home/Index");
            }
        }

        public SelectList FeeModeList(object selectedValue = null)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Monthly", Value = "M" });
            items.Add(new SelectListItem { Text = "Yearly", Value = "Y" });

            SelectList feeModeSelectList = new SelectList(items, "Value", "Text", selectedValue);

            return feeModeSelectList;
        }

        public SelectList MarksOrGradesTypeList(object selectedValue = null)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Marks", Value = "M" });
            items.Add(new SelectListItem { Text = "Grades", Value = "G" });

            SelectList marksOrGradesList = new SelectList(items, "Value", "Text", selectedValue);

            return marksOrGradesList;
        }        

     
        public ActionResult ExportDataToExcel(DataTable dtExcelData, string sheetName, string fileNameWithoutExtension, bool totalRequired = true, bool passwordRequired = true)
        {

            using (XLWorkbook wb = new XLWorkbook())
            {
                sheetName = sheetName.Replace(" ", "_");
                fileNameWithoutExtension = fileNameWithoutExtension.Replace(" ", "_");

                dtExcelData.TableName = sheetName;

                IXLWorksheet workSheet = wb.Worksheets.Add(dtExcelData);

                IXLRow row = workSheet.Row(dtExcelData.Rows.Count + 1);

                IXLRange range = workSheet.RangeUsed();
                IXLTable xlTable = range.AsTable();

                if (totalRequired)
                {
                    string colLetterForSNo = GetExcelColumnLetter(xlTable, "#");

                    if (!string.IsNullOrEmpty(colLetterForSNo))
                    {
                        row.Cell(colLetterForSNo).Value = string.Empty;
                    }

                    string colLetterForDaysPresent = GetExcelColumnLetter(xlTable, "Days Present");
                    if (!string.IsNullOrEmpty(colLetterForDaysPresent))
                    {
                        row.Cell(colLetterForDaysPresent).Value = string.Empty;
                    }
                }

                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                if (passwordRequired)
                    wb.Worksheet(sheetName).Protect("123456");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= " + fileNameWithoutExtension + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }

                return null;
            }
        }

    

        [HttpPost]
        public JsonResult GetFormattedNumberToDisplay(decimal inputNumber)
        {
            string formattedNumberToDisplay = String.Format("{0:" + FrameWork.Core.Localizer.CurrentUser.AmountFormat + "}", inputNumber);

            var jsonObject = Json(new { success = true, ResponseText = formattedNumberToDisplay }, JsonRequestBehavior.AllowGet);

            return jsonObject;
        }


        [HttpPost]
        public ActionResult ReadFile(HttpPostedFileBase file)
        {
            string imageDataURL = string.Empty;

            string fileName = file.FileName;
            string ext = fileName.Substring(fileName.LastIndexOf("."));
            string fileNameWithoutExt = fileName.Substring(0, fileName.Length - ext.Length);
            fileName = fileNameWithoutExt + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ext;

            string path = GetTempFolderName();
            string filePath = Server.MapPath(path) + fileName;
            string returnFilePath = path.Substring(1, path.Length - 1) + fileName;

            file.SaveAs(filePath);

            byte[] imageByteData = System.IO.File.ReadAllBytes(filePath);
            string imageBase64Data = Convert.ToBase64String(imageByteData);
            imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);

            //MemoryStream target = new MemoryStream();
            //file.InputStream.CopyTo(target);
            //byte[] data = target.ToArray();
            //imageDataURL = "data:image/jpeg;base64," + Convert.ToBase64String(data);

            //return Content(imageDataURL, "text/html");
            return Content(returnFilePath, "text/html"); ;
        }

        #region Getting Temporary Folder Name
        public string GetTempFolderName()
        {
            //string strTempFolderName = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + @"\";

            string strTempFolderName = "~/Temp/";

            if (Directory.Exists(Server.MapPath(strTempFolderName)))
            {
                return strTempFolderName;
            }
            else
            {
                Directory.CreateDirectory(Server.MapPath(strTempFolderName));
                return strTempFolderName;
            }
        }
        #endregion

        [HttpPost]
        public JsonResult UpdateUserFavoriteMenu(int menuId, string menuName, bool isFavoriteMenu)
        {
            UserMasterFacade facadeUserMaster = new UserMasterFacade();

            bool newStatus;

            if (isFavoriteMenu)
                newStatus = false;
            else
                newStatus = true;

            facadeUserMaster.UpdateUserFavoriteMenu(menuId, newStatus);

            string returnResponse = string.Empty;

            if (isFavoriteMenu == true)
                returnResponse = "\"" + menuName + "\" has been removed from favorite list successfully.";
            else
                returnResponse = "\"" + menuName + "\" has been added in favorite list successfully.";

            var jsonObject = Json(new { success = true, ResponseText = returnResponse }, JsonRequestBehavior.AllowGet);

            return jsonObject;
        }

        public string[] GenerateReportHeaderImage(string instituteCode, string instituteName, string paperSize, string orientation, string companyLogo = "", string instituteAddress = "", string phone1 = "", string phone2 = "", string imageType = "png")
        {
            int headerImageWidth = 0;
            int headerImageHeight = 0;

            int minHeaderHeight = 70;
            int maxHeaderHeight = 70;

            switch (paperSize)
            {
                case "A4":
                    switch (orientation)
                    {
                        case "P":
                            headerImageWidth = 700;
                            headerImageHeight = companyLogo == "" ? minHeaderHeight : maxHeaderHeight;
                            break;
                        case "L":
                            headerImageWidth = 1040;
                            headerImageHeight = companyLogo == "" ? minHeaderHeight : maxHeaderHeight;
                            break;
                    }
                    break;
                case "Legal":
                    switch (orientation)
                    {
                        case "P":
                            headerImageWidth = 700;
                            headerImageHeight = companyLogo == "" ? minHeaderHeight : maxHeaderHeight;
                            break;
                        case "L":
                            headerImageWidth = 1270;
                            headerImageHeight = companyLogo == "" ? minHeaderHeight : maxHeaderHeight;
                            break;
                    }
                    break;
            }

            //Bitmap outputImage = new Bitmap(1169, 150, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Bitmap outputImage = new Bitmap(headerImageWidth, headerImageHeight);

            SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Black);

            System.Drawing.Font reportHeadingFont = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
            System.Drawing.Font instituteNameFont = new System.Drawing.Font("Segoe UI", 11, FontStyle.Bold);
            System.Drawing.Font instituteAddressFont = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
            System.Drawing.Font periodDetailFont = new System.Drawing.Font("Segoe UI", 8, FontStyle.Bold);
            System.Drawing.Font phone1Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Bold);
            System.Drawing.Font phone2Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Bold);

            using (Graphics gr = Graphics.FromImage(outputImage))
            {
                gr.Clear(System.Drawing.Color.White);

                if (companyLogo != "")
                {
                    System.Drawing.Image logo = System.Drawing.Image.FromFile(Server.MapPath("~/images/" + companyLogo));
                    gr.DrawImage(logo, new Rectangle(5, 2, 50, 50), new Rectangle(0, 0, logo.Width, logo.Height), GraphicsUnit.Pixel);
                }

                int instituteNameWidth = Convert.ToInt32(gr.MeasureString(instituteName, instituteNameFont).Width);
                int instituteNameXPoint = Convert.ToInt32(headerImageWidth / 2) - Convert.ToInt32(instituteNameWidth / 2);
                gr.DrawString(instituteName, instituteNameFont, drawBrush, new PointF(instituteNameXPoint, 2));

                if (instituteAddress != "")
                {
                    int instituteAddressWidth = Convert.ToInt32(gr.MeasureString(instituteAddress, instituteAddressFont).Width);
                    int instituteAddressXPoint = Convert.ToInt32(headerImageWidth / 2) - Convert.ToInt32(instituteAddressWidth / 2);
                    int instituteAddressYPoint = Convert.ToInt32(gr.MeasureString(instituteName, instituteNameFont).Height) + 2;
                    gr.DrawString(instituteAddress, instituteAddressFont, drawBrush, new PointF(instituteAddressXPoint, instituteAddressYPoint));
                }

                if (phone1 != "")
                {
                    int phone1Width = Convert.ToInt32(gr.MeasureString(phone1, phone1Font).Width);
                    int phone1XPoint = Convert.ToInt32(headerImageWidth - phone1Width) - 40;
                    int phone1YPoint = 0;

                    if (companyLogo == "")
                    {
                        phone1YPoint = Convert.ToInt32(gr.MeasureString(instituteName, instituteNameFont).Height) + 2;
                        if (instituteAddress != "")
                        {
                            phone1YPoint = phone1YPoint + Convert.ToInt32(gr.MeasureString(instituteAddress, instituteAddressFont).Height) + 2;
                        }
                    }
                    else
                    {
                        phone1YPoint = Convert.ToInt32(headerImageHeight - gr.MeasureString(phone1, phone1Font).Height) - 2;
                    }

                    if (phone2 != "")
                    {
                        int phone2Width = Convert.ToInt32(gr.MeasureString(phone2, phone2Font).Width);
                        phone1XPoint = phone1XPoint - phone2Width;
                        gr.DrawString("Ph: " + phone1 + ", " + phone2, phone2Font, drawBrush, new PointF(phone1XPoint, phone1YPoint));
                    }
                    else
                    {
                        gr.DrawString("Ph: " + phone1, phone1Font, drawBrush, new PointF(phone1XPoint, phone1YPoint));
                    }
                }
            }

            string[] img = { "", "" };
            string imageLocation = string.Empty;

            if (imageType == "png")
            {
                if (paperSize == "A4" && orientation == "P")
                    imageLocation = Server.MapPath(ConfigurationManager.AppSettings["ReportHeaderUrl"] + instituteCode + "-A4_P" + ".png");

                if (paperSize == "A4" && orientation == "L")
                    imageLocation = Server.MapPath(ConfigurationManager.AppSettings["ReportHeaderUrl"] + instituteCode + "-A4_L" + ".png");

                if (paperSize == "Legal" && orientation == "L")
                    imageLocation = Server.MapPath(ConfigurationManager.AppSettings["ReportHeaderUrl"] + instituteCode + "-Legal_L" + ".png");


                outputImage.Save(imageLocation, System.Drawing.Imaging.ImageFormat.Png);
                img[0] = new Uri(imageLocation).AbsoluteUri;
                img[1] = "image/png";
            }
            else
            {
                if (paperSize == "A4" && orientation == "P")
                    imageLocation = Server.MapPath(ConfigurationManager.AppSettings["ReportHeaderUrl"] + instituteCode + "-A4_P" + ".png");

                if (paperSize == "A4" && orientation == "L")
                    imageLocation = Server.MapPath(ConfigurationManager.AppSettings["ReportHeaderUrl"] + instituteCode + "-A4_L" + ".png");

                if (paperSize == "Legal" && orientation == "L")
                    imageLocation = Server.MapPath(ConfigurationManager.AppSettings["ReportHeaderUrl"] + instituteCode + "-Legal_L" + ".png");

                outputImage.Save(imageLocation, System.Drawing.Imaging.ImageFormat.Jpeg);
                img[0] = new Uri(imageLocation).AbsoluteUri;
                img[1] = "image/jpeg";
            }


            return img;
        }

        public bool RemoteFileExistsUsingHTTP(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";

                //Getting the Web Response
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                //Returns TURE if the Status code == 200
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }

        public string GetExcelColumnLetter(IXLTable table, string columnHeader)
        {
            var cell = table.HeadersRow().CellsUsed(c => c.Value.ToString() == columnHeader).FirstOrDefault();

            if (cell != null)
            {
                return cell.WorksheetColumn().ColumnLetter();
            }

            return null;
        }

      
        [HttpPost]
        public JsonResult EncryptData(string stringToEncrypt)
        {
            //string stringToEncrypt = parameter1Name + parameter1Value + parameter2Name + parameter2Value + parameter3Name + parameter3Value;
            string encryptedString = FrameWork.Core.Utility.EncryptData(stringToEncrypt);

            var jsonObject = Json(new { Success = true, Token = encryptedString, ResponseText = "Record modified successfully." }, JsonRequestBehavior.AllowGet);

            return jsonObject;

        }      


        public string DataTableToJSON(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();

                foreach (DataColumn col in dt.Columns)
                    row.Add(col.ColumnName, dr[col]);

                rows.Add(row);
            }

            return serializer.Serialize(rows);
        }

     
        public int GetNewTimeStamp()
        {
            return FrameWork.Core.Localizer.GetTimeStamp;
        }    
     
        protected string ConvertViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }
        
        public DateTime GetServerDateTime()
        {
            return (new CommonFacade()).GetCurrentDateFromServer();
        }

       
        public ActionResult ExportDataSetToExcel(DataSet dsExcelData, string sheetName1, string sheetName2, string fileNameWithoutExtension, bool totalRequired = true, bool passwordRequired = true)
        {

            using (XLWorkbook wb = new XLWorkbook())
            {
                //sheetName1 = sheetName1.Replace(" ", "_");
                //sheetName2 = sheetName2.Replace(" ", "_");

                fileNameWithoutExtension = fileNameWithoutExtension.Replace(" ", "_");

                //if (dsExcelData.Tables[0].Rows.Count > 0)
                //{
                    dsExcelData.Tables[0].TableName = sheetName1;

                    IXLWorksheet workSheet = wb.Worksheets.Add(dsExcelData.Tables[0]);

                    IXLRow row = workSheet.Row(dsExcelData.Tables[0].Rows.Count + 1);

                    IXLRange range = workSheet.RangeUsed();
                    IXLTable xlTable = range.AsTable();

                    if (totalRequired)
                    {
                        string colLetterForSNo = GetExcelColumnLetter(xlTable, "#");

                        if (!string.IsNullOrEmpty(colLetterForSNo))
                        {
                            row.Cell(colLetterForSNo).Value = string.Empty;
                        }

                        string colLetterForDaysPresent = GetExcelColumnLetter(xlTable, "Days Present");
                        if (!string.IsNullOrEmpty(colLetterForDaysPresent))
                        {
                            row.Cell(colLetterForDaysPresent).Value = string.Empty;
                        }
                    }
                   
                    if (passwordRequired)
                        wb.Worksheet(sheetName1).Protect("123456");
                //}

                //if (dsExcelData.Tables[1].Rows.Count > 0)
                //{
                    dsExcelData.Tables[1].TableName = sheetName2;

                    IXLWorksheet workSheet2 = wb.Worksheets.Add(dsExcelData.Tables[1]);

                    IXLRow row2 = workSheet2.Row(dsExcelData.Tables[1].Rows.Count + 1);

                    IXLRange range2 = workSheet2.RangeUsed();
                    IXLTable xlTable2 = range2.AsTable();

                    if (totalRequired)
                    {
                        string colLetterForSNo = GetExcelColumnLetter(xlTable2, "#");

                        if (!string.IsNullOrEmpty(colLetterForSNo))
                        {
                            row2.Cell(colLetterForSNo).Value = string.Empty;
                        }

                        string colLetterForDaysPresent = GetExcelColumnLetter(xlTable2, "Days Present");
                        if (!string.IsNullOrEmpty(colLetterForDaysPresent))
                        {
                            row2.Cell(colLetterForDaysPresent).Value = string.Empty;
                        }
                    }
                    
                    if (passwordRequired)
                        wb.Worksheet(sheetName2).Protect("123456");
                //}

                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= " + fileNameWithoutExtension + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }

                return null;
            }
        }

      
        public ActionResult RemoveQRImage(string fileName)
        {
            string filePath = FrameWork.Core.Localizer.ServerRootPath + FrameWork.Core.Localizer.QRCodeImagePath.Replace("~", "").Replace("/", "\\") + "\\" + fileName + ".bmp";

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            return Json(true);
        }

   
        //#
        public JsonResult StudentOrderByJsonList(object selectedValue = null)
        {
            try
            {
                List<SelectListItem> items = new List<SelectListItem>();

                items.Add(new SelectListItem { Text = "Roll Number", Value = "RollNumber" });
                items.Add(new SelectListItem { Text = "Admission Number", Value = "AdmissionNumber" });
                items.Add(new SelectListItem { Text = "Name", Value = "Name" });

                SelectList lstData = new SelectList(items, "Value", "Text", selectedValue);

                return Json(new { Success = true, ResponseText = "List Retrieved Successfully.", ListData = lstData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, ResponseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public SelectList UserDateFormatList(object selectedValue = null)
        {
            UserDateFormatFacade facade = new UserDateFormatFacade();
            return new SelectList(facade.GetList(), "Id", "Name", selectedValue);
        }

    }
}