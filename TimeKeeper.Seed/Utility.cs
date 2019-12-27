using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TimeKeeper.DAL.Utilities;
using TimeKeeper.Domain.Entities;
using TimeKeeper.Utility.Factory;

namespace TimeKeeper.Seed
{
    public static class Utility
    {
        //the following dictionaries are commented, due to the fact that the won't be needed in the seed - the original Id numbers will be taken from the legacy database
        public static Dictionary<int, int> employeesDictionary = new Dictionary<int, int>();
        public static Dictionary<int, int> customersDictionary = new Dictionary<int, int>();
        public static Dictionary<int, int> calendarDictionary = new Dictionary<int, int>();
        public static Dictionary<int, int> projectsDictionary = new Dictionary<int, int>();

        public static Dictionary<string, int> teamsDictionary = new Dictionary<string, int>();
        public static Dictionary<string, int> rolesDictionary = new Dictionary<string, int>();
        //public static Dictionary<int, int> membersDictionary = new Dictionary<int, int>(); //original table doesn't have an Id number
        //public static Dictionary<int, int> tasksDictionary = new Dictionary<int, int>(); ////original table doesn't have an Id number

        //Status Dictionaries
        public static Dictionary<string, int> employeePositionsDictionary = new Dictionary<string, int>();
        public static Dictionary<int, int> employmentStatusesDictionary = new Dictionary<int, int>();
        public static Dictionary<int, int> dayTypesDictionary = new Dictionary<int, int>(); //this dictionary is not necessary, because the day types are in orderly fashion in the database, starting from 1
        public static Dictionary<int, int> customerStatusesDictionary = new Dictionary<int, int>();
        public static Dictionary<int, int> projectStatusesDictionary = new Dictionary<int, int>();//this dictionary is not necessary, because the day types are in orderly fashion in the database, starting from 1
        public static Dictionary<int, int> pricingStatusesDictionary = new Dictionary<int, int>();

        public static string ReadString(this ExcelWorksheet sht, int row, int col) => sht.Cells[row, col].Value.ToString().Trim();

        public static int ReadInteger(this ExcelWorksheet sht, int row, int col) => int.Parse(sht.ReadString(row, col));

        public static DateTime ReadDate(this ExcelWorksheet sht, int row, int col)
        {
            var data = sht.Cells[row, col].Value;
            if (data == null) return DateTime.MinValue;
            return DateTime.Parse(data.ToString());
        }
        public static DateTime ReadDateValue(this ExcelWorksheet sht, int row, int col)
        {
            var data = sht.Cells[row, col].Value;
            if (data == null) return DateTime.MinValue;
            return DateTime.FromOADate(double.Parse(data.ToString()));
        }

        public static bool ReadBool(this ExcelWorksheet sht, int row, int col) => sht.ReadString(row, col) == "-1";

        public static decimal ReadDecimal(this ExcelWorksheet sht, int row, int col) => decimal.Parse(sht.ReadString(row, col));

        public static string SelectCity(this ExcelWorksheet sht, int row, int col)
        {
            string addr = sht.ReadString(row, col);
            string[] words = addr.Split(", ");
            return words[0];
        }
        public static string SelectCountry(this ExcelWorksheet sht, int row, int col)
        {
            string addr = sht.ReadString(row, col);
            string[] words = addr.Split(", ");
            return words[1];
        }

        public static string SelectRole(this Employee employee)
        {
            if (employee.Position.Id == 1) return "admin";
            if (employee.Position.Id == 2 || employee.Position.Id == 3 || employee.Position.Id == 4) return "lead";
            return "user";
        }

        public static User CreateUserAndRole(this Employee employee)
        {
            User user = new User
            {
                Id = employee.Id,
                Name = employee.FullName,
                Username = employee.MakeUsername(),
                Password = "$ch00l",
                Role = employee.SelectRole()
            };

            return user;
        }
    }
}
