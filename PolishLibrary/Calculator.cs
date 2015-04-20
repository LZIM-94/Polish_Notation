using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PolishLibrary
{
   public class Calculator
    {

      public  static double Calculate(List<string> PolishNote)
        {
            try
            {
                Stack<double> stack = new Stack<double>();
                List<double> result = new List<double>();
                foreach (string token in PolishNote)
                {
                    if (!Operators.IsOperator(token))
                    {
                        stack.Push(Convert.ToDouble(token));
                    }
                    else
                    {
                        if (Operators.OperatorsPriority[token] == 4)
                        {
                            if (stack.Count == 0) throw new MyCalculateException("После унарного оператора отсутствует операнд.");
                            else stack.Push(Calc(token, stack.Pop()));//вычисление унарных операторов
                        }
                        else
                        {
                            if (stack.Count == 1 && (token == "+" || token == "-"))
                            {
                                stack.Push(Calc(token, stack.Pop(), 0)); //если вдруг перед минусом ничего не окажется или будет перед числом написан плюс
                            }
                            else
                            {
                                if (stack.Count == 1 || stack.Count == 0) throw new MyCalculateException("Неверное расположение операторов и операндов.");
                                else stack.Push(Calc(token, stack.Pop(), stack.Pop()));//вычисление бинарных
                            }

                        }

                    }
                }

                if (result.Count == 0)
                {
                    if (stack.Count == 0) throw new MyCalculateException("Выражение отсутствует");
                    else result.Add(stack.Pop());
                }

                return result[0];
            }
            catch (MyCalculateException ex)
            {
                throw new MyCalculateException("Ошибка вычисления: " + ex.Message);
                
            }

        }

        static double Calc(string token, double op1, double op2)
        {

            switch (token)
            {
                case "+": return op2 + op1;
                case "-": return op2 - op1;
                case "*": return op2 * op1;
                case "/": if (op1 == 0) throw new MyCalculateException("Деление на ноль."); else return op2 / op1;
                case "^": return Math.Pow(op2,op1);
                default: return 0.0;
               
            }
        }


        static double Calc(string token, double op1)
        {
          
                switch (token)
                {
                    case "sin": return Math.Sin(op1);
                    case "cos": return Math.Sin(op1);
                    case "tan": return Math.Sin(op1);
                    case "cotan": return Calc("/", 1, Math.Tan(op1));
                    case "ctan": return Calc("/", 1, Math.Tan(op1));
                    case "ln": if (op1 == 0) throw new MyCalculateException("Логарифм от нуля не берется.");else return Math.Log(op1);
                    case "log": if (op1 == 0) throw new MyCalculateException("Логарифм от нуля не берется.");else return Math.Log10(op1);
                    default: return 0.0;

                }
            
          
        }
        
       

    }
}
