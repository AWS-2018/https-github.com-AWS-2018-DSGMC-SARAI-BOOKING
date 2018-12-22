using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Mvc;


namespace SaraiBooking.ViewModels
{
    public class CaptchaImageResult : ActionResult
    {
        public string GetCaptchaString(int length)
        {
            int intZero = '0';
            int intNine = '9';
            int intA = '0';
            int intZ = '9';
            int intCount = 0;
            int intRandomNumber = 0;
            string strCaptchaString = "";

            Random random = new Random(System.DateTime.Now.Millisecond);

            while (intCount < length)
            {
                intRandomNumber = random.Next(intZero, intZ);
                if (((intRandomNumber >= intZero) && (intRandomNumber <= intNine) || (intRandomNumber >= intA) && (intRandomNumber <= intZ)))
                {
                    strCaptchaString = strCaptchaString + (char)intRandomNumber;
                    intCount = intCount + 1;
                }
            }
            return strCaptchaString;
        }


        public override void ExecuteResult(ControllerContext context)
        {
            //Bitmap bmp = new Bitmap(75, 30);
            Image bmp = Base64ToImage(); // (System.Drawing.Image)Bitmap.FromFile(Server.MapPath("onam.jpg")); // set image
            Graphics g = Graphics.FromImage(bmp);
            //g.Clear(ColorTranslator.FromHtml("#1274C0")); //AB5611
            string randomString = GetCaptchaString(6);
            context.HttpContext.Session["CaptchaString"] = randomString;
            //g.DrawString(randomString, new Font("Courier", 14), new SolidBrush(ColorTranslator.FromHtml("#dedede")), 2, 2);
            g.DrawString(randomString, new Font("Courier", 16), new SolidBrush(ColorTranslator.FromHtml("#FF0000")), 5, 5);
            //int alpha = 128; // range from 0 (transparent) to 255 (opaque)
            //g.DrawString(randomString, new Font("Courier", 14), new SolidBrush(Color.FromArgb(alpha, ColorTranslator.FromHtml("#dedede"))), 2, 2);
            HttpResponseBase response = context.HttpContext.Response;
            //response.ContentType = "image/jpeg";
            response.ContentType = "image/png";
            bmp.Save(response.OutputStream, ImageFormat.Jpeg);
            bmp.Dispose();
        }

        public System.Drawing.Image Base64ToImage()
        {
            byte[] imageBytes = Convert.FromBase64String(HttpContext.Current.Session["Base64String"].ToString());
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            HttpContext.Current.Session["Base64String"] = null;
            return image;
        }
    }
}