using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.Serialization;

namespace TrainingApp.Models
{
    [DataContract]
    public class PlanSchedule
    {
        [DataMember]
        public List<ScheduleDay> Schedule { get; set; }

        public PlanSchedule()
        {
            Schedule = new List<ScheduleDay>();
        }
    }

    [DataContract]
    public class ScheduleDay
    {
        [DataMember]
        public string Distance { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Tips { get; set; }

        [DataMember]
        public Visibility IsLogged { get; set; }
    }
}
