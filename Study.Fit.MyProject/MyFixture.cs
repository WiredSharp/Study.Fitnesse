using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.Fit.MyProject
{
    public class MyFixture
    {
        public int MyInt { get; set; }

        public string MyString { get; set; }

        public double MyDouble { get; set; }

        public double Multiply()
        {
            if (MyDouble == -1.0)
                throw new InvalidOperationException("MyDouble has not been set !");
            return MyInt * MyDouble;
        }

        public void ShowMultiply()
        {
            MyString = String.Format("{0} x {1} = {2}", MyInt, MyDouble, Multiply());
        }

        public MyFixture()
        {
            MyDouble = -1.0;
        }

        public void Execute()
        {
            ShowMultiply();
        }
    }
}
