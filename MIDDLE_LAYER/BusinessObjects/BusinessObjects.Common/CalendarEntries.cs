using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Common
{
    public class CalendarEntries
    {
        public DateTime CalendarDate { get; set; }

        public string Remarks { get; set; }

        public string HorizontalAllignment { get; set; }

        public string BadgeColor { get; set; }
        public string ColorCode { get; set; }

        public string TooltipRemarks { get; set; }

        public string LegendText { get; set; }

        public int DayNumber { get; set; }

        public string CalendarClass { get; set; }
    }
}
