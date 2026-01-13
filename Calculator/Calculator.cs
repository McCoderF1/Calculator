using System;
using System.Collections.Generic;
using Calculator.Engine.Interfaces;

namespace Calculator.Engine
{
    public class Calculator : ICalculator
    {
        public long? EvaluateExpression(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                return 0;

            if (!TryTokenize(expression, out List<string> tokens))
                return null;

            if (!TryConvertToPostfixNotation(tokens, out List<string> postfixNotationTokens))
                return null;

            if (!TryEvaluatePostfixNotation(postfixNotationTokens, out long result))
                return null;

            return result;
        }

        private static bool TryTokenize(string expression, out List<string> tokens)
        {
            tokens = new List<string>();
            int i = 0;

            while (i < expression.Length)
            {
                char c = expression[i];

                if (char.IsWhiteSpace(c))
                {
                    i++;
                    continue;
                }

                if (char.IsDigit(c))
                {
                    int start = i;
                    while (i < expression.Length && char.IsDigit(expression[i]))
                        i++;

                    tokens.Add(expression.Substring(start, i - start));
                    continue;
                }

                if (IsOperatorChar(c))
                {
                    tokens.Add(c.ToString());
                    i++;
                    continue;
                }

                return false;
            }

            return tokens.Count > 0;
        }

        private static bool TryConvertToPostfixNotation(List<string> tokens, out List<string> output)
        {
            output = new List<string>(tokens.Count);
            Stack<string> operatorStack = new Stack<string>();

            foreach (string token in tokens)
            {
                if (IsNumber(token))
                {
                    output.Add(token);
                    continue;
                }

                if (!IsOperator(token))
                    return false;

                while (operatorStack.Count > 0 && Precedence(operatorStack.Peek()) >= Precedence(token))
                    output.Add(operatorStack.Pop());

                operatorStack.Push(token);
            }

            while (operatorStack.Count > 0)
                output.Add(operatorStack.Pop());

            return output.Count > 0;
        }

        private static bool TryEvaluatePostfixNotation(List<string> postfixNotationTokens, out long result)
        {
            result = 0;
            Stack<long> stack = new Stack<long>();

            foreach (string token in postfixNotationTokens)
            {
                if (IsNumber(token))
                {
                    if (!long.TryParse(token, out long value))
                        return false;

                    stack.Push(value);
                    continue;
                }

                if (!IsOperator(token))
                    return false;

                if (stack.Count < 2)
                    return false;

                long right = stack.Pop();
                long left = stack.Pop();

                long? applied = Apply(left, token, right);
                if (!applied.HasValue)
                    return false;

                stack.Push(applied.Value);
            }

            if (stack.Count != 1)
                return false;

            result = stack.Pop();
            return true;
        }

        private static long? Apply(long left, string op, long right)
        {
            return op switch
            {
                "+" => left + right,
                "-" => left - right,
                "*" => left * right,
                "/" => right == 0 ? null : left / right,
                _ => null
            };
        }

        private static int Precedence(string op)
        {
            return op switch
            {
                "*" => 2,
                "/" => 2,
                "+" => 1,
                "-" => 1,
                _ => 0
            };
        }

        private static bool IsOperator(string token) => token is "+" or "-" or "*" or "/";
        private static bool IsOperatorChar(char c) => c is '+' or '-' or '*' or '/';
        private static bool IsNumber(string token) => token.Length > 0 && char.IsDigit(token[0]);
    }
}
