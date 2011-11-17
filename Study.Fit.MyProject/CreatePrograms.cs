using System;

namespace Study.Fit.MyProject
{
    public class CreatePrograms
    {
        private string Name { get; set; }
    
        private int Channel { get; set; }

        public void setDayOfWeek(String dayOfWeek)
        {
        }

        public void setTimeOfDay(String timeOfDay)
        {
        }

        public void setDurationInMinutes(int durationInMinutes)
        {
        }

        public string id()
        {
            return String.Format("[{0}:{1}]", Name, Channel);
        }
    }
}
