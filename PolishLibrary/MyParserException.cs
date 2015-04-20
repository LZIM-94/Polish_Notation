using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PolishLibrary
{
   public class MyParserException:Exception
    {
       public MyParserException()
           : base()
       {
       }

       public MyParserException(string message)
           : base(message)
       {
       }
    }
}
