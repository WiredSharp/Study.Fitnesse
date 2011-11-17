using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.Fit.MyProject
{
    public class EpisodesInToDoList
    {
        public EpisodesInToDoList(string programId)
        { }

        private static List<object> List(params object[] objs)
        {
            return objs.ToList();
        }

        public List<Object> Query()
        {
            return
               List(
                  List(
                     List("episodeName", "E1"),
                     List("date", "17/5/2008"),
                     List("startTime", "7:00")
                  )
               );
        }
    }
}
