using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OfficeFinancial.Business;
using OfficeFinancial.Common.Dto.Accounting;
using OfficeFinancialUI.ShatedData;

namespace OfficeFinancialUI.View.Company
{
    /// <summary>
    /// Interaction logic for VoucherTemplateSearch.xaml
    /// </summary>
    public partial class VoucherTemplateSearch : Window
    {
        #region Private Variable
        private List<VoucherTemplateDto> _tVoucherTemplates = new List<VoucherTemplateDto>();
        private TextBox _shortCodeTextBox;
        private ComboBox _debitAccount;
        private ComboBox _creditAccount;
        #endregion

        public VoucherTemplateSearch()
        {
            InitializeComponent();
        }
        public VoucherTemplateSearch(ref TextBox textBox, ref ComboBox debitAccountComboBox, ref ComboBox creditAccountComboBox)
        {
            InitializeComponent();
            _shortCodeTextBox = textBox;
            _debitAccount = debitAccountComboBox;
            _creditAccount = creditAccountComboBox;

        }

        private void InitGrid()
        {
            _tVoucherTemplates.Add(AccountingService.GetlVoucherTemplateByCode(txtShortCode.Text, ApplicationState.SelectedCompanyId));
                
            grid.ItemsSource = _tVoucherTemplates;

        }

        private void BtnShowClick(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtShortCode.Text))
            {
                InitGrid();
            }
        }

        private void GridMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    var grid = sender as ExtendedGrid.ExtendedGridControl.ExtendedDataGrid;
                    if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                    {
                        var dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as ExtendedGrid.Microsoft.Windows.Controls.DataGridRow;
                        var item = dgr.Item as VoucherTemplateDto;
                        _shortCodeTextBox.Text = item.ShortCode;
                        _debitAccount.SelectedValue = item.DebitAccountCode;
                        _creditAccount.SelectedValue = item.CreditAccountCode;
                    }
                }

                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "System Message", MessageBoxButton.OK,
                                   MessageBoxImage.Error);
            }
        }

    }
}
