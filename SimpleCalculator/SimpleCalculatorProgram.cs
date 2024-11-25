using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SimpleCalculatorLibrary;
using StringUtilsLibrary;


namespace SimpleCalculatorProgram
{


    class SimpleCalculatorProgram
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            double result = 0;
            string? numInput;
            CalcReturnStruct operationIn = new CalcReturnStruct();

            Console.WriteLine("Simple calculator in C#\r");
            Console.WriteLine("-----------------------");
            Console.WriteLine("Possible operators +-/*");

            while (!endApp)
            {
                Console.Write("Type the calculation in form 4 + 5, and then press Enter: ");
                numInput = Console.ReadLine();

                operationIn.Success = false;
                while (!CalcInputParser.ParseInput(numInput, ref operationIn))
                {
                    Console.Write("Input not valid. Please try again:");
                    numInput = Console.ReadLine();
                }

                try
                {
                    result = SimpleCalculator.DoOperation(operationIn.FirstNumber, operationIn.SecondNumber, operationIn.OperationDefiner);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            return;
        }
    }
}