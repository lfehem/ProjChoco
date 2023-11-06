using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjChoco.Models
{
    // cf cours
    public class ExceptionClass : Exception
    {
        string _message;

        public ExceptionClass(string msg)
        {
            _message = msg;

        }

        public override string ToString()
        {
            return _message;
        }
    }
}
