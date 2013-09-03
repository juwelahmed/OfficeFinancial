using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OfficeFinancial.Business;
using OfficeFinancial.Common;
using OfficeFinancial.Entities.AccountingEntities;
using OfficeFinancialUI.Infrastracture;

namespace OfficeFinancialUI.View.Accounting
{
    /// <summary>
    /// Interaction logic for AccountingYearSetup.xaml
    /// </summary>
    public partial class AccountingYearSetup : UserControl, ICommandServiceForUI
    {
        private bool _addNewMode = true;
        private Int64 _selectedYeareId = -1;

        public AccountingYearSetup()
        {
            InitializeComponent();

            try
            {
                LoadMonthCombo();
                LoadAccountingYearData();
                ClearFields();

                //_addNewMode = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }



        #region  ICommandServiceForUI Implementation

        public Func<bool> AddNewCommand
        {
            get { return ClearFields; }
        }


        public Func<bool> SaveCommand
        {
            get { return SaveData; }
        }


        public Func<bool> DeleteCommand
        {
            get { return DeleteData; }
        }



        public Func<string, bool> ExportToCSVCommand
        {
            get
            {
                return exportFilePath => { grid.ExportToCsv("Chart Of Accounts", exportFilePath, true); return true; };
            }
        }

        public Func<string, bool> ExportToEXCELCommand
        {
            get
            {
                return exportFilePath => { grid.ExportToExcel("Chart Of Accounts", exportFilePath, true); return true; };
            }
        }

        public Func<string, bool> ExportToPDFCommand
        {
            get
            {
                return exportFilePath => { grid.ExportToPdf("Chart Of Accounts", exportFilePath, true); return true; };
            }
        }

        #endregion

        #region Private Method

        private bool ClearFields()
        {
            cboStartFromMonth.SelectedIndex = -1;
            cboEndFromMonth.SelectedIndex = -1;
            txtAccountingYear.Text = "";
            dpkFirstPostingDate.Text = "";
            dpkLastPostingDate.Text = "";
            txtFirstVoucherNo.Text = "";
            txtLastVoucherNo.Text = "";
            chkLocked.IsChecked = false;

            _addNewMode = true;

            return true;
        }

        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(txtAccountingYear.Text.Trim()))
            {
                MessageBox.Show("Mandatory can not be empty.Please check...");
                txtAccountingYear.Focus();
                return false;
            }

            if (cboStartFromMonth.SelectedIndex == -1)
            {
                MessageBox.Show("Mandatory can not be empty.Please check...");
                cboStartFromMonth.Focus();
                return false;
            }

            if (cboEndFromMonth.SelectedIndex == -1)
            {
                MessageBox.Show("Mandatory can not be empty.Please check...");
                cboEndFromMonth.Focus();
                return false;
            }
            if (cboStartFromMonth.SelectedIndex <= cboEndFromMonth.SelectedIndex)
            {
                MessageBox.Show("Invalid month selection.Please check...");
                cboEndFromMonth.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFirstVoucherNo.Text.Trim()))
            {
                MessageBox.Show("Mandatory can not be empty.Please check...");
                txtFirstVoucherNo.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtLastVoucherNo.Text.Trim()))
            {
                MessageBox.Show("Mandatory can not be empty.Please check...");
                txtLastVoucherNo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(dpkFirstPostingDate.Text.Trim()))
            {
                MessageBox.Show("Mandatory can not be empty.Please check...");
                dpkFirstPostingDate.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(dpkLastPostingDate.Text.Trim()))
            {
                MessageBox.Show("Mandatory can not be empty.Please check...");
                txtLastVoucherNo.Focus();
                return false;
            }

            return true;
        }

        private tAccountingYear AccountingYearBuilder()
        {
            var chartOfAccount = new tAccountingYear
            {
                CompanyId = ShatedData.ApplicationState.SelectedCompanyId,
                AccountYearId = Convert.ToInt64(txtAccountingYear.Text),
                StartMonth = cboStartFromMonth.SelectedIndex + 1,
                EndMonth = cboEndFromMonth.SelectedIndex + 1,
                StartVoucherNo = Convert.ToInt64(txtFirstVoucherNo.Text),
                EndVoucherNo = Convert.ToInt64(txtLastVoucherNo.Text),
                StartPostingDate = DateTime.Parse(dpkFirstPostingDate.Text),
                EndPostingDate = DateTime.Parse(dpkLastPostingDate.Text),
                IsLocked = chkLocked.IsChecked == true,
                EntryUser = ShatedData.ApplicationState.LoginUserName,
                SetDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };


            return chartOfAccount;
        }

        private bool SaveData()
        {
            if (!ValidateData()) return false;

            var accountingYear = AccountingYearBuilder();
            try
            {

                if (_addNewMode)

                    AccountingService.AddAccountingYear(accountingYear);
                else
                    AccountingService.UpdateAccountingYear(accountingYear);

                LoadAccountingYearData();

                ClearFields();
            }
            catch (Exception ex)
            {
                //LoggerHelper.Write(TraceEventType.Error, "Error in  SaveData in Accounting year setup. " + ex,
                //    new[] { Constants.LOGGING_CATEGORY_EXCEPTION });
                //MessageBox.Show("Oops!! Try again later.", "Error in  Processing");
                throw ex;
            }

            return true;
        }

        private bool DeleteData()
        {
            if (_selectedYeareId == -1) return false;

            var dialogResult = MessageBox.Show("Delete this Account", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (dialogResult != MessageBoxResult.Yes) return false;
            try
            {
                if (AccountingService.DeleteAccountingYear(new tAccountingYear() { AccountYearId = _selectedYeareId,CompanyId=ShatedData.ApplicationState.SelectedCompanyId }) == 0) return false;

                _selectedYeareId = -1;

                LoadAccountingYearData();

                ClearFields();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadMonthCombo()
        {
            var startMonths = new[]
                                 {
                                     "January", "February", "March", "April", "May", "Jun", "July", "August", "September",
                                     "October", "November", "December"
                                 };
            var endMonths = new[]
                                 {
                                     "January", "February", "March", "April", "May", "Jun", "July", "August", "September",
                                     "October", "November", "December"
                                 };
            cboStartFromMonth.ItemsSource = startMonths;
            cboEndFromMonth.ItemsSource = endMonths;
        }

        private void LoadAccountingYearData()
        {
            grid.ItemsSource = null;

            var listAccountingYear = AccountingService.GetAllAccountingYears();

            grid.ItemsSource = listAccountingYear;

        }

        private void UpdateFields(tAccountingYear tAccountingYear)
        {
           _selectedYeareId =tAccountingYear.AccountYearId;
            txtAccountingYear.Text = tAccountingYear.AccountYearId.ToString();
            cboStartFromMonth.SelectedIndex = tAccountingYear.StartMonth-1;
            cboEndFromMonth.SelectedIndex = tAccountingYear.EndMonth - 1;
            txtFirstVoucherNo.Text = tAccountingYear.StartVoucherNo.ToString();
            txtLastVoucherNo.Text = tAccountingYear.EndVoucherNo.ToString();
            dpkFirstPostingDate.Text = tAccountingYear.StartPostingDate.ToShortDateString();
            dpkLastPostingDate.Text = tAccountingYear.EndPostingDate.ToShortDateString();
            chkLocked.IsChecked = tAccountingYear.IsLocked;
        } 


        #endregion
        #region Event Handler

        private void txtAccountCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void GridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems != null)
                {

                    if (e.AddedItems.Count != 0)
                    {
                        var seletectedItem = e.AddedItems[0] as tAccountingYear;
                        if (seletectedItem != null)
                        {
                            UpdateFields(seletectedItem);
                        }
                    }

                    _addNewMode = false;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "System Message", MessageBoxButton.OK,
                                   MessageBoxImage.Error);
            }

        }

        #endregion

    }
}
