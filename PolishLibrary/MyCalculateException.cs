using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PolishLibrary
{
    public class MyCalculateException : Exception
    {
        public MyCalculateException()
            : base()
        {
        }

        public MyCalculateException(string message)
            : base(message)
        {
        }
    }
}
