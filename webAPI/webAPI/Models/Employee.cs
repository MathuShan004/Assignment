using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAPI.Models
{
    public class Employee
    {
        public string Employee_Id { get; set; }
        public string Full_Name { get; set; }
        public string Gender { get; set; }
        public string Date_of_Birth { get; set; }
        public string Joined_Date { get; set; }
        public double Salary { get; set; }
        public string Branch { get; set; }
    }
}