namespace ReversePolishNotation
{
    public static class RPNCalculator
    {
        public static decimal Calculate(string expression)
        {
            Stack<decimal> reader = new Stack<decimal>();
            string[] splitter = expression.Split();

            if (splitter.Length == 0)
            {
                return 0;
            }

            for (int i = 0; i < splitter.Length; i++)
            {
                bool isNum = decimal.TryParse(splitter[i], out decimal num);
                if (isNum)
                {
                    reader.Push(num);
                }
                else
                {
                    ApllyOperator(reader, splitter[i]);
                }
            }

            return reader.Pop();
        }

        private static Stack<decimal> ApllyOperator(Stack<decimal> reader, string ariphmeticalOperator)
        {
            if (reader.Count == 0)
            {
                throw new ArgumentException("Wrong request");
            }

            var (firstOperand, secondOperand) = (reader.Pop(), reader.Pop());
            var calculated = ariphmeticalOperator switch
            {
                "+" => firstOperand + secondOperand,
                "*" => firstOperand * secondOperand,
                "-" => secondOperand - firstOperand,
                "/" when firstOperand is 0 => throw new DivideByZeroException("Cannot divide by zero"),
                "/" => secondOperand / firstOperand,
                _ => throw new ArgumentException("Wrong operand or operator")
            };
            reader.Push(calculated);
            return reader;
        }
    }
}