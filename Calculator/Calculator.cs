
namespace Calculator.Engine
{
    public class Calculator
    {
        public int? Add(params int[] v)
        {
            int? result = null;

            result = v[0];

            if (v.Length > 1)
            {
                for (int i = 1; i < v.Length; i++)
                {
                    result += v[i];
                }
            }

            return result;
        }
    }
}
