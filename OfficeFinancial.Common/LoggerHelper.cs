using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.ExtraInformation;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;

using Microsoft.Practices.EnterpriseLibrary.Logging.ExtraInformation;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;

namespace OfficeFinancial.Common
{

    /// <summary>
    /// It will check Filter Status Before Constructing Log Messages and can
    /// populating a Log Message with Additional Context Information.  
    /// The application can avoid the performance penalty of collecting information 
    /// for an event that will not be logged.
    /// </summary>
    public class LoggerHelper
    {
        static LoggerHelper()
        {
            Logger.SetLogWriter(new LogWriterFactory().Create());
        }

        /// <summary>
        /// It checks Filter Status Before constructing Log Messages and
        /// writes the specified category.
        /// </summary>
        /// <param name="priority">The priority.</param>
        /// <param name="category">The category.</param>
        public static void Write(TraceEventType eventType, string message, string category)
        {
            Write(eventType, message, new[] { category });
        }

        /// <summary>
        /// It checks Filter Status Before constructing Log Messages and
        /// writes the specified category.
        /// </summary>
        /// <param name="priority">The priority.</param>
        /// <param name="category">The category.</param>
        public static void Write(TraceEventType eventType, string message, Exception ex, string category)
        {
            LoggerHelper.Write(eventType, message, ex, new[] { category });
        }

        /// <summary>
        /// It checks Filter Status Before constructing Log Messages and
        /// writes the specified category.
        /// </summary>
        /// <param name="priority">The priority.</param>
        /// <param name="category">The categories.</param>
        public static void Write(TraceEventType eventType, string message, params string[] categories)
        {
            LoggerHelper.Write(eventType, message, new Exception(), categories);
        }

        /// <summary>
        /// It checks Filter Status Before constructing Log Messages and
        /// writes the specified category.
        /// </summary>
        /// <param name="priority">The priority.</param>
        /// <param name="category">The categories.</param>
        public static void Write(TraceEventType eventType, string message, Exception ex, params string[] categories)
        {
            var logEntry = new LogEntry {Severity = eventType};

            if (!String.IsNullOrEmpty(ex.Message) && !String.IsNullOrEmpty(ex.StackTrace))
                message = message + "\n" + ex.GetType().Name + "\nException Message: " + ex.Message + "\nException Trace: " + ex.StackTrace;
            if (ex != null && ex.InnerException != null && !String.IsNullOrEmpty(ex.InnerException.Message))
                message = message + "\nInner Exception Message: " + ex.InnerException.Message;
            logEntry.Message = message;
            logEntry.Categories = categories;

          
            // Event will be logged according to currently configured filters.
            // Perform operations (possibly expensive) to gather additional information 
            // for the event to be logged. Otherwise, event will not be logged.
            //if (Logger.GetFilter<CategoryFilter>().ShouldLog(categories))
            //{
                Logger.Write(logEntry);
            //}
        }

        /// <summary>
        /// It checks Filter Status Before constructing Log Messages and
        /// writes the specified category by populating a Log Message with 
        /// Additional Context Information.
        /// </summary>
        /// <param name="priority">The priority.</param>
        /// <param name="category">The category.</param>
        public static void Write(TraceEventType eventType, string message, Dictionary<string, object> dictionary,
            string category)
        {
            Write(eventType, message, dictionary, new[] { category });
        }

        /// <summary>
        /// It checks Filter Status Before constructing Log Messages and
        /// writes the specified category by populating a Log Message with 
        /// Additional Context Information.
        /// </summary>
        /// <param name="priority">The priority.</param>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="categories">The categories.</param>
        public static void Write(TraceEventType eventType, string message, Dictionary<string, object> dictionary,
            params string[] categories)
        {
            var logEntry = new LogEntry {Severity = eventType, Message = message, Categories = categories};

            // Event will be logged according to currently configured filters.
            // Perform operations (possibly expensive) to gather additional information 
            // for the event to be logged. Otherwise, event will not be logged.
            //if (Logger.GetFilter<CategoryFilter>().ShouldLog(categories))
            //{
                var informationHelper =
                    new ManagedSecurityContextInformationProvider();
                informationHelper.PopulateDictionary(dictionary);
                logEntry.ExtendedProperties = dictionary;
                Logger.Write(logEntry);
            //}
        }
    }

}
