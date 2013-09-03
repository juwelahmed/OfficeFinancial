using System;
using System.Windows;
using OfficeFinancialUI.Infrastracture;
using OfficeFinancialUI.ShatedData;
using OfficeFinancial.Business;

namespace OfficeFinancialUI.View.Security
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : ICommandServiceForUI
    {
        

        public ChangePassword()
        {
            InitializeComponent();

            //base.SaveCommand = SaveData;


            txtUserId.Text = ApplicationState.LoginUserName;

        }



        #region private method


        #endregion


        public bool SaveData()
        {
            if (!ValidateData()) return false;

            bool result = SecurityAdminService.ChangePassword(txtUserId.Text, txtOldPassword.Password, txtNewPassword.Password);


            return result;
        }



        //public Func<bool> AddNewCommand { get; set; }

        //public Func<bool> SaveCommand { get; set; }

        //public Func<bool> DeleteCommand { get; set; }

        #region Private method

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(txtOldPassword.Password))
            {
                MessageBox.Show("Old password can not be empty.");
                txtOldPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNewPassword.Password))
            {
                MessageBox.Show("New password can not be empty.");
                txtOldPassword.Focus();
                return false;
            }

            return true;


        }

        #endregion



        public  Func<bool> AddNewCommand
        {
            get { return null; }
        }

        public  Func<bool> SaveCommand
        {
            get { return SaveData; }
        }

        public  Func<bool> DeleteCommand
        {
            get { return null; }
        }

        public Func<string, bool> ExportToCSVCommand
        {
            get { throw new NotImplementedException(); }
        }

        public Func<string, bool> ExportToEXCELCommand
        {
            get { throw new NotImplementedException(); }
        }

        public Func<string, bool> ExportToPDFCommand
        {
            
             get { throw new NotImplementedException(); }
        }
    }
}
