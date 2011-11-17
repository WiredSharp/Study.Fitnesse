using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.Fit.MyProject
{
    public class SeasonPassManager
    {
       private readonly Schedule _schedule;
       private IEnumerable<Program> _toDoList;
 
       public SeasonPassManager(Schedule schedule) {
          this._schedule = schedule;
          _toDoList = Enumerable.Empty<Program>();
       }
 
       public int SizeOfToDoList() {
           return _toDoList.Count();
       }
 
       public void CreateNewSeasonPass(string programName, int channel) {
          _toDoList = _schedule.FindProgramsNamedOn(programName, channel);
       }
    }
}
