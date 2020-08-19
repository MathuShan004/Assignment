using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAPI.Models
{
    public class Currency
    {
        public string Country { get; set; }
        public string Type { get; set; }
        public double Rate { get; set; }
    }
}