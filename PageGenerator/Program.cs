using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator = Study.Fit.PageGenerator.PageGenerator;

namespace PageGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var generator = new Generator(args[0], args[1]);
                generator.GeneratePages();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
