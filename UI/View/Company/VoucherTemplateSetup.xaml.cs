using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OfficeFinancial.Business;
using OfficeFinancial.Common.Dto.Accounting;
using OfficeFinancial.Entities.AccountingEntities;
using OfficeFinancialUI.Infrastracture;
using System.Linq;

namespace OfficeFinancialUI.View.Company
{
    /// <summary>
    /// Interaction logic for VoucherTemplateSetup.xaml
    /// </summary>
    public partial class VoucherTemplateSetup : UserControl, ICommandServiceForUI
    {
        #region Private Variable
        private IEnumerable<VoucherTemplateDto> _tVoucherTemplates = new List<VoucherTemplateDto>();
        private int _companyId = 1;
        private bool _isNewRecord = true;
        #endregion

        #region Constructor
        public VoucherTemplateSetup()
        {
            InitializeComponent();

            InitGrid();
        }
        #endregion

        #region Private Method

        private void InitGrid()
        {
            _tVoucherTemplates = AccountingService.GetAllVoucherTemplates();

            GenerateCombo();

            grid.ItemsSource = _tVoucherTemplates;

        }
       
        private void GenerateCombo()
        {
            var accounts = (List<tChartOfAccount>) AccountingService.GetAllChartOfAccount();
            var source = accounts.Select(a => new OfficeFinancial.Common.ComboBoxItem { Key = a.AccountCode.ToString(), Value = a.AccountCode + " | " + a.AccountName }).ToList();
            //source.Insert(0,null);

            cboCreditAccount.DisplayMemberPath = "Value";
            cboCreditAccount.SelectedValuePath = "Key";
            cboCreditAccount.ItemsSource = source;

            cboDebitAccount.DisplayMemberPath = "Value";
            cboDebitAccount.SelectedValuePath = "Key";
            cboDebitAccount.ItemsSource = source;
            
        }

        private bool IsValidData()
        {
            if (String.IsNullOrEmpty(txtShortCode.Text))
            {
                MessageBox.Show("Short Code can not be empty.", "Data Validation", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                txtShortCode.Focus();
                return false;
            }
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
            if (String.IsNullOrEmpty(txtNarration.Text))
            {
                MessageBox.Show("Narration can not be empty.", "Data Validation", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                txtNarration.Focus();
                return false;
            }
            return true;
        }

        private bool Save()
        {
            if (!IsValidData())
                return false;

            var template = GetTemplate();

            bool isAdded;
            if (_isNewRecord)
            {
                isAdded = AccountingService.AddVoucherTemplate(template);
            }
            else
            {
                isAdded = AccountingService.UpdateVoucherTemplate(template);
            }

            ClearFields();

            _isNewRecord = true;
            InitGrid();

            return isAdded;
        }

        private VoucherTemplateDto GetTemplate()
        {
            return new VoucherTemplateDto
                       {
                           ShortCode = txtShortCode.Text,
                           DebitAccountCode = Convert.ToInt32(cboDebitAccount.SelectedValue),
                           CreditAccountCode = Convert.ToInt32(cboCreditAccount.SelectedValue),
                           CompanyId = _companyId,
                           Narration = txtNarration.Text,
                       };
        }

        private bool ClearFields()
        {
            txtShortCode.Text = String.Empty;
            cboDebitAccount.SelectedIndex = -1;
            cboCreditAccount.SelectedIndex = - 1;
            txtNarration.Text = String.Empty;

            _isNewRecord = true;

            return true;
        }

        private bool Delete()
        {
            if (!String.IsNullOrWhiteSpace(txtShortCode.Text))
            {
                try
                {
                    var result = AccountingService.DeleteVoucherTemplate(GetTemplate());

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

        private void UpdateFields(VoucherTemplateDto template)
        {
            txtShortCode.Text = template.ShortCode;
            cboDebitAccount.SelectedValue = template.DebitAccountCode.ToString();
            cboCreditAccount.SelectedValue = template.CreditAccountCode.ToString();
            txtNarration.Text = template.Narration;
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
                        var seletectedItem = e.AddedItems[0] as VoucherTemplateDto;
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
            //e.Handled = ValidationHelper.IsTextNumeric(e.Text);
        }

        #endregion


    }

}
