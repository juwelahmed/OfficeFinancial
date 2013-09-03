using System;
using System.Windows;
using System.Windows.Markup;
using System.Globalization;

using OfficeFinancialUI.ViewModel;

using OfficeFinancialUI.View.Security;
using OfficeFinancialUI.Setup;

using OfficeFinancial.Common;

namespace OfficeFinancialUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        LanguageViewModel _languageViewModel;
        static App()
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
            

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            //try
            //{
                SetupHelper.Run();

                _languageViewModel = new LanguageViewModel();

                ShowLoginDialog();

                //SetLanguageDictionary();
            //}
            //catch (Exception ex)
            //{

            //    HandleException(ex);
            //}
           
        }

        public static void SetLanguageDictionary(string languageCode)
        {
            ResourceDictionary dict = new ResourceDictionary();
            switch (languageCode) //(Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "en-US":
                    dict.Source = new Uri("..\\Resources\\EnglishLanguage.xaml", UriKind.Relative);
                    break;
                case "da-DK":
                    dict.Source = new Uri("..\\Resources\\DenishLanguage.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("..\\Resources\\EnglishLanguage.xaml", UriKind.Relative);
                    break;
            }
            //if (App.Current.Resources.MergedDictionaries != null)
            //    App.Current.Resources.MergedDictionaries = null;

            Current.Resources.MergedDictionaries.Add(dict);
            


        }

        void ShowLoginDialog()
        {
            var loginWindow = new UserLogin();

            loginWindow.ShowDialog();

            //if (!loginWindow.LoginSuccess)
            //{
            //    Application.Current.Shutdown();
            //}
            //else
            //{
            //    new MainWindow().ShowDialog();
            //}
        }

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private static void HandleException(Exception ex)
        {
            //if (ex == null) 
            //LoggerHelper.Write(TraceEventType.Error, "General error in UI. " + ex,
            //        new string[] { Ranju.Shared.RanjuConstants.LOGGING_CATEGORY_DEV, Ranju.Shared.RanjuConstants.LOGGING_CATEGORY_PRODUCTION });
            MessageBox.Show("Generic error. contact the administrator.");
            Environment.Exit(1);
        }
    }
}
