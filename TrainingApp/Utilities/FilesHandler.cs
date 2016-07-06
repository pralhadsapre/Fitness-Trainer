using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using TrainingApp.Models;
using System.Json;

namespace TrainingApp.Utilities
{
    public class FilesHandler
    {
        public static List<PlanInfo> GetAvailablePlans()
        {
            List<PlanInfo> plans = new List<PlanInfo>();
            try
            {
                System.IO.Stream src = Application.GetResourceStream(new Uri("Plans/Info.txt", UriKind.Relative)).Stream;
                StreamReader sr = new StreamReader(src);
                JsonObject parent = JsonObject.Parse(sr.ReadToEnd()) as JsonObject;
                JsonArray plansArray = parent["plans"] as JsonArray;

                PlanInfo planObj;
                int index = 0;
                foreach (JsonObject obj in plansArray)
                {
                    planObj = new PlanInfo();
                    planObj.ShortName = obj["planMetaData"]["shortName"];
                    planObj.LongName = obj["planMetaData"]["longName"];
                    planObj.Duration = obj["planMetaData"]["Duration"];
                    planObj.RunsPerWeek = obj["planMetaData"]["runsPerWeek"];
                    planObj.WeeklyKM = obj["planMetaData"]["weeklyKM"];
                    planObj.LongestRun = obj["planMetaData"]["longestRun"];
                    planObj.Type = obj["planMetaData"]["type"];
                    planObj.FileName = obj["planMetaData"]["fileName"];
                    planObj.Index = index++;

                    foreach (JsonObject planDetails in (obj["planDetails"] as JsonArray))
                        planObj.Details.Add(new Details { Heading = planDetails["heading"], Data = planDetails["data"] });

                    foreach (JsonObject planStrategies in (obj["planStrategies"] as JsonArray))
                        planObj.Strategies.Add(new Details { Heading = planStrategies["heading"], Data = planStrategies["data"] });

                    plans.Add(planObj);
                }
                return plans;
            }
            catch (Exception exp) { return null; }
        }

        public static PlanSchedule GetPlanSchedule(string fileName)
        {
            PlanSchedule plan = new PlanSchedule();

            try
            {
                System.IO.Stream src = Application.GetResourceStream(new Uri("Plans/" + fileName + ".txt", UriKind.Relative)).Stream;
                StreamReader sr = new StreamReader(src);
                JsonObject parent = JsonObject.Parse(sr.ReadToEnd()) as JsonObject;
                foreach (JsonObject day in (parent["planSchedule"] as JsonArray))
                {
                    plan.Schedule.Add(new ScheduleDay
                    {
                        Description = day["description"],
                        Distance = day["distance"],
                        Tips = day["tips"],
                        IsLogged = Visibility.Visible
                    });
                }
                return plan;
            }
            catch (Exception exp) { return null; }
        }
    }
}
