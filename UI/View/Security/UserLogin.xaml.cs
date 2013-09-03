using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Configuration;
using System.Diagnostics;


using OfficeFinancialUI.Model;
using OfficeFinancialUI.ViewModel;

using OfficeFinancialUI.ShatedData;
using BusinessService=OfficeFinancial.Business;

namespace OfficeFinancialUI.View.Security
{
    /// <summary>
    /// Interaction logic for UserLogin.xaml
    /// </summary>
    public partial class UserLogin : Window
    {
        #region Fields

        private LanguageViewModel languageViewModel;
        private bool bLoginSuccess = false;

        #endregion

        public bool LoginSuccess
        {
            get { return bLoginSuccess; }
        }

        public UserLogin()
        {
            InitializeComponent();


            LanguageComboBox.DataContext = ApplicationState.LanguageViewModel;

            string languageCode = Thread.CurrentThread.CurrentCulture.ToString();

            LanguageComboBox.SelectedValue = languageCode;
        }



        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedLanguage = LanguageComboBox.SelectedValue as Languages;

            if (selectedLanguage != null)
                OfficeFinancialUI.App.SetLanguageDictionary(selectedLanguage.Code);
        }

        private void CheckAllowMultipleInstances()
        {
            bool allowMultipleInstances = false;//ApplicationState.GetParameterValue<bool>(Constants.GENERAL_SETUP_ALLOW_MULTIPLE_INSTANCES);
            if (!allowMultipleInstances)
            {
                // Get Reference to the current Process
                Process thisProc = Process.GetCurrentProcess();
                Process[] running = Process.GetProcessesByName(thisProc.ProcessName);

                // Check how many total processes have the same name as the current one
                if (running.Length > 1)
                {
                    // If there is more than one, than it is already running.
                    Thread warningThread = new Thread(new ThreadStart(ShowWarning));
                    warningThread.Start();
                    bLoginSuccess = false;
                    this.Close();
                }
            }
        }


        /// <summary>
        /// Shows the warning.
        /// </summary>
        private void ShowWarning()
        {
            MessageBox.Show("Office Financial Application is already running.", "Warning!!");
        }



        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            lblError.Content = "";

            if (String.IsNullOrEmpty(txtUserID.Text.Trim()))
            {
                lblError.Content = "User ID required. Try again.";
                txtUserID.Focus();
            }
            else if (String.IsNullOrEmpty(txtPasswordID.Password))
            {
                lblError.Content = "Password required. Try again.";
                txtPasswordID.Focus();
            }

            var userToLogin =BusinessService.SecurityAdminService.GetUserByID(txtUserID.Text);


            if (userToLogin != null && txtPasswordID.Password.Equals(userToLogin.Password, StringComparison.CurrentCulture))
            {

                ApplicationState.LoginUserName = txtUserID.Text;
                ApplicationState.SelectedLanguage = (LanguageComboBox.SelectedValue as Languages).Code;
                new MainWindow().Show();

                ShatedData.ApplicationState.SelectedAccountingYearId = 201301;
                ShatedData.ApplicationState.SelectedCompanyId = 1;
                ShatedData.ApplicationState.SelectedCompanyCurrency = "BDT";
                bLoginSuccess = true;
            }

            else
            {
                lblError.Content = "Invalid credential. Try again.";
            }

            if (bLoginSuccess)
                this.Close();

        }



        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
