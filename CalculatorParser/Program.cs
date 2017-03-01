using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorParser
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length > 0)
                {
                    ShowCalculateResult(args[0]);
                }
            }
            finally
            {
                Console.WriteLine("Press Enter to exit");
                Console.ReadLine();
            }
        }

        private static void ShowCalculateResult(string value)
        {
            try
            {
                Console.WriteLine("{0} = {1}", value, CalculatorParser.Calculate(value));
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Malformed input : " + ex.Message);
            }
        }
    }
}
