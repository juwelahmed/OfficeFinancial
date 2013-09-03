using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OfficeFinancial.Entities.AccountingEntities;
using OfficeFinancialUI.Infrastracture;
using OfficeFinancial.Business;
using OfficeFinancial.Common.Dto.Accounting;
using OfficeFinancial.Common;

namespace OfficeFinancialUI.View.Company
{
    /// <summary>
    /// Interaction logic for ChartOfAccount.xaml
    /// </summary>
    public partial class ChartOfAccount : UserControl, ICommandServiceForUI
    {
        private int _selectedAccountCode = -1;
        private bool _addNewMode;

        private readonly IEnumerable<tTaxSetup> _listTaxSetup;

        public ChartOfAccount()
        {
            InitializeComponent();

            try
            {

                _listTaxSetup = AccountingService.GetAllTaxes();

                LoadAccountTypeCombo();
                LoadTaxCombo();
                LoadChartOfAccount();

                ClearFields();

                _addNewMode = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Private method

        private bool ClearFields()
        {
            _selectedAccountCode = -1;
            txtAccountCode.Text = "";
            txtAccountText.Text = "";
            cboAccountType.SelectedIndex = -1;
            cboSummaryFrom.SelectedIndex = -1;
            cboTax.SelectedIndex = -1;
            _addNewMode = true;
            txtAccountCode.IsEnabled = true;
            return true;

        }

        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(txtAccountCode.Text.Trim()))
            {
                MessageBox.Show("Mandatory can not be empty.Please check...");
                txtAccountCode.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAccountText.Text.Trim()))
            {
                MessageBox.Show("Mandatory can not be empty.Please check...");
                txtAccountText.Focus();
                return false;
            }

            if (cboAccountType.SelectedIndex == -1)
            {
                MessageBox.Show("Mandatory can not be empty.Please check...");
                cboAccountType.Focus();
                return false;
            }


            return true;
        }

        private tChartOfAccount ChartOfAccountBuilder()
        {
            var chartOfAccount = new tChartOfAccount
            {
                CompanyId = ShatedData.ApplicationState.SelectedCompanyId ,
                AccountCode = Convert.ToInt16(txtAccountCode.Text),
                AccountName = txtAccountText.Text,
                AccountType = cboAccountType.SelectedIndex,

            };
            if (cboSummaryFrom.SelectedValue != null)
                if (cboSummaryFrom.SelectedIndex != 0)
                {
                    var findIndex = cboSummaryFrom.SelectedValue.ToString().IndexOf("|", 0);
                    if (findIndex != -1)
                    {
                        int accountCode = Convert.ToInt16(cboSummaryFrom.SelectedValue.ToString().Substring(0, findIndex));
                        chartOfAccount.SumFrom = accountCode;
                    }
                }

            if (cboTax.SelectedValue != null)
                if (cboTax.SelectedIndex != 0)
                {
                    var findIndex = cboTax.SelectedValue.ToString().IndexOf("|", 0);
                    if (findIndex != -1)
                    {
                        string taxCode = Convert.ToString(cboTax.SelectedValue.ToString().Substring(0, findIndex));
                        chartOfAccount.TaxCode = taxCode;
                    }
                }

            return chartOfAccount;
        }

        private bool SaveData()
        {
            if (!ValidateData()) return false;

            var chartOfAccount = ChartOfAccountBuilder();

            if (_addNewMode)

                AccountingService.AddAccount(chartOfAccount);
            else
                AccountingService.UpdateAccount(chartOfAccount);

            LoadChartOfAccount();
            
            ClearFields();

            return true;
        }

        private bool DeleteData()
        {
            if (_selectedAccountCode == -1) return false;

            var dialogResult = MessageBox.Show("Delete this Account", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (dialogResult != MessageBoxResult.Yes) return false;
            try
            {
                if (AccountingService.DeleteAccount(new tChartOfAccount { CompanyId = ShatedData.ApplicationState.SelectedCompanyId,AccountCode=_selectedAccountCode }) == 0) return false;

                _selectedAccountCode = -1;

                LoadChartOfAccount();

                ClearFields();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadChartOfAccount()
        {
            grid.ItemsSource = null;

            var listchartOfAccount = AccountingService.GetAllChartOfAccount();

            var listchartOfAccountDto = new List<ChartOfAccountDto>();

            var listchartOfAccountSumFromDto = new List<string>();
            listchartOfAccountSumFromDto.Insert(0, "");

            foreach (var item in listchartOfAccount)
            {
                var chartOfAccountDto = new ChartOfAccountDto();
                chartOfAccountDto.ID = item.CompanyId;
                chartOfAccountDto.AccountCode = item.AccountCode;
                chartOfAccountDto.AccountName = item.AccountName;
                chartOfAccountDto.AccountTypeID = item.AccountType;
                chartOfAccountDto.AccountType = cboAccountType.Items[item.AccountType].ToString();
                chartOfAccountDto.SumFrom = item.SumFrom;
                chartOfAccountDto.TaxCode = item.TaxCode;
                listchartOfAccountDto.Add(chartOfAccountDto);

                listchartOfAccountSumFromDto.Add(item.AccountCode.ToString() + " | " + item.AccountName);

            }

            grid.ItemsSource = listchartOfAccountDto;

            cboSummaryFrom.ItemsSource = listchartOfAccountSumFromDto;

        }


        private void LoadAccountTypeCombo()
        {
            //cboAccountType.DataSource = Enum.GetValues(typeof(Constants.AccountType)).;
            cboAccountType.Items.Add("Income/Expense");
            cboAccountType.Items.Add("Balance");
            cboAccountType.Items.Add("Total");
            cboAccountType.Items.Add("Header");
            cboAccountType.Items.Add("Title");
            cboAccountType.Items.Add("NewPage");
        }

        private void LoadTaxCombo()
        {
            var listTax = new List<string>();

            listTax.Insert(0, "");

            foreach (var item in _listTaxSetup)
            {
                var tax = item.TaxCode + " | " + item.Description;
                listTax.Add(tax);
            }
            cboTax.ItemsSource = listTax;

        }

        private void UpdateFields(ChartOfAccountDto chartAccountDto)
        {
            _selectedAccountCode = chartAccountDto.ID;
            txtAccountCode.Text = chartAccountDto.AccountCode.ToString();
            txtAccountText.Text = chartAccountDto.AccountName;
            cboAccountType.SelectedItem = chartAccountDto.AccountType; //Enum.Parse(typeof(Constants.AccountType), datarow.Cells[(int)GridCols.AccountType].Value.ToString());

            cboSummaryFrom.SelectedIndex = -1;
            if (chartAccountDto.SumFrom.HasValue)
            {
                for (int i = 0; i < cboSummaryFrom.Items.Count; i++)
                {
                    var item = cboSummaryFrom.Items[i] as string;
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        var findIndex = item.IndexOf("|", 0);
                        if (findIndex != -1)
                        {
                            int accountCode = Convert.ToInt16(item.Substring(0, findIndex));
                            if (accountCode == chartAccountDto.SumFrom.Value)
                            {
                                cboSummaryFrom.SelectedItem = item;
                                break;
                            }
                        }
                    }
                }

            }

            cboTax.SelectedIndex = -1;
            if (string.IsNullOrWhiteSpace(chartAccountDto.TaxCode)) return;
            foreach (var t in cboTax.Items)
            {
                var item = t as string;
                if (string.IsNullOrWhiteSpace(item)) continue;
                if (item.Substring(0, 3) != chartAccountDto.TaxCode.Trim()) continue;
                cboTax.SelectedItem = item;
                break;
            }
        }

        #endregion

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

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.AddedItems != null)
            {
               
                if (e.AddedItems.Count != 0)
                {
                    var seletectedItem = e.AddedItems[0] as ChartOfAccountDto;
                    if (seletectedItem != null)
                    {
                        UpdateFields(seletectedItem);
                        txtAccountCode.IsEnabled = false;
                        _addNewMode = false;
                    }
                }
            }
        }

        private void txtAccountCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = ValidationHelper.IsTextNumeric(e.Text);
        }
    }
}
