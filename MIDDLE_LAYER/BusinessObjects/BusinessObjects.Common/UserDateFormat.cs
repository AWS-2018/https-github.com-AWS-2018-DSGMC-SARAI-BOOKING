using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Common
{
    public class UserDateFormat
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public UserDateFormat(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
