using Facade.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace SaraiBooking.Controllers
{
	public class AboutUsController : BaseController
	{
		//
		// GET: /AboutUs/
		public ActionResult Index()
		{
			ViewBag.PageHeader = "About Us";
			return View();
		}

		public ActionResult About()
		{
			ViewBag.PageHeader = "About Us";

            //string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
            //String ecname = System.Environment.MachineName;
            //ViewBag.ComputerName = computer_name[0].ToString();

            return View();
		}
      
    }
}