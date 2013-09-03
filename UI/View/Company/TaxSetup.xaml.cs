using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OfficeFinancial.Business;
using OfficeFinancial.Common;
using OfficeFinancial.Entities.AccountingEntities;
using OfficeFinancialUI.Infrastracture;

namespace OfficeFinancialUI.View.Company
{
    /// <summary>
    /// Interaction logic for TaxSetup.xaml
    /// </summary>
    public partial class TaxSetup : UserControl, ICommandServiceForUI
    {
        private IEnumerable<tTaxSetup> _taxSetups = new List<tTaxSetup>();
        private bool _isNewRecord = true;

        public TaxSetup()
        {
            InitializeComponent();

            try
            {
                InitGrid();
                LoadChartOfAccountComboBox();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "System Message", MessageBoxButton.OK,
                                   MessageBoxImage.Error);
            }
        }

        #region Private Method

        private void InitGrid()
        {
            _taxSetups = AccountingService.GetAllTaxes();

            grid.ItemsSource = _taxSetups;

        }

        private bool IsValidData()
        {
            if (String.IsNullOrEmpty(txtTaxCode.Text))
            {
                MessageBox.Show("Tax Code can not be empty.", "Data Validation", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                txtTaxCode.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtTaxRate.Text))
            {
                MessageBox.Show("Tax Rate can not be empty.", "Data Validation", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                txtTaxRate.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtTaxDescription.Text))
            {
                MessageBox.Show("Description can not be empty.", "Data Validation", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                txtTaxDescription.Focus();
                return false;
            }

            if (cboAccount.SelectedIndex==-1)
            {
                MessageBox.Show("Tax Account  can not be empty.", "Data Validation", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                cboAccount.Focus();
                return false;
            }
            return true;
        }

        private bool Save()
        {
            if (!IsValidData())
                return false;

            var tax = GetTax();

            bool isAdded;
            if (_isNewRecord)
            {
                isAdded = AccountingService.AddTax(tax);
            }
            else
            {
                isAdded = AccountingService.UpdateTax(tax);
            }

            ClearFields();

            _isNewRecord = true;
            InitGrid();

            return isAdded;
        }

        private tTaxSetup GetTax()
        {
            var taxSetup= new tTaxSetup
                       {
                           CompanyId = ShatedData.ApplicationState.SelectedCompanyId,
                           TaxCode = txtTaxCode.Text,
                           TaxRate = Convert.ToInt16(txtTaxRate.Text),
                           Description = txtTaxDescription.Text//,
                           //AccountCode=cboAccount
                       };

            var findIndex = cboAccount.SelectedValue.ToString().IndexOf("|", 0);
        
            if (findIndex != -1)
               taxSetup.AccountCode =  Convert.ToInt16(cboAccount.SelectedValue.ToString().Substring(0, findIndex));
                

            return taxSetup;
        }

        private bool ClearFields()
        {
            txtTaxCode.Text = String.Empty;
            txtTaxRate.Text = String.Empty;
            txtTaxDescription.Text = String.Empty;
            cboAccount.SelectedIndex = -1;
            return true;
        }

        private bool Delete()
        {
            if (!String.IsNullOrWhiteSpace(txtTaxCode.Text))
            {
                try
                {
                    var result = AccountingService.DeleteTax(GetTax());

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
        private void LoadChartOfAccountComboBox()
        {

            var listchartOfAccount = AccountingService.GetAllChartOfAccount();

            //var listchartOfAccountDto = new List<ChartOfAccountDto>();

            var listchartOfAccountDto = new List<string>();
            listchartOfAccountDto.Insert(0, "");

            foreach (var item in listchartOfAccount)
            {
                //var chartOfAccountDto = new ChartOfAccountDto();
                //chartOfAccountDto.ID = item.CompanyId;
                //chartOfAccountDto.AccountCode = item.AccountCode;
                //chartOfAccountDto.AccountName = item.AccountName;
                //chartOfAccountDto.AccountTypeID = item.AccountType;
                //chartOfAccountDto.AccountType = cboAccountType.Items[item.AccountType].ToString();
                //chartOfAccountDto.SumFrom = item.SumFrom;
                //chartOfAccountDto.TaxCode = item.TaxCode;
                //listchartOfAccountDto.Add(chartOfAccountDto);

                listchartOfAccountDto.Add(item.AccountCode.ToString() + " | " + item.AccountName);

            }

            //grid.ItemsSource = listchartOfAccountDto;

            cboAccount.ItemsSource = listchartOfAccountDto;

        }

        private void UpdateFields(tTaxSetup tax)
        {
            txtTaxCode.Text = tax.TaxCode;
            txtTaxRate.Text = tax.TaxRate.ToString();
            txtTaxDescription.Text = tax.Description;

            cboAccount.SelectedIndex = -1;
            if (tax.AccountCode != 0)
            {
                for (int i = 0; i < cboAccount.Items.Count; i++)
                {
                    var item = cboAccount.Items[i] as string;
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        var findIndex = item.IndexOf("|", 0);
                        if (findIndex != -1)
                        {
                            int accountCode = Convert.ToInt16(item.Substring(0, findIndex));
                            if (accountCode == tax.AccountCode)
                            {
                                cboAccount.SelectedItem = item;
                                break;
                            }
                        }
                    }
                }

            }

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

        #region Event Handler
        private void GridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems != null)
                {

                    if (e.AddedItems.Count != 0)
                    {
                        var seletectedItem = e.AddedItems[0] as tTaxSetup;
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
    }
}
