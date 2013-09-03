using System;

namespace OfficeFinancial.Common
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class IdentityAttribute : Attribute
    {
        private readonly string _positionalString;

        
        public string PositionalString 
        { 
            get
            {
                return _positionalString;
            }
        }
        

        // This is a named argument
        public int NamedInt { get; set; }
    }

}
