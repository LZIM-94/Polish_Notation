using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PolishLibrary
{
   public class Parser
    {
        string inputSring;

        public string InputSring
        {
            get { return inputSring; }
            set { inputSring = value; }
        }
        List<string> outputString;

        public List<string> OutputString
        {
            get { return outputString; }
            set { outputString = value; }
        }

        public Parser(string str)
        {
            InputSring = str;
            OutputString = new List<string>();
           
        }

        public Parser()
        {
            InputSring = "";
            OutputString = new List<string>();
           
        }


      


        string TakeDigit(string str) //Берет число в начале строки
        {
            string number = "";

            Regex reg = new Regex(@"^\d+(\,\d+)?");//Поиск числа с начала строки

            number = reg.Match(str).Value;
            if (number == "")
            {
                throw new MyParserException(" Одно из чисел имеет неверный формат записи"); 
            }
           

            return number;
        }


        string TakeOperator(string str)
        {
            string oper = "";

            Regex reg = new Regex(@"^(\+|\-|\*|\/|\^|\(|\)|sin|cos|tan|c(o)?tan|ln|log)");

            oper = reg.Match(str).Value;
            if (oper == "") throw new MyParserException("Строку \"" + str + "\" не получается распознать как арефметическое выражение");




            return oper;
        }

        List<string> PreParse(string str1)
        {
            List<string> buffList = new List<string>();
            string str = str1;
           
            Regex digit = new Regex(@"^(\d|\,)"); //регулярное выражение проверяющее символ на цифру 
            str = str.Replace(" ","");//убираем пробелы
            str = str.ToLower();//переводим в нижний регистр
            str = str.Replace(".",",");


            try
            {
                while (str.Length != 0)
                {
                    if (digit.IsMatch(str))
                    {
                        string number = TakeDigit(str);
                        str = str.Remove(0, number.Length);
                       
                       // if (number[0] == '.') number = '0' + number; //если перед или после точки нет числа, допишем туда ноль
                       // else if (number.Last() == '.') number += '0';

                        buffList.Add(number);
                    }
                    else
                    {
                        string oper = TakeOperator(str);
                        str = str.Remove(0, oper.Length);
                        buffList.Add(oper);
                    }
                }
                return buffList;
            }
            catch(MyParserException ex)
            {  
                buffList.Clear();
                throw new MyParserException("Ошибка при разборе строки: "+ex.Message);
            }
        }

      public List<string> Parse(string str) //Перевод в постфиксную запись
       {
           try
           {
           List<string> TokensList = PreParse(str);
           List<string> ResultList = new List<string>();
           Stack<string> stack = new Stack<string>();
          
               foreach (string elem in TokensList)
               {
                   if (!Operators.IsOperator(elem)) //если число добавляем в результирующий список
                   {
                       ResultList.Add(elem);
                   }
                   else //если не число
                   {
                       if (elem == "(") stack.Push(elem);//открывающую скобку кладем в стек
                       else if (elem == ")")
                       {
                           while (stack.Peek() != "(") { ResultList.Add(stack.Pop()); }//выгружаем пока не дошли до открывающей скобки
                           stack.Pop(); //удалим из стека знак открывающей скобки
                           if (stack.Count != 0 && Operators.IsOperator(stack.Peek())) ResultList.Add(stack.Pop());//если после скобки был оператор то кладем его в результ список
                       }
                       else//если оператор
                       {
                           while (stack.Count != 0 && Operators.OperatorsPriority[stack.Peek()] >= Operators.OperatorsPriority[elem])//пока стек не пуст и приоритет оператора в стеке больше или равен текущему - выгружаем 
                           {
                               ResultList.Add(stack.Pop());
                           }
                           stack.Push(elem);//кладем в стек оператор
                       }
                   }
               }
               while (stack.Count != 0) //если в стеке что-то осталось выгружаем в результирующий список
               {
                   ResultList.Add(stack.Pop());
               }
               OutputString = ResultList;
               return ResultList;
           }
           catch (MyParserException ex)
           {
               throw;
           }


       }

       public List<string> Parse()
       {
           return Parse(InputSring);
       }

    }
}
