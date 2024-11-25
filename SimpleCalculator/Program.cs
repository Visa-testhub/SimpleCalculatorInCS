using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


class Calculator
{
    public static double DoOperation(double num1, double num2, char op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.

        // Use a switch statement to do the math.
        switch (op)
        {
            case '+':
                result = num1 + num2;
                break;
            case '-':
                result = num1 - num2;
                break;
            case '*':
                result = num1 * num2;
                break;
            case '/':
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        return result;
    }
}

public struct CalcReturnStruct 
{
    public double   FirstNumber;
    public double   SecondNumber;
    public char     OperationDefiner;
    public bool     Success;
}

enum CalcTaskEnum
{
    FindFirstDouble,
    FindOperator,
    FindSecondDouble,
    Done
}

class CalcInputParser
{
    // Divide this to be something that gathers the doubles and operator and something that validates them.
    // They should be named something like gather operands(or whatever is a name of math calculation symbols) and validate operands
    public static bool parseInput(string? sIn, ref CalcReturnStruct oOut)
    { 
        int             n = 0;
        double          doubleOut;
        string          firstS = string.Empty;
        string          secondS = string.Empty;
        char            o = 'a';
        CalcTaskEnum    task = CalcTaskEnum.FindFirstDouble;

        if (sIn == null)
        {
            return oOut.Success;
        }

        while (n < sIn.Length)
        {
            while (Char.IsWhiteSpace(sIn[n]))
            {
                n++;
                if (n == sIn.Length)
                {
                    n--;
                    break;
                }
            }

            if (Char.IsDigit(sIn[n]) || Char.Equals(',', sIn[n]) || Char.Equals('.', sIn[n]))
            {
                if (task == CalcTaskEnum.FindFirstDouble)
                {
                    firstS = firstS + sIn[n];
                }
                else if (task == CalcTaskEnum.FindSecondDouble)
                {
                    secondS = secondS + sIn[n];
                }
            }
            else
            {
                if (task == CalcTaskEnum.FindFirstDouble)
                {
                    task = CalcTaskEnum.FindOperator;
                }
                else if (task == CalcTaskEnum.FindSecondDouble)
                {
                    task = CalcTaskEnum.Done;
                }

                if (Char.Equals(sIn[n], '+') || Char.Equals(sIn[n], '-') || Char.Equals(sIn[n], '/') || Char.Equals(sIn[n], '*'))
                {
                    if (task == CalcTaskEnum.FindOperator)
                    {
                        o = sIn[n];
                        task = CalcTaskEnum.FindSecondDouble;
                    }
                }
            }
            n++;
        }

        if (String.IsNullOrEmpty(firstS))
        {
            return oOut.Success;
        }
        else
        {
            if (Double.TryParse(firstS, out doubleOut))
            {
                oOut.FirstNumber = doubleOut;
            }
            else
            {
                return oOut.Success;
            }
        }

        if (String.IsNullOrEmpty(secondS))
        {
            return oOut.Success;
        }
        else
        {
            if (Double.TryParse(secondS, out doubleOut))
            {
                oOut.SecondNumber = doubleOut;
            }
            else
            {
                return oOut.Success; 
            }
        }
    
        if (o == 'a') 
        {
            return oOut.Success;
        }
        else
        {
            oOut.OperationDefiner = o;
            oOut.Success = true;
        }
        return oOut.Success;
    }

}
class Program
{
    static void Main(string[] args)
    {
        bool                endApp = false;
        double              result = 0;
        string?             numInput;
        CalcReturnStruct    operationIn = new CalcReturnStruct();

        Console.WriteLine("Simple calculator in C#\r");
        Console.WriteLine("-----------------------");
        Console.WriteLine("Possible operators +-/*");

        while (!endApp)
        {
            Console.Write("Type the calculation in form 4 + 5, and then press Enter: ");
            numInput = Console.ReadLine();

            operationIn.Success = false;
            while (!CalcInputParser.parseInput(numInput, ref operationIn))
            {
                Console.Write("Input not valid. Please try again:");
                numInput = Console.ReadLine();
            }
            
            try
            {
                result = Calculator.DoOperation(operationIn.FirstNumber, operationIn.SecondNumber, operationIn.OperationDefiner);
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