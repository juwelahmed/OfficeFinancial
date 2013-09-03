using System;
using System.Windows;
using System.Windows.Controls;
using OfficeFinancialUI.Infrastracture;
using OfficeFinancial.Business;
using OfficeFinancial.Entities.SecurityAdminEnities;
namespace OfficeFinancialUI.View.Security
{
    /// <summary>
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList : UserControl, ICommandServiceForUI
    {
        #region Private fields

        private bool _fieldModified = false;
        private bool _addNewMode = false;

        #endregion

        enum GridCols
        {
            UserID = 0,
            Password,
            Notes,
            Language,
            Theme,
            FontName,
            FontSize
        }

        public UserList()
        {
            InitializeComponent();

            try
            {
                LoadData();

                LoadThemeCombo();
                LoadFontNameCombo();
                LoadFontSizeCombo();
            }
            catch (Exception ex)
            {
                //LoggerHelper.Write(TraceEventType.Error, "Error in loading mnuReport_AandF_Click " + ex,
                //new string[] {.LOGGING_CATEGORY_DEV, LOGGING_CATEGORY_PRODUCTION });
                MessageBox.Show("Oops!! Try again later.", "Error in  Processing");
            }
        }

        #region ICommandServiceForUI Implementation

        public Func<bool> AddNewCommand
        {
            get { return null; }
        }

        public Func<bool> SaveCommand
        {
            get { return null; }
        }

        public Func<bool> DeleteCommand
        {
            get { return null; }
        }

        public Func<string, bool> ExportToCSVCommand
        {
            get
            {
                return (exportFilePath) => { grid.ExportToCsv("UserList", exportFilePath, true); return true; };
            }
        }

        public Func<string, bool> ExportToEXCELCommand
        {
            get
            {
                return (exportFilePath) => { grid.ExportToExcel("UserList", exportFilePath, true); return true; };
            }
        }

        public Func<string, bool> ExportToPDFCommand
        {
            get
            {
                return (exportFilePath) => { grid.ExportToPdf("UserList", exportFilePath, true); return true; };
            }
        }

        #endregion


        #region Private Method

        private void LoadData()
        {
            var users = SecurityAdminService.GetAll();
            grid.ItemsSource = users;
        }

        private void LoadThemeCombo()
        {
            cboTheme.ItemsSource = null;

            cboTheme.ItemsSource = OfficeFinancialUI.ShatedData.ApplicationState.Themes;
        }

        private void LoadFontNameCombo()
        {
            cboFont.ItemsSource = OfficeFinancialUI.ShatedData.ApplicationState.Fonts;
        }

        private void LoadFontSizeCombo()
        {
            cboFontSize.ItemsSource = OfficeFinancialUI.ShatedData.ApplicationState.FontSize;
        }

        private void UpdateFields(tUser user)
        {
            txtUserId.Text = user.ID;
            txtPassword.Text = "**********";
            txtNotes.Text = user.Notes;


        }

        #endregion



        #region Event Handler

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems == null) return;
            var selectedUser = e.AddedItems[0] as tUser;
            if (selectedUser != null)
            {

            }
        }

        #endregion



    }
}
