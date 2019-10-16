using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Seed
{
    public static class Utility
    {
        public static Dictionary<int, int> employeesDictionary = new Dictionary<int, int>();
        public static Dictionary<int, int> customersDictionary = new Dictionary<int, int>();
        public static Dictionary<int, int> calendarDictionary = new Dictionary<int, int>();
        public static Dictionary<int, int> projectsDictionary = new Dictionary<int, int>();
        public static Dictionary<string, int> teamsDictionary = new Dictionary<string, int>();
        public static Dictionary<string, int> rolesDictionary = new Dictionary<string, int>();
        

        public static string ReadString(this ExcelWorksheet sht, int row, int col) => sht.Cells[row, col].Value.ToString().Trim();

        public static int ReadInteger(this ExcelWorksheet sht, int row, int col) => int.Parse(sht.ReadString(row, col));

        public static DateTime ReadDate(this ExcelWorksheet sht, int row, int col) => DateTime.Parse(sht.ReadString(row, col));

        public static bool ReadBool(this ExcelWorksheet sht, int row, int col) => sht.ReadString(row, col) == "-1";

        public static decimal ReadDecimal(this ExcelWorksheet sht, int row, int col) => decimal.Parse(sht.ReadString(row, col));
    }
}
