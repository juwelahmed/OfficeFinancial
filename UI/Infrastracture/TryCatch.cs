using System;
using System.Diagnostics;
using System.Windows;
using OfficeFinancial.Common;

namespace OfficeFinancialUI.Infrastracture
{
    public class TryCatch
    {
        //public static void BeginTryCatch(Action<CommandArgs> function, CommandArgs obj)
        //{
        //    try
        //    {
        //        function.Invoke(obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}
        public static void BeginTryCatch(Action<object> function, object obj)
        {
            try
            {
                function.Invoke(obj);
            }
            catch (Exception ex)
            {
                LoggerHelper.Write(TraceEventType.Error, "Error in  BeginTryCatch in TryCatch. " + ex,
                    new[] { Constants.LOGGING_CATEGORY_EXCEPTION });
                //MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("Oops!! Try again later.", "Error in  Processing");
            }
        }
        public static void BeginTryCatch(Action function)
        {
            try
            {
                function.Invoke();
            }
            catch (Exception ex)
            {
                LoggerHelper.Write(TraceEventType.Error, "Error in  BeginTryCatch in TryCatch. " + ex,
                   new[] { Constants.LOGGING_CATEGORY_EXCEPTION });
                //MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("Oops!! Try again later.", "Error in  Processing");
                //You can log error here for e.g you can use log4net
            }
        }
    }
}
