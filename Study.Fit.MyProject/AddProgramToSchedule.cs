using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.Fit.MyProject
{
    /// <summary>
    /// classes must be public to be accessed from Fitnesse
    /// </summary>
    public class AddProgramsToSchedule
    {
        private static Schedule _schedule;

        internal static Schedule Schedule
        {
            get
            {
                if (_schedule == null)
                {
                    _schedule = new Schedule();
                }
                return _schedule;
            }
        }

        private static int s_numberCreated = 0;

        private bool _lastCreationSuccessful;

        public string Name { get; set; }

        public string Episode { get; set; }

        public int Channel { get; set; }

        public string Date { get; set; }

        public string StartTime { get; set; }

        public int Minutes { get; set; }

        private string _lastId;

        public string LastId
        {
            get
            {
                if (_lastCreationSuccessful)
                {
                    return _lastId;
                }
                else
                {
                    return "n/a";
                }
            }
        }

        public bool Created
        {
            get { return _lastCreationSuccessful; }
        }

        public bool Execute()
        {
            try
            {
                Program program = Schedule.AddProgram(Name, Episode, Channel, BuildStartDateTime(), Minutes);
                _lastId = program.GetId();
                _lastCreationSuccessful = true;
                return true;
            }
            catch (ConflictingProgramException)
            {
                _lastCreationSuccessful = false;
                return false;
            }
        }

        private DateTime BuildStartDateTime()
        {
            string dateTime = String.Format("{0} {1}", Date, StartTime);
            return DateTime.Parse(dateTime);
        }

        public AddProgramsToSchedule()
        {
            Console.WriteLine("Creating ProgramsToSchedule #{0}\n", ++s_numberCreated);
        }
    }
}
