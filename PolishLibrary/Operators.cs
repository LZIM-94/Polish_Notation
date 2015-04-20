using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PolishLibrary
{
    static public class Operators
    {
       static Dictionary<string, int> operatorsPriority;

       static public Dictionary<string, int> OperatorsPriority
        {
            get { return operatorsPriority; }
            set { operatorsPriority = value; }
        }

        public static void InitOperators() //Создадим словарь для быстрого получения приритета
        {
            if (OperatorsPriority == null)
            {
                operatorsPriority = new Dictionary<string, int>();
                operatorsPriority.Add("+", 1);
                operatorsPriority.Add("-", 1);

                operatorsPriority.Add("*", 2);
                operatorsPriority.Add("/", 2);

                operatorsPriority.Add("^", 3);

                operatorsPriority.Add("sin", 4);
                operatorsPriority.Add("cos", 4);
                operatorsPriority.Add("tan", 4);
                operatorsPriority.Add("cotan", 4);
                operatorsPriority.Add("ctan", 4);
                operatorsPriority.Add("log", 4);
                operatorsPriority.Add("ln", 4);

                operatorsPriority.Add("(", 0);
                operatorsPriority.Add(")", 0);
            }

        }


        public static bool IsOperator(string str)
        {
            InitOperators();
            if (operatorsPriority.ContainsKey(str)) return true;
            else return false;
        }
    }
}
