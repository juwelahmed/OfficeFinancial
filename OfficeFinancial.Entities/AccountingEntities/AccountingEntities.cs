using System;
using System.ComponentModel.DataAnnotations;
using OfficeFinancial.Common;


namespace OfficeFinancial.Entities.AccountingEntities
{
    public class tChartOfAccount 
    {
        public virtual int CompanyId { get; set; }
        public virtual int AccountCode { get; set; }
        public virtual string AccountName { get; set; }
        public virtual int AccountType { get; set; }
        public virtual int? SumFrom { get; set; }
        public virtual string TaxCode { get; set; }

    }

    public class tTaxSetup
    {
        public virtual int CompanyId { get; set; }
        public virtual string TaxCode { get; set; }
        public virtual Decimal TaxRate { get; set; }
        public virtual string Description { get; set; }
        public int AccountCode { get; set; }
    }

    public class tCurrency
    {
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }
        public virtual string Notes { get; set; }
        public virtual string Sign { get; set; }
        public virtual decimal CurrentRate { get; set; }
        public virtual decimal LastRate { get; set; }
        public virtual DateTime UpdateDateTime { get; set; }
    }

    public class tCurrencyRate
    {
        public virtual int Id { get; set; }

        public virtual string CurrencyCode { get; set; }

        public virtual Decimal Rate { get; set; }

        public virtual DateTime RateDate { get; set; }
    }

    public class tAccountingYear
    {
        public virtual int CompanyId { get; set; }
        public virtual Int64 AccountYearId { get; set; }
        public virtual int StartMonth { get; set; }
        public virtual int EndMonth { get; set; }
        public virtual Int64 StartVoucherNo { get; set; }
        public virtual Int64 EndVoucherNo { get; set; }
        public virtual DateTime StartPostingDate { get; set; }
        public virtual DateTime EndPostingDate { get; set; }
        public virtual bool IsLocked { get; set; }
        public virtual DateTime SetDateTime { get; set; }
        public virtual DateTime UpdateDateTime { get; set; }
        public virtual string EntryUser { get; set; }

    }
    public class tDebtorGroup
    {
        public virtual string DebtorGroupId { get; set; }

        public virtual string CurrencyCode { get; set; }

        public virtual Decimal InterestRatePerMonth { get; set; }

        public virtual bool IsTaxFree { get; set; }

        public virtual int ReceivableAccountId { get; set; }

        public virtual int SaleAccountId { get; set; }

        public virtual int PurchaseAccountId { get; set; }

        public virtual int CounterAccountId { get; set; }
    }

    public class tVoucherTemplate
    {
        [Key]
        public virtual String ShortCode { get; set; }

        public virtual Int32 DebitAccount { get; set; }

        public virtual Int32 CreditAccount { get; set; }

        public virtual Int32 CompanyId { get; set; }

        public virtual String Narration { get; set; }
    }

    public class tAccountingTransactionTemp
    {
        [Identity]
        [Key]
        public virtual Int64 LineId { get; set; }

        public virtual decimal AccountingYearId { get; set; }

        public virtual Int32 CompanyId { get; set; }

        public virtual Int32 DebitAccount { get; set; }

        public virtual Int32 CreditAccount { get; set; }

        public virtual string Currency { get; set; }

        public virtual decimal Total { get; set; }

        public virtual decimal ValueTotal { get; set; }

        public virtual DateTime VoucherDate { get; set; }

        public virtual Int32 VoucherNo { get; set; }

        public virtual string UserId { get; set; }
    }

    public class tLastVoucherInformation
    {
        [Key]
        public virtual int LastVoucherNo { get; set; }

    }
}
