using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1Classes
{
    public class ExceptionComparation : Exception
    {
        public ExceptionComparation(string message)
           : base(message)
        { }
    }


    public class ExceptionAlreadyExist : Exception
    {
        public ExceptionAlreadyExist(string message)
            : base(message)
        { }

    }


    public class ExceptionDoesNotExist : Exception
    {
        public ExceptionDoesNotExist(string message)
            :base(message)
        { }
    }
}

