using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Common
{
    public class BlockedIPAddress : BusinessObject<BlockedIPAddress>
    {
        public int Id { get; set; }

        public string IPAddress { get; set; }
        public DateTime RecordDate { get; set; }

        public void TrimData()
        {
            if (IPAddress == null)
                IPAddress = "";

            IPAddress = IPAddress.Trim();
        }

        public string CheckValidation()
        {
            string result = "";

            if (IPAddress == "")
                result += "<li>Invalid IP Address</li>";

            if (result != "")
                result = "<ul>" + result + "</ul>";

            return result.ToString();
        }
    }
}
