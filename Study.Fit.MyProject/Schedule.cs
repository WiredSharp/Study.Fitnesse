using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.Fit.MyProject
{
    public class Schedule
    {
        private readonly List<Program> _scheduledPrograms = new List<Program>();

        public Program AddProgram(string programName, string episodeName, int channel,
              DateTime startDateTime, int lengthInMinutes)
        {

            var timeSlot = new TimeSlot(channel, startDateTime, lengthInMinutes);

            if (ConflictsWithOtherTimeSlots(timeSlot))
                throw new ConflictingProgramException();

            var program = new Program(programName, episodeName, timeSlot);
            _scheduledPrograms.Add(program);
            return program;
        }

        private bool ConflictsWithOtherTimeSlots(TimeSlot timeSlot)
        {
            return _scheduledPrograms.Any(program => program.TimeSlot.ConflictWith(timeSlot));
        }

        public void RemoveProgramById(string id)
        {
            _scheduledPrograms.RemoveAll(program => program.GetId() == id);
        }

        public IEnumerable<Program> FindProgramsNamedOn(string programName, int channel)
        {
            return _scheduledPrograms.Where(prg => prg.TimeSlot.Channel == channel && prg.Name == programName);
        }
    }
}
