namespace StringUtilsLibrary
{
    public struct CalcReturnStruct
    {
        public double FirstNumber;
        public double SecondNumber;
        public char OperationDefiner;
        public bool Success;
    }

    internal enum CalcTaskEnum
    {
        FindFirstDouble,
        FindOperator,
        FindSecondDouble,
        Done
    }

    public class CalcInputParser
    {
        // Divide this to be something that gathers the doubles and operator and something that validates them.
        // They should be named something like gather operands(or whatever is a name of math calculation symbols) and validate operands
        public static bool ParseInput(string? sIn, ref CalcReturnStruct oOut)
        {
            int n = 0;
            double doubleOut;
            string firstS = string.Empty;
            string secondS = string.Empty;
            char o = 'a';
            CalcTaskEnum task = CalcTaskEnum.FindFirstDouble;

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
    public class StringUtilsLibrary
    {

    }
}
