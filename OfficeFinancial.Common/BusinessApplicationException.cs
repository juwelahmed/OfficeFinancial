using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFinancial.Common
{
    public class BusinessApplicationException : ApplicationException
    {
        private string _code;
        private Type _exceptionType;


        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }


        public Type ExceptionType
        {
            get { return _exceptionType; }
            set { _exceptionType = value; }
        }

        public BusinessApplicationException()
        {
        }

        public BusinessApplicationException(string message)
            : base(message)
        {

        }

        public BusinessApplicationException(string message, Type exceptionType)
            : base(message)
        {
            _exceptionType = exceptionType;
        }

        public BusinessApplicationException(string message, string code)
            : base(message)
        {
            _code = code;
        }

        public BusinessApplicationException(string message, Exception inner)
            : base(message, inner)
        {

        }

        public BusinessApplicationException(string message, string code, Exception inner)
            : base(message, inner)
        {
            _code = code;
        }
    }
}
