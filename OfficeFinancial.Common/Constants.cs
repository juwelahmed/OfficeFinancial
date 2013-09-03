using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFinancial.Common
{
    public class Constants
    {
        /// <summary>
        /// LogFile category for logging.
        /// </summary>
        public static readonly string LOGGING_CATEGORY_LOG_FILE = "LogFile";
        /// <summary>
        /// LogFile category for logging.
        /// </summary>
        public static readonly string LOGGING_CATEGORY_DEV = "Dev";
        /// <summary>
        /// LogFile category for logging.
        /// </summary>
        public static readonly string LOGGING_CATEGORY_PRODUCTION = "Production";

        /// <summary>
        /// LogFile category for logging.
        /// </summary>
        public static readonly string LOGGING_CATEGORY_EXCEPTION = "ExceptionHandling";

        public enum Module
        {
            Security=1,
            Company,
            Accounting,
            Sale,
            Purchase,
            Inventory,
            Dimention
        }
    }
}
