using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.Fit.MyProject
{
    public class RemoveProgramById
    {
        public RemoveProgramById(string id)
        {
            AddProgramsToSchedule.Schedule.RemoveProgramById(id);
        }
    }
}
