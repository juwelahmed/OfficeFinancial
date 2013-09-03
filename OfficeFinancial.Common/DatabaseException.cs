using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFinancial.Common
{
    public class DatabaseException : ApplicationException
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

        public DatabaseException()
        {
        }

        public DatabaseException(string message)
            : base(message)
        {

        }

        public DatabaseException(string message, Type exceptionType)
            : base(message)
        {
            _exceptionType = exceptionType;
        }

        public DatabaseException(string message, string code)
            : base(message)
        {
            _code = code;
        }

        public DatabaseException(string message, Exception inner)
            : base(message, inner)
        {

        }

        public DatabaseException(string message, string code, Exception inner)
            : base(message, inner)
        {
            _code = code;
        }
    }
}
