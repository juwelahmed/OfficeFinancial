using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFinancial.Common.Dto.Accounting
{
   public  class ChartOfAccountDto
    {
       public int ID { get; set; }
       public int AccountCode { get; set; }
       public string AccountName { get; set; }
       public int AccountTypeID { get; set; }
       public string AccountType { get; set; }
       public int? SumFrom { get; set; }
       public string TaxCode { get; set; }
    }

   public class VoucherTemplateDto
   {
       public String ShortCode { get; set; }

       public Int32 DebitAccountCode { get; set; }

       public string DebitAccountName { get; set; }

       public string DebitAccountCodeWithName { get; set; }

       public Int32 CreditAccountCode { get; set; }

       public string CreditAccountName { get; set; }

       public Int32 CreditAccountCodeWithName { get; set; }

       public Int32 CompanyId { get; set; }

       public string CompanyName { get; set; }

       public String Narration { get; set; }
   }

   public class AccountingTransactionTempDto
   {
       public Int64 LineId { get; set; }

       public decimal AccountingYearId { get; set; }

       public Int32 CompanyId { get; set; }

       public Int32 DebitAccount { get; set; }

       public string DebitAccountName { get; set; }

       public Int32 CreditAccount { get; set; }

       public string CreditAccountName { get; set; }

       public string CurrencyCode { get; set; }

       public decimal Total { get; set; }

       public decimal ValueTotal { get; set; }

       public DateTime VoucherDate { get; set; }

       public Int32 VoucherNo { get; set; }

       public string UserId { get; set; }
   }

}
