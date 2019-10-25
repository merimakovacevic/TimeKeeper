using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Seed
{
    public class ProjectStatuses
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);

                ProjectStatus projectStatus = new ProjectStatus
                {
                    //Id = rawData.ReadInteger(row, 1),
                    Name = rawData.ReadString(row, 2)
                };

                unit.ProjectStatuses.Insert(projectStatus);
                unit.Save();

                Utility.projectStatusesDictionary.Add(oldId, projectStatus.Id);
            }
        }
    }
}

