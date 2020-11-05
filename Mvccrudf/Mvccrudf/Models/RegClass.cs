using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvccrudf.Models
{
    public class RegClass
    {
        public Guid RowNumber { get; set; }
        public string Region { get; set; }
        public string Location { get; set; }
        public string Unit { get; set; }
    }
}