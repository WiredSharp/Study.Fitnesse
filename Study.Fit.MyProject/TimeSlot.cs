using System;

namespace Study.Fit.MyProject
{
    public class TimeSlot
    {
        public readonly int Channel;
        private readonly DateTime Start;
        private readonly int Minutes;

        public TimeSlot(int channel, DateTime start, int minutes)
        {
            // TODO: Complete member initialization
            Channel = channel;
            Start = start;
            Minutes = minutes;
        }

        public bool ConflictWith(TimeSlot other)
        {
            return Channel == other.Channel && Start == other.Start;
        }
    }
}