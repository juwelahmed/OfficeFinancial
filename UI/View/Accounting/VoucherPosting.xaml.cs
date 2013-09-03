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
using System.Windows.Navigation;
using System.Windows.Shapes;

using OfficeFinancial.Business;
using OfficeFinancial.Common;
using OfficeFinancial.Common.Dto.Accounting;
using OfficeFinancial.Entities.AccountingEntities;
using OfficeFinancialUI.Infrastracture;

namespace OfficeFinancialUI.View.Accounting
{
    /// <summary>
    /// Interaction logic for VoucherPosting.xaml
    /// </summary>
    public partial class VoucherPosting : UserControl
    {
        public VoucherPosting()
        {
            InitializeComponent();
            try
            {
                LoadAccountingTransactionData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadAccountingTransactionData()
        {
            var listTransactionPreview = new List<TransactionPreview>();

            var transactionData = AccountingService.GetPrePostingTransaction(ShatedData.ApplicationState.SelectedAccountingYearId, ShatedData.ApplicationState.SelectedCompanyId);

            var transactionGroupData = transactionData.GroupBy(e => e.AccountCode);

            foreach (var groupItem in transactionGroupData)
            {
                var accountCode = groupItem.Key;
                var listTransaction = groupItem.ToList();

                var transaction = listTransaction.First();

                var transactionPreviewItem = new TransactionPreview { AccountName = transaction.AccountName, BeforeTransactionBalance = transaction.BalanceBefore,
                    Debit = transaction.DebitAmount, Credit = transaction.CreditAmount, TotalDebitCredit=transaction.TotalDebitCredit, AfterTransactionBalance = transaction.BalanceBefore + transaction.DebitAmount - transaction.CreditAmount };
                transactionPreviewItem.ItemDetail = listTransaction;

                listTransactionPreview.Add(transactionPreviewItem);
                //var transactionPreviewItemDetail= new List<AccountingTransactionPrePostingDto>();
                // foreach (var transactionItem in listTransaction)
                // {

                // }
            }
            grid.ItemsSource = listTransactionPreview;

        }
        public class TransactionPreview
        {
            public string AccountName { get; set; }
            public decimal BeforeTransactionBalance { get; set; }
            public decimal Debit { get; set; }
            public decimal Credit { get; set; }
            public decimal TotalDebitCredit { get; set; }
            public decimal AfterTransactionBalance { get; set; }

            public IEnumerable<AccountingTransactionPrePostingDto> ItemDetail { get; set; }
        }
    }
}
