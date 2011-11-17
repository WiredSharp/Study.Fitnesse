using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.Fit.MyProject
{
    public class Program
    {
        public readonly string Name;
        private readonly string _episode;
        public readonly TimeSlot TimeSlot;

        public Program(string name, string episode, TimeSlot timeSlot)
        {
            Name = name;
            _episode = episode;
            TimeSlot = timeSlot;
        }

        public string GetId()
        {
            return String.Format("({0}:{1})", Name, TimeSlot.Channel);
        }
    }
}
