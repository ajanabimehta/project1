using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Models
{
    public class Class1
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public List<Class1> cityinfo { get; set; }

        public int id { get; set; }
        public string name { get; set; }
        public string age { get; set; }
        public string city { get; set; }
        public string education { get; set; }
        public string file { get; set; }
        public string gender { get; set; }
        public string filename { get; set; }
        public string email { get; set; }
    }
}