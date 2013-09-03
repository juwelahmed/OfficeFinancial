using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OfficeFinancial.Business;
using OfficeFinancial.Common.Dto.Accounting;
using OfficeFinancial.Entities.AccountingEntities;
using OfficeFinancialUI.Infrastracture;
using OfficeFinancialUI.ShatedData;
using OfficeFinancial.Common;

namespace OfficeFinancialUI.View.Company
{
    /// <summary>
    /// Interaction logic for VoucherEntry.xaml
    /// </summary>
    public partial class VoucherEntry : UserControl, ICommandServiceForUI
    {
        #region Private Variable
        private IEnumerable<AccountingTransactionTempDto> _tVoucherTemplates = new List<AccountingTransactionTempDto>();
        private bool _isNewRecord = true;
        private static VoucherTemplateDto _template;

        #endregion

        public static VoucherTemplateDto Template
        {
            get { return _template; }
            set { _template = value; }
        }

        public VoucherEntry()
        {
            InitializeComponent();

            GenerateAccountCombo();
            GenerateCurrencyCombo();

            InitGrid();
        }

        private void GenerateAccountCombo()
        {
            var accounts = (List<tChartOfAccount>)AccountingService.GetAllChartOfAccount();
            var source = accounts.Select(a => new OfficeFinancial.Common.ComboBoxItem { Key = a.AccountCode.ToString(), Value = a.AccountCode + " | " + a.AccountName }).ToList();
            //source.Insert(0,null);

            cboCreditAccount.DisplayMemberPath = "Value";
            cboCreditAccount.SelectedValuePath = "Key";
            cboCreditAccount.ItemsSource = source;

            cboDebitAccount.DisplayMemberPath = "Value";
            cboDebitAccount.SelectedValuePath = "Key";
            cboDebitAccount.ItemsSource = source;

        }

        private void GenerateCurrencyCombo()
        {
            var currencies = AccountingService.GetAllCurrencies() as List<tCurrency>;

            if (currencies != null)
            {
                var source = currencies.Select(a => new OfficeFinancial.Common.ComboBoxItem { Key = a.Code, Value = a.Code}).ToList();

                cboCurrency.DisplayMemberPath = "Value";
                cboCurrency.SelectedValuePath = "Key";
                cboCurrency.ItemsSource = source;

            }
        }

        private bool IsValidData()
        {
            
            if (cboDebitAccount.SelectedIndex == -1)
            {
                MessageBox.Show("Debit Account can not be empty.", "Data Validation", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                cboDebitAccount.Focus();
                return false;
            }
            if (cboCreditAccount.SelectedIndex == -1)
            {
                MessageBox.Show("Credit Account can not be empty.", "Data Validation", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                cboCreditAccount.Focus();
                return false;
            }
            if (cboCurrency.SelectedIndex == -1)
            {
                MessageBox.Show("Currency can not be empty.", "Data Validation", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                cboCurrency.Focus();
                return false;
            }
            if (String.IsNullOrWhiteSpace(txtTotal.Text))
            {
                MessageBox.Show("Total amount can not be empty.", "Data Validation", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                txtTotal.Focus();
                return false;
            }
            if (String.IsNullOrWhiteSpace(txtValueTotal.Text))
            {
                MessageBox.Show("Value total can not be empty.", "Data Validation", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                txtValueTotal.Focus();
                return false;
            }
            
            DateTime date;
            if(!DateTime.TryParse(dtpVoucherDate.Text,out date))
            {
                MessageBox.Show("Please provide a valid voucher date.", "Data Validation", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                dtpVoucherDate.Focus();
                return false;
            }

            return true;

        }

        private void InitGrid()
        {
            _tVoucherTemplates = AccountingService.GetAllUnpostedVoucher();

            grid.ItemsSource = _tVoucherTemplates;

        }

        private bool Save()
        {
            if (!IsValidData())
                return false;

            var vouchers = GetVouchers();

            bool isAdded;
            if (_isNewRecord)
            {
                isAdded = AccountingService.AddUnpostedVoucher(vouchers);
            }
            else
            {
                isAdded = AccountingService.UpdateUnpostedVoucher(vouchers);
            }

            ClearFields();

            _isNewRecord = true;
            InitGrid();

            return isAdded;
        }

        private AccountingTransactionTempDto GetVouchers()
        {
            return new AccountingTransactionTempDto
                {
                    CompanyId = ApplicationState.SelectedCompanyId,
                    CreditAccount = Convert.ToInt32(cboCreditAccount.SelectedValue),
                    DebitAccount = Convert.ToInt32(cboDebitAccount.SelectedValue),
                    AccountingYearId = ApplicationState.SelectedAccountingYearId,
                    CurrencyCode = cboCurrency.SelectedValue.ToString(),
                    Total = Convert.ToDecimal(txtTotal.Text),
                    UserId = ApplicationState.LoginUserName,
                    ValueTotal = Convert.ToDecimal(txtValueTotal.Text),
                    VoucherDate = Convert.ToDateTime(dtpVoucherDate.Text),
                    LineId = _isNewRecord ? 0 : Convert.ToInt64(txtLineId.Text),
                };
        }

        private bool ClearFields()
        {
            txtShortCode.Text = String.Empty;
            cboDebitAccount.SelectedIndex = -1;
            cboCreditAccount.SelectedIndex = -1;
            cboCurrency.SelectedIndex = -1;
            txtTotal.Text = String.Empty;
            txtValueTotal.Text = String.Empty;
            dtpVoucherDate.Text = String.Empty;

            _isNewRecord = true;

            return true;
        }

        private bool Delete()
        {
            if (!String.IsNullOrWhiteSpace(txtLineId.Text))
            {
                try
                {
                    var result = AccountingService.DeleteUnpostedVoucher(GetVouchers());

                    ClearFields();
                    InitGrid();
                    _isNewRecord = true;

                    return result;

                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }
            }

            return false;
        }

        private void UpdateFields(AccountingTransactionTempDto template)
        {
            cboDebitAccount.SelectedValue = template.DebitAccount;
            cboCreditAccount.SelectedValue = template.CreditAccount;
            cboCurrency.SelectedValue = template.CurrencyCode;
            txtTotal.Text = template.Total.ToString();
            txtValueTotal.Text = template.ValueTotal.ToString();
            dtpVoucherDate.Text = template.VoucherDate.ToShortDateString();
            txtLineId.Text = template.LineId.ToString();
        }
        #region Event Handler
        private void GridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems != null)
                {

                    if (e.AddedItems.Count != 0)
                    {
                        var seletectedItem = e.AddedItems[0] as AccountingTransactionTempDto;
                        if (seletectedItem != null)
                        {
                            UpdateFields(seletectedItem);
                        }
                    }

                    _isNewRecord = false;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "System Message", MessageBoxButton.OK,
                                   MessageBoxImage.Error);
            }
        }

        private void TxtTaxRatePreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = ValidationHelper.IsTextNumeric(e.Text);
        }

        #endregion


        #region Command
        public Func<bool> AddNewCommand
        {
            get { return ClearFields; }
        }

        public Func<bool> SaveCommand
        {
            get { return Save; }
        }

        public Func<bool> DeleteCommand
        {
            get { return Delete; }
        }
        public Func<string, bool> ExportToCSVCommand { get; private set; }
        public Func<string, bool> ExportToEXCELCommand { get; private set; }
        public Func<string, bool> ExportToPDFCommand { get; private set; } 
        #endregion

        private void BtnSearchClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var search = new VoucherTemplateSearch(ref txtShortCode, ref cboDebitAccount, ref cboCreditAccount);

                search.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "System Message", MessageBoxButton.OK,
                                   MessageBoxImage.Error);
            }
        }

        private void TxtShortCodeLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                var template = AccountingService.GetlVoucherTemplateByCode(txtShortCode.Text,
                                                                           ApplicationState.SelectedCompanyId);
                if (template != null)
                {
                    cboDebitAccount.SelectedValue = template.DebitAccountCode;
                    cboCreditAccount.SelectedValue = template.CreditAccountCode;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "System Message", MessageBoxButton.OK,
                                   MessageBoxImage.Error);
            }
        }
    }
}
