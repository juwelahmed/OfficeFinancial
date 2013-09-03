using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFinancial.Common.Dto.Accounting
{
    public class AccountingTransactionPrePostingDto
    {
        public int CompanyId { get; set; }
        public int AccountingYearId { get; set; }
        public int PostingSessionNo { get; set; }
        public int TransactionItemSerialNo { get; set; }
        public Int64 AccountCode { get; set; }
        public string CurrencyCode { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal TotalDebitCredit { get; set; }
        public decimal ValueCreditAmount { get; set; }
        public decimal ValueDebitAmount { get; set; }
        public string Particular { get; set; }
        public Int64 VoucherNo { get; set; }
        public DateTime VoucherDate { get; set; }
        public string UserName { get; set; }
        public DateTime SetupDateTime { get; set; }
        public string AccountName { get; set; }
        public decimal BalanceBefore { get; set; }
    }
}
