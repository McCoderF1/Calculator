namespace Calculator.Engine.Interfaces
{
    public interface IParser
    {
        long[] ParseValues(string input);

        bool IsOperator(char input);
    }
}