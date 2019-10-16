﻿using OfficeOpenXml;
using System;
using System.IO;
using TimeKeeper.DAL;

namespace TimeKeeper.Seed
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo file = new FileInfo(@"C:\Users\merimako\Documents\Projects\Library Final\Library Final\Library.xlsx");
            using (ExcelPackage package = new ExcelPackage(file))
            {
                using (UnitOfWork unit = new UnitOfWork())

                {

                    unit.Context.Database.EnsureDeleted();
                    unit.Context.Database.EnsureCreated();

                    Teams.Collect(package.Workbook.Worksheets["Teams"], unit);

                }
            }
        }
    }
}
