namespace Calculator.Engine
{
    /// <summary>
    /// Converts strings into numeric expressions
    /// </summary>
    public class Parser
    {
        public int[] ParseValues(string input)
        {
            var res = input.Split(['-', '+', '/', '*', '=']);

            return [.. res.Select(int.Parse)];
        }

        public bool IsDigit(char input)
        {
            return input >= '0' && input <= '9';
        }

        public bool IsOperator(char input)
        {
            string operands = "-+/*=";

            for (int i = 0; i < operands.Length; i++)
            {
                if (input == operands[i])
                    return true;
            }

            return false;
        }
    }
}
