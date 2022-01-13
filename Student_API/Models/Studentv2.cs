using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student_API.Models
{
    public class Studentv2
    {
        public int ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public String GMail { get; set; }
    }
}