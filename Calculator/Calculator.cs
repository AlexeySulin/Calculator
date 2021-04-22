using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public static class Calculator
    {
        public static double Calualte(string inputExpr) //главный метод вычисления выражения
        {
            double result = 0.0;
            try
            {
                string postfixExpr = GetPostfixExpression(inputExpr);
                result = Count(postfixExpr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        private static string GetPostfixExpression(string input) //Получение постфиксной записи выражения
        {
            Stack<char> operations = new Stack<char>();
            string resString = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                if (IsSeparator(input[i])) //если символ - ' ' или '='
                    continue;
                else if (char.IsDigit(input[i]) || input[i] == '.')// если символ - цифра
                {
                    while (!IsSeparator(input[i]) && !IsOperation(input[i]))
                    {
                        resString += input[i];
                        i++;
                        if (i == input.Length)
                            break;
                    }
                    resString += " ";
                    i--;
                }
                else if (IsOperation(input[i])) //если символ операция
                {
                    if (input[i] == ')') //если операция - ')', выписываем все операции из стэка до '('
                    {
                        char c = operations.Pop();
                        while (c != '(')
                        {
                            resString += c;
                            c = operations.Pop();
                        }
                    }
                    else
                    {
                        if (operations.Count != 0)
                        {
                            if (GetPriority(input[i]) <= GetPriority(operations.Peek()) && input[i] != '(')
                                resString += operations.Pop();
                        }
                        operations.Push(input[i]);
                    }
                }
            }
            while (operations.Count > 0)
                resString += operations.Pop();

            if (string.IsNullOrEmpty(resString))
                throw new Exception("Неверный формат ввода! Введите корретное выражение.");
            return resString;
        }

        private static double Count(string postfixExpr) //Считаем значение выражения
        {
            Stack<double> result = new Stack<double>();
            for (int i = 0; i < postfixExpr.Length; i++)
            {
                if (char.IsDigit(postfixExpr[i])) //Если встретили цифру
                {
                    string s = string.Empty;
                    double d = 0;
                    while (!IsSeparator(postfixExpr[i]))
                    {
                        s += postfixExpr[i];
                        i++;
                    }
                    if (!Double.TryParse(s, out d))
                    {
                        throw new Exception("Неправильный формат ввода вещественного числа. Введи вещественное число согласно примеру: 3,14");
                    }
                    else
                        result.Push(d);
                }
                if (IsOperation(postfixExpr[i]))
                {
                    result.Push(PerfomOperation(result.Pop(), result.Pop(), postfixExpr[i]));
                }
            }
            return result.Peek();
        }

        private static double PerfomOperation(double b, double a, char op) //выполнение операции
        {
            double res = 0;
            switch (op)
            {
                case '+':
                    res = a + b;
                    break;
                case '-':
                    res = a - b;
                    break;
                case '*':
                    res = a * b;
                    break;
                case '/':
                    if (b == 0)
                        throw new DivideByZeroException("Деление на ноль запрещено!");
                    else
                        res = a / b;
                    break;
                case '^':
                    res = Math.Pow(a, b);
                    break;
            }
            return res;
        }

        private static bool IsSeparator(char c) => " =".Contains(c); //является ли символ разделителем

        private static bool IsOperation(char op) => "()+-/*^".Contains(op); //определяем операцию 

        private static int GetPriority(char op) //определяем приоритет операции
        {
            switch (op)
            {
                case '(':
                    return 0;
                case ')':
                    return 1;
                case '+':
                case '-':
                    return 2;
                case '*':
                case '/':
                    return 3;
                case '^':
                    return 4;
                default:
                    return 5;
            }
        }
    }
}
