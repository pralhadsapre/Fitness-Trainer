using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrainingApp.Models
{
    public class PlanInfo
    {
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string Duration { get; set; }
        public string RunsPerWeek { get; set; }
        public string WeeklyKM { get; set; }
        public string LongestRun { get; set; }
        public string Type { get; set; }
        public string FileName { get; set; }
        public int Index { get; set; }
        
        public List<Details> Details { get; set; }
        public List<Details> Strategies { get; set; }

        public PlanInfo()
        {
            Details = new List<Details>();
            Strategies = new List<Details>();
        }
    }

    public class Details
    {
        public string Heading { get; set; }
        public string Data { get; set; }
    }
}
