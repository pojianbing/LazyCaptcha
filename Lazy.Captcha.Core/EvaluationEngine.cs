using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lazy.Captcha.Core
{
    public class EvaluationEngine
    {
        /// <summary>
        /// 操作符级别映射
        /// </summary>
        private Dictionary<char, int> OperatorLevelMaps = new Dictionary<char, int>
        {
            { '(', 1 },
            { ')', 1 },
            { '+', 2 },
            { '-', 2 },
            { 'x', 3 },
            { '*', 3 },
            { '÷', 3 },
            { '/', 3 },
            { '^', 4 },
            { '√', 4 },
            { '%', 4 }
        };

        /// <summary>
        /// 是否数字
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool IsNumber(string str)
        {
            return double.TryParse(str, out var _);
        }

        /// <summary>
        /// 是否数字
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool IsNumber(char c)
        {
            var charCode = (int)c;
            return charCode >= 48 && charCode <= 57;
        }

        /// <summary>
        /// 转为逆波兰
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private List<string> ConvertToRPN(string exp)
        {
            var output = new List<string>();
            var index = 0;
            var operatorStack = new Stack<char>();

            while (index < exp.Count())
            {
                var currentIndex = index;
                var prevIndex = index - 1;
                var prev = prevIndex < 0 ? "" : exp[prevIndex] + "";
                var current = exp[currentIndex];
                var isNegativeSign = current == '-' && currentIndex == 0;

                if (OperatorLevelMaps.ContainsKey(current) && !isNegativeSign)
                {
                    if (current == '(')
                    {
                        operatorStack.Push(current);
                    }
                    else if (current == ')')
                    {
                        while (operatorStack.Peek() != '(')
                        {
                            output.Add(operatorStack.Pop().ToString());
                        }
                        operatorStack.Pop(); // )弹出
                    }
                    else
                    {
                        while (operatorStack.Count != 0 && operatorStack.Peek() != '(' && OperatorLevelMaps[current] <= OperatorLevelMaps[operatorStack.Peek()])
                        {
                            output.Add(operatorStack.Pop().ToString());
                        }
                        operatorStack.Push(current);
                    }

                    index++;
                }
                else
                {
                    if (isNegativeSign) index++;
                    // 扫描连续数字: -1.23
                    while (index < exp.Count() && (IsNumber(exp[index]) || exp[index] == '.') && exp[index] != ' ')
                    {
                        index++;
                    }
                    output.Add(exp.Substring(currentIndex, index - currentIndex));
                }
            }

            while (operatorStack.Count != 0) output.Add(operatorStack.Pop().ToString());

            return output;
        }

        /// <summary>
        /// 根据运算符计算
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="op"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private double Cacl(double v1, double v2, string op)
        {
            switch (op)
            {
                case "+":
                    return v1 + v2;
                case "-":
                    return v1 - v2;
                case "x":
                case "*":
                    return v1 * v2;
                case "÷":
                case "/":
                    return v1 / v2;
                case "^":
                    return Math.Pow(v1, v2);
            }

            throw new Exception("未知操作符");
        }

        /// <summary>
        /// 根据逆波兰计算
        /// </summary>
        /// <param name="rpn"></param>
        /// <returns></returns>
        private double CalcRPN(List<string> rpn)
        {
            var stack = new Stack();

            for (var i = 0; i < rpn.Count; i++)
            {
                var item = rpn[i];

                if (IsNumber(item))
                {
                    stack.Push(item);
                }
                else
                {
                    if (item == "√")
                    {
                        var v = stack.Pop();
                        stack.Push(Math.Sqrt((double)v));
                    }
                    else if (item == "%")
                    {
                        stack.Push(PopDouble() / 100.0d);
                    }
                    else
                    {
                        var v1 = PopDouble();
                        var v2 = PopDouble();
                        var v = Cacl(v2, v1, item);
                        stack.Push(v);
                    }
                }

                double PopDouble()
                {
                    var v = (string)stack.Pop();
                    return double.Parse(v);
                }
            }

            return (double)stack.Pop();
        }

        /// <summary>
        /// 计算
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public double Evaluate(string exp)
        {
            exp = exp.Replace(" ", "");
            var rpn = ConvertToRPN(exp);
            return CalcRPN(rpn);
        }
    }
}
