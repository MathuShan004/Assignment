using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAPI.Models
{
    public class EmployeeList
    {
        public string Employee_Id { get; set; }
        public string Full_Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Date_of_Birth { get; set; }
        public string Joined_Date { get; set; }
        public double Salary { get; set; }
        public string Branch { get; set; }
        public string Type { get; set; }
        public double Rate { get; set; }
        public double LocalSalary { get; set; }
        public double PATE_Tax_Amount { get; set; }
        public double Net_Pay_Amount { get; set; }
    }
}