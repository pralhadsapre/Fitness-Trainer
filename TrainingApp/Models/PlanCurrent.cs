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
    public class PlanCurrent
    {        
        [DataMember]
        public int _daysLeft;

        [DataMember]
        public double _distanceRan;

        [DataMember]
        public double _durationRan;

        [DataMember]
        public DateTime _startDate;

        [DataMember]
        public DateTime _marathonDate;

        [DataMember]
        public int _dayIndex;

        [DataMember]
        public int _initialDayIndex;

        [DataMember]
        public int _initialDaysLeft;

        [DataMember]
        public PlanSchedule _plan;

        [IgnoreDataMember]
        public ScheduleDay Today { get; set; }

        [IgnoreDataMember]
        public List<ScheduleDay> FourDaysPlan { get; set; }
        
        public string DaysLeft { get { return _daysLeft + " days to go"; } }

        [DataMember]
        public string ShortName { get; set; }

        [DataMember]
        public string LongName { get; set; }

        public string DistanceRan { get { return _distanceRan + " km"; } }
        public string DurationRan { get { return _durationRan + " minutes"; } }

        public PlanCurrent(PlanSchedule schedule, PlanInfo info, DateTime marathonDate)
        {
            _plan = schedule;
            ShortName = info.ShortName;
            LongName = info.LongName;            
            _distanceRan = 0;
            _durationRan = 0;            
            _startDate = DateTime.Now;
            _marathonDate = marathonDate;

            _initialDayIndex = _plan.Schedule.Count - _marathonDate.Subtract(DateTime.Now).Days - 1;
            if (_initialDayIndex < 0)
                _initialDayIndex = 0;

            _initialDaysLeft = _marathonDate.Subtract(DateTime.Now).Days;
            if (_initialDaysLeft > _plan.Schedule.Count)
                _initialDaysLeft = _plan.Schedule.Count;
                     
        }

        private void PrepareIndexes()
        {
            _dayIndex = _initialDayIndex + DateTime.Now.Subtract(_startDate).Days;
            _daysLeft = _initialDaysLeft - DateTime.Now.Subtract(_startDate).Days;

            if (_dayIndex > (_plan.Schedule.Count - 1))
                _dayIndex = _plan.Schedule.Count;
            else if (_dayIndex < 0)
                _dayIndex = 0;

            if (_daysLeft < 0)
                _daysLeft = 0;
            else if (_daysLeft > _plan.Schedule.Count)
                _daysLeft = _plan.Schedule.Count;
        }

        public void PrepareFiveDays()
        {
            PrepareIndexes();
            FourDaysPlan = new List<ScheduleDay>();
            Today = new ScheduleDay();
            if (_dayIndex < _plan.Schedule.Count)
            {
                Today = _plan.Schedule.ElementAt(_dayIndex);
                for (int i = _dayIndex + 1; (i < _plan.Schedule.Count) && (i < _dayIndex + 5); i++)
                    FourDaysPlan.Add(_plan.Schedule.ElementAt(i));
            }
        }

        public void LogData(double distance, double duration)
        {
            _distanceRan += distance;
            _durationRan += duration;
            _plan.Schedule.ElementAt(_dayIndex).IsLogged = Visibility.Collapsed;
            //_dayIndex++;
            //_daysLeft--;
        }
    }
}
