using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL
{
    public static class EntityValidator
    {
        public static bool Validate<T>(this T entity)
        {
            if (typeof(T) == typeof(JobDetail)) Validate(entity as JobDetail);
            return true;
        }
        public static bool Validate(this JobDetail task)
        {
            string exceptionText = "";
            if (task.Hours < 0)
            {
                exceptionText += "Number of hours for a task can't be less than 0. ";
            }

            if (task.Hours > 12)
            {
                exceptionText += "Number of hours for a task can't be greater than 12. ";
            }
            else if (task.Day.TotalHours + task.Hours > 12)
            {
                exceptionText += "Total number of recorded hours in a day can't be creater than 12. ";
            }

            if (task.Description == null)
            {
                throw new Exception($"Unable to save task. {exceptionText}");
            }
            else if(task.Description == "")
            {
                exceptionText += "Please enter a task description. ";
            }

            if(task.Project == null || task.Day == null)
            {
                throw new Exception($"Unable to save task. {exceptionText}");
            }

            if (exceptionText != "")
            {
                throw new Exception($"Unable to save task. {exceptionText}");
            }

            return true;
        }

    }
}
