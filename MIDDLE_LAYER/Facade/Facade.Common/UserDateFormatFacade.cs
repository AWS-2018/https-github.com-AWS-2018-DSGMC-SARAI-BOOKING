using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessObjects.Common;

namespace Facade.Common
{
    public class UserDateFormatFacade
    {
        public List<UserDateFormat> GetList()
        {
            List<UserDateFormat> lstData = new List<UserDateFormat>();

            lstData.Add(new UserDateFormat("MMM dd, yyyy", "MMM dd, yyyy"));
            lstData.Add(new UserDateFormat("dd MMM, yyyy", "dd MMM, yyyy"));
            lstData.Add(new UserDateFormat("MMM/dd/yyyy", "MMM/dd/yyyy"));
            lstData.Add(new UserDateFormat("MMM-dd-yyyy", "MMM-dd-yyyy"));
            //lstData.Add(new UserDateFormat("dd/MM/yyyy", "dd/MM/yyyy"));
            lstData.Add(new UserDateFormat("dd/MMM/yyyy", "dd/MMM/yyyy"));
            //lstData.Add(new UserDateFormat("dd-MM-yyyy", "dd-MM-yyyy"));
            lstData.Add(new UserDateFormat("dd-MMM-yyyy", "dd-MMM-yyyy"));

            return lstData;
        }
    }
}
