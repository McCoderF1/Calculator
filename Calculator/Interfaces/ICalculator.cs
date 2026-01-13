namespace Calculator.Engine.Interfaces
{
    public interface ICalculator
    {
        long? EvaluateExpression(string expression);
    }
}