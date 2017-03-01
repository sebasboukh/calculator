using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalculatorParser
{
    public class CalculatorParser
    {
        /// <summary>
        /// Parses the input string and returns the result of the calculation
        /// </summary>
        /// <exception cref="FormatException"></exception>
        /// <param name="input">the string to parse</param>
        /// <returns></returns>
        public static int Calculate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new FormatException("Input is null or empty");
            }

            try
            {
                int result;
                int index = 0;

                // Get the first Member
                Member member = GetMember(input, ref index);
                switch (member.Operator)
                {
                    // the input can start with an operator or not 
                    // UNDEFINED means PLUS
                    // ex: "-1" , "+3", "7" 
                    case Operator.MINUS:
                        result = -member.Value;
                        break;
                    default:
                        result = member.Value;
                        break;
                }

                // Compute the other members
                while ((member = GetMember(input, ref index)) != null)
                {
                    switch (member.Operator)
                    {
                        case Operator.UNDEFINED:
                            throw new FormatException();
                        case Operator.PLUS:
                            result += member.Value;
                            break;
                        case Operator.MINUS:
                            result -= member.Value;
                            break;
                    }
                }

                return result;

            }
            catch (Exception)
            {
                throw new FormatException("Input is malformed");
            }
        }

        /// <summary>
        /// Parses the input string and returns the first Member or null
        /// </summary>
        /// <param name="input">string to parse</param>
        /// <param name="index">start index</param>
        /// <returns>The first member or null</returns>
        private static Member GetMember(string input, ref int index)
        {
            // ignore spaces
            while (index < input.Length && input[index] == ' ')
            {
                index++;
            }

            if (index == input.Length)
            {
                return null;
            }

            // get the operator
            Operator op = Operator.UNDEFINED;
            switch (input[index])
            {
                case '+':
                    op = Operator.PLUS;
                    // move the index
                    index++;
                    break;
                case '-':
                    op = Operator.MINUS;
                    // move the index
                    index++;
                    break;
            }

            // ignore spaces
            while (index < input.Length && input[index] == ' ')
            {
                index++;
            }

            // get the value
            Regex regex = new Regex(@"\d");
            int startIndex = index;
            while (index < input.Length && regex.IsMatch(new string(input[index], 1)))
            {
                index++;
            }

            int value = int.Parse(input.Substring(startIndex, index - startIndex));

            return new Member { Operator = op, Value = value };
        }



    }
}
