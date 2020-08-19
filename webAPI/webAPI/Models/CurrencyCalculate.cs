using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace webAPI.Models
{
    public class CurrencyCalculate
    {
        public static List<Employee> ReeadFile()
        {
            try {

                var path = ConfigurationManager.AppSettings["PathEmployee"];
                var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(path)), true))
            {
                csvTable.Load(csvReader);
            }
            List<Employee> employee = new List<Employee>();
            for (int i = 0; i < csvTable.Rows.Count; i++)
            {


                employee.Add(new Employee
                {
                    Employee_Id = csvTable.Rows[i][0].ToString(),
                    Full_Name = csvTable.Rows[i][1].ToString(),
                    Gender = csvTable.Rows[i][2].ToString(),
                    Date_of_Birth = csvTable.Rows[i][3].ToString(),
                    Joined_Date = csvTable.Rows[i][4].ToString(),
                    Salary = Convert.ToDouble(csvTable.Rows[i][5].ToString()),
                    Branch = csvTable.Rows[i][6].ToString()
                });

            }
            return employee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<Currency> ReeadFile1()
        {
            try {


                var path = ConfigurationManager.AppSettings["PathSalary"];
                var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(path)), true))
            {
                csvTable.Load(csvReader);
            }
            List<Currency> currency = new List<Currency>();
            for (int i = 0; i < csvTable.Rows.Count; i++)
            {

                currency.Add(new Currency
                {
                    Country = csvTable.Rows[i][0].ToString(),
                    Type = csvTable.Rows[i][1].ToString(),
                    Rate = Convert.ToDouble(csvTable.Rows[i][2].ToString())

                });

            }
            return currency;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public  List<EmployeeList> CalculateEmpDetails()
        {
            List<Employee> list1 = new List<Employee>();
            List<Currency> list2 = new List<Currency>();
            List<EmployeeList> list = new List<EmployeeList>();

            list1 = ReeadFile();
            list2 = ReeadFile1();

            foreach (Employee e in list1)
            {
                foreach (Currency c in list2)
                {
                    if (e.Branch == c.Country)
                    {
                        EmployeeList employeeList = new EmployeeList();
                        employeeList.Employee_Id = e.Employee_Id;
                        employeeList.Full_Name = e.Full_Name;
                        string[] arr = e.Full_Name.Split(' ');
                        employeeList.FirstName = arr[0];
                        employeeList.LastName = arr[1];
                        employeeList.Gender = e.Gender;
                        employeeList.Date_of_Birth = e.Date_of_Birth;
                        employeeList.Joined_Date = e.Joined_Date;
                        employeeList.Salary = e.Salary;
                        employeeList.Branch = e.Branch;
                        employeeList.Type = c.Type;
                        employeeList.Rate = c.Rate;
                        employeeList.LocalSalary = e.Salary * c.Rate;

                        list.Add(employeeList);

                    }
                }
            }
            int a = list.Count;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Branch == "Sri Lanka")
                {
                    list[i].PATE_Tax_Amount = list[i].LocalSalary < 100000.00 ? 0 : list[i].LocalSalary < 249999.99 ? list[i].LocalSalary * 0.05 : list[i].LocalSalary * 0.1;
                    list[i].Net_Pay_Amount = list[i].LocalSalary - list[i].PATE_Tax_Amount;
                }
                if (list[i].Branch == "India")
                {
                    list[i].PATE_Tax_Amount = list[i].LocalSalary < 100000.00 ? 0 : list[i].LocalSalary < 299999.99 ? list[i].LocalSalary * 0.04 : list[i].LocalSalary * 0.07;
                    list[i].Net_Pay_Amount = list[i].LocalSalary - list[i].PATE_Tax_Amount;
                }
                if (list[i].Branch == "Pakistan")
                {
                    list[i].PATE_Tax_Amount = list[i].LocalSalary < 500000.00 ? list[i].LocalSalary * 0.005 : list[i].LocalSalary * 0.04;
                    list[i].Net_Pay_Amount = list[i].LocalSalary - list[i].PATE_Tax_Amount;
                }
                if (list[i].Branch == "Bangladesh")
                {
                    list[i].PATE_Tax_Amount = 0;
                    list[i].Net_Pay_Amount = list[i].LocalSalary - list[i].PATE_Tax_Amount;

                }



            }
            return list;

        }
    }
}