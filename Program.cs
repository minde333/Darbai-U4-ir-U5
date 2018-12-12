using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp36
{
    class Program
    {
        public static void Main()
        {
            double windowWidth = readValue("Enter width of window: ", MIN_WIDTH, MAX_WIDTH, "Width is not between " + MIN_WIDTH + " and " + MAX_WIDTH);
            Console.WriteLine("Width: " + windowWidth);
            double age = readValue("Enter your age: ", MIN_AGE, MAX_AGE, "Age is not between " + MIN_AGE + " and " + MAX_AGE);
            Console.WriteLine("Age: " + age);
        }
        static double readValue(string prompt, double low, double high, string error)

        {
            double result = 0;

            do
            {
                try
                {
                    Console.WriteLine(prompt +
                " between " + low +
                " and " + high);
                    result = int.Parse(Console.ReadLine());
                    if ((result <= low) || (result >= high))
                    {
                        Console.WriteLine(error);
                    }
                    continue;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    // Code that is obeyed whether an exception
                    // is thrown or not
                }

            } while ((result <= low) || (result >= high));

            return result;
        }
        const double MAX_WIDTH = 5.0;
        const double MIN_WIDTH = 0;
        const int MAX_AGE = 100;
        const int MIN_AGE = 0;
    }
}
 