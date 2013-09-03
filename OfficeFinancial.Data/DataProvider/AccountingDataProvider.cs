using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OfficeFinancial.Data.Helper;
using OfficeFinancial.Entities.AccountingEntities;
using Dapper;
using System.Data.SqlClient;
using OfficeFinancial.Common.Dto.Accounting;

namespace OfficeFinancial.Data.DataProvider
{
    public class AccountingDataProvider
    {
        #region Chart Of Account
        public static IEnumerable<tChartOfAccount> GetAllChartOfAccount()
        {
            IEnumerable<tChartOfAccount> accounts;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                const string query = "SELECT * FROM tChartOfAccount C ORDER BY C.AccountCode";

                accounts = connection.Query<tChartOfAccount>(query);
            }

            return accounts;
        }

        public static tChartOfAccount GetAccountByCode(int companyId, int accountcode)
        {
            tChartOfAccount account;
            const string query = "SELECT * FROM tChartOfAccount C WHERE CompanyID=@CompanyID and  AccountCode = @AccountCode";

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                account =
                    connection.Query<tChartOfAccount>(query, new tChartOfAccount { CompanyId = companyId, AccountCode = accountcode }).FirstOrDefault();
            }

            return account;
        }

        public static int AddAccount(tChartOfAccount account)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query =
                    new StringBuilder(
                        "INSERT INTO tChartOfAccount(CompanyID,AccountCode, AccountName, AccountType, SumFrom, TaxCode)");
                query.Append("VALUES(@CompanyID,@AccountCode, @AccountName, @AccountType, @SumFrom, @TaxCode)");

                rowsAdded = connection.Execute(query.ToString(), account);

            }

            return rowsAdded;
        }

        public static int UpdateAccount(tChartOfAccount account)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query =
                    new StringBuilder(
                        "UPDATE tChartOfAccount SET  AccountName = @AccountName, ");
                query.Append("AccountType = @AccountType, SumFrom = @SumFrom, TaxCode = @TaxCode WHERE CompanyID = @CompanyID and AccountCode=@AccountCode");

                rowsAdded = connection.Execute(query.ToString(), account);
            }

            return rowsAdded;
        }

        public static int DeleteAccount(tChartOfAccount account)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query =
                    new StringBuilder("DELETE FROM tChartOfAccount WHERE CompanyID = @CompanyID and AccountCode=@AccountCode");

                rowsAdded = connection.Execute(query.ToString(), account);
            }

            return rowsAdded;
        } 
        #endregion

        #region Tax Setup
        public static IEnumerable<tTaxSetup> GetAllTaxes()
        {
            IEnumerable<tTaxSetup> tTaxs;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                const string query = "SELECT * FROM tTaxSetup C ORDER BY C.TaxCode";

                tTaxs = connection.Query<tTaxSetup>(query);
            }

            return tTaxs;
        }

        public static tTaxSetup GetTaxesByCode(int companyId, string taxCode)
        {
            tTaxSetup tTaxs;
            const string query = "SELECT * FROM tTaxSetup C WHERE CompanyID=@CompanyID and  TaxCode = @TaxCode";

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                tTaxs =
                    connection.Query<tTaxSetup>(query, new tTaxSetup { CompanyId = companyId, TaxCode = taxCode }).FirstOrDefault();
            }

            return tTaxs;
        }

        public static int AddTax(tTaxSetup tTaxSetup)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query =
                    new StringBuilder(
                        "INSERT INTO tTaxSetup(CompanyID,TaxCode,TaxRate,AccountCode,  Description)");
                query.Append("VALUES(@CompanyID,@TaxCode, @TaxRate,@AccountCode, @Description)");

                rowsAdded = connection.Execute(query.ToString(), tTaxSetup);

            }

            return rowsAdded;
        }

        public static int UpdateTax(tTaxSetup tTaxSetup)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query =
                    new StringBuilder(
                        "UPDATE tTaxSetup SET TaxRate=@TaxRate,AccountCode = @AccountCode, Description = @Description ");
                query.Append("WHERE  CompanyID=@CompanyID and TaxCode = @TaxCode");

                rowsAdded = connection.Execute(query.ToString(), tTaxSetup);
            }

            return rowsAdded;
        }

        public static int DeleteTax(tTaxSetup tTaxSetup)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query =
                    new StringBuilder("DELETE FROM tTaxSetup WHERE  CompanyID=@CompanyID and TaxCode = @TaxCode");

                rowsAdded = connection.Execute(query.ToString(), tTaxSetup);
            }

            return rowsAdded;
        }

        #endregion

        #region Currency
        public static IEnumerable<tCurrency> GetAllCurrencies()
        {
            IEnumerable<tCurrency> currencies;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                string query = "SELECT * FROM tCurrency C ORDER BY C.Code";

                currencies = connection.Query<tCurrency>(query);
            }

            return currencies;
        }

        public static tCurrency GetCurrencyByCode(string currencyCode)
        {
            tCurrency currency;
            const string query = "SELECT * FROM tCurrency C WHERE Code = @Code";

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                currency =
                    connection.Query<tCurrency>(query, new tCurrency { Code  = currencyCode }).FirstOrDefault();
            }

            return currency;
        }

        public static int AddCurrency(tCurrency currency)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query =
                    new StringBuilder(
                        "INSERT INTO tCurrency(Code, Description, Note,Sign,CurrentRate,LastRate,UpdateDateTime)");
                query.Append("VALUES(@Code, @Description, @Note,@Sign,@CurrentRate,@LastRate,@UpdateDateTime)");

                rowsAdded = connection.Execute(query.ToString(), currency);

            }

            return rowsAdded;
        }

        public static int UpdateCurrency(tCurrency currency)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query =
                    new StringBuilder(
                        "UPDATE tCurrency SET Description = @Description,Note = @Note,Sign=@Sign,CurrentRate=@CurrentRate,LastRate=@LastRate,UpdateDateTime=@UpdateDateTime ");
                query.Append(" WHERE Code = @Code");

                rowsAdded = connection.Execute(query.ToString(), currency);
            }

            return rowsAdded;
        }

        public static int DeleteCurrency(tCurrency currency)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query =
                    new StringBuilder("DELETE FROM tCurrency WHERE Code = @Code");

                rowsAdded = connection.Execute(query.ToString(), currency);
            }

            return rowsAdded;
        }
        #endregion

        #region Currency Rate
        public static IEnumerable<tCurrencyRate> GetAllCurrencieRates()
        {
            IEnumerable<tCurrencyRate> currencyRates;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                string query = "SELECT * FROM tCurrencyRate C ORDER BY C.Id";

                currencyRates = connection.Query<tCurrencyRate>(query);
            }

            return currencyRates;
        }
        
        public static int AddCurrencyRate(tCurrencyRate currencyRate)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query =
                    new StringBuilder(
                        "INSERT INTO tCurrencyRate(Currency, Rate, RateDate)");
                query.Append("VALUES(@Currency, @Rate,@RateDate)");

                rowsAdded = connection.Execute(query.ToString(), currencyRate);

            }

            return rowsAdded;
        }

        public static int UpdateCurrencyRate(tCurrencyRate currencyRate)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query =
                    new StringBuilder(
                        "UPDATE tCurrencyRate SET Currency = @Currency, ");
                query.Append("Rate = @Rate, RateDate = @RateDate WHERE Id = @Id");

                rowsAdded = connection.Execute(query.ToString(), currencyRate);
            }

            return rowsAdded;
        }

        public static int DeleteCurrencyRate(tCurrencyRate currencyRate)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query =
                    new StringBuilder("DELETE FROM tCurrencyRate WHERE Id = @Id");

                rowsAdded = connection.Execute(query.ToString(), currencyRate);
            }

            return rowsAdded;
        }
        #endregion

        #region Debtor Group
        public static IEnumerable<tDebtorGroup> GetAllDebtorGroups()
        {
            IEnumerable<tDebtorGroup> debtorGroups;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                string query = "SELECT * FROM tDebtorGroup C ORDER BY C.DebtorGroupId";

                debtorGroups = connection.Query<tDebtorGroup>(query);
            }

            return debtorGroups;
        }

        public static tDebtorGroup GetDebtorGroupById(string debtorGroupId)
        {
            tDebtorGroup debtorGroup;
            string query = "SELECT * FROM tDebtorGroup C WHERE DebtorGroupId = @DebtorGroupId";

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                debtorGroup =
                    connection.Query<tDebtorGroup>(query, new tDebtorGroup { DebtorGroupId = debtorGroupId }).FirstOrDefault();
            }

            return debtorGroup;
        }

        public static int AddDebtorGroup(tDebtorGroup debtorGroup)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query =
                    new StringBuilder(
                        "INSERT INTO tDebtorGroup(DebtorGroupId, Currency, InterestRatePerMonth,IsTaxFree,ReceivableAccountId,SaleAccountId,PurchaseAccountId,CounterAccountId)");
                query.Append("VALUES(@DebtorGroupId, @Currency, @InterestRatePerMonth,@IsTaxFree,@ReceivableAccountId,@SaleAccountId,@PurchaseAccountId,@CounterAccountId)");

                rowsAdded = connection.Execute(query.ToString(), debtorGroup);

            }

            return rowsAdded;
        }

        public static int UpdateDebtorGroup(tDebtorGroup debtorGroup)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query =
                    new StringBuilder(
                        "UPDATE tDebtorGroup SET Currency = @Currency,InterestRatePerMonth = @InterestRatePerMonth,IsTaxFree = @IsTaxFree,ReceivableAccountId = @ReceivableAccountId ");
                query.Append("SaleAccountId = @SaleAccountId, PurchaseAccountId = @PurchaseAccountId, CounterAccountId = @CounterAccountId WHERE DebtorGroupId = @DebtorGroupId");

                rowsAdded = connection.Execute(query.ToString(), debtorGroup);
            }

            return rowsAdded;
        }

        public static int DeleteDebtorGroup(tDebtorGroup debtorGroup)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query =
                    new StringBuilder("DELETE FROM tDebtorGroup WHERE DebtorGroupId = @DebtorGroupId");

                rowsAdded = connection.Execute(query.ToString(), debtorGroup);
            }

            return rowsAdded;
        }
        #endregion

        #region Accounting Period

        public static IEnumerable<tAccountingYear> GetAllAccountingYear()
        {
            IEnumerable<tAccountingYear> tAccountingYears;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                const string query = "SELECT * FROM tAccountingYear A ORDER BY A.AccountYearId";

                tAccountingYears = connection.Query<tAccountingYear>(query);
            }

            return tAccountingYears;
        }

        public static tAccountingYear GetAccountingYearById(int companyId,int accountYearId)
        {
            tAccountingYear tAccountingYear;
            const string query = "SELECT * FROM tAccountingYear A WHERE A.CompanyId=@CompanyId and  A.AccountYearId = @AccountYearId";

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                tAccountingYear =
                    connection.Query<tAccountingYear>(query, new tAccountingYear { CompanyId =companyId ,AccountYearId = accountYearId }).FirstOrDefault();
            }

            return tAccountingYear;
        }

        public static int AddAccountingYear(tAccountingYear tAccountingYear)
        {
            int rowsAdded;

            try
            {

            
           
            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query =
                    new StringBuilder(
                        "INSERT INTO tAccountingYear(CompanyId, AccountYearId,StartMonth,EndMonth,StartVoucherNo,EndVoucherNo,StartPostingDate,EndPostingDate,SetDateTime,EntryUser)");
                query.Append("VALUES( @CompanyId, @AccountYearId,@StartMonth,@EndMonth,@StartVoucherNo,@EndVoucherNo,@StartPostingDate,@EndPostingDate,@SetDateTime,@EntryUser)");

                rowsAdded = connection.Execute(query.ToString(), tAccountingYear);

            }
            }
             catch (SqlException ex)
            {
                //LoggerHelper.Write(TraceEventType.Error, "Error in AddAccountingYear. " + ex,
                // new[] { Constants.LOGGING_CATEGORY_EXCEPTION });   
                throw ex;
            }
            return rowsAdded;
        }

        public static int UpdateAccountingYear(tAccountingYear tAccountingYear)
        {
            int rowsUpdated;

            try
            {

            
            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query =
                    new StringBuilder("UPDATE tAccountingYear SET AccountYearId = @AccountYearId,StartMonth=@StartMonth,EndMonth=@EndMonth,StartVoucherNo=@StartVoucherNo, EndVoucherNo=@EndVoucherNo");
                query.Append(" , StartPostingDate= @StartPostingDate,EndPostingDate=@EndPostingDate,SetDateTime=@SetDateTime,EntryUser=@EntryUser");
                query.Append(" WHERE CompanyId=@CompanyId and  AccountYearId = @AccountYearId");

                rowsUpdated = connection.Execute(query.ToString(), tAccountingYear);
            }
            }
            catch (SqlException ex)
            {
                throw ex ;
            }
            return rowsUpdated;
        }

        public static int DeleteAccountingYear(tAccountingYear tAccountingYear)
        {
            int rowsDeleted;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query =
                    new StringBuilder("DELETE FROM tAccountingYear WHERE CompanyId=@CompanyId and  AccountYearId = @AccountYearId");

                rowsDeleted = connection.Execute(query.ToString(), tAccountingYear);
            }

            return rowsDeleted;
        }  

        #endregion

        #region Voucher Template
        public static IEnumerable<tVoucherTemplate> GetAllVoucherTemplates()
        {
            IEnumerable<tVoucherTemplate> templates;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                string query = SqlQueryHelper.GetSelectQuery<tVoucherTemplate>();

                templates = connection.Query<tVoucherTemplate>(query);
            }

            return templates;
        }

        public static tVoucherTemplate GetVoucherTemplateByCode(string shortCode, int companyId)
        {
            tVoucherTemplate templates;
            //string query = DynamicQuery.GetSelectQuery<tVoucherTemplate>((p) => p.ShortCode == "s").Sql; //SqlQueryHelper.GetSelectQuery<tVoucherTemplate>(param => param.ShortCode == shortCode).Sql;
            string query = "SELECT * FROM tVoucherTemplate WHERE ShortCode = @ShortCode";

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                templates = connection.Query<tVoucherTemplate>(query, new tVoucherTemplate { ShortCode = shortCode, CompanyId = companyId }).FirstOrDefault();
            }

            return templates;
        }

        public static int AddVoucherTemplate(tVoucherTemplate templates)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query = SqlQueryHelper.GetInsertQuery<tVoucherTemplate>();

                rowsAdded = connection.Execute(query, templates);

            }

            return rowsAdded;
        }

        public static int UpdateVoucherTemplate(tVoucherTemplate templates)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query = SqlQueryHelper.GetUpdateQuery<tVoucherTemplate>();

                rowsAdded = connection.Execute(query, templates);
            }

            return rowsAdded;
        }

        public static int DeleteVoucherTemplate(tVoucherTemplate templates)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query = SqlQueryHelper.GetDeleteQuery<tVoucherTemplate>();

                rowsAdded = connection.Execute(query, templates);
            }

            return rowsAdded;
        }
        #endregion

        #region Voucher Entry
        public static IEnumerable<tAccountingTransactionTemp> GetAllUnpostedVoucher()
        {
            IEnumerable<tAccountingTransactionTemp> entries;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                string query = SqlQueryHelper.GetSelectQuery<tAccountingTransactionTemp>();

                entries = connection.Query<tAccountingTransactionTemp>(query);
            }

            return entries;
        }

        public static tAccountingTransactionTemp GetUnpostedVoucherByNo(int voucherNo)
        {
            tAccountingTransactionTemp templates;
            string query = "SELECT * FROM tAccountingTransactionTemp WHERE VoucherNo = @VoucherNo";

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                templates = connection.Query<tAccountingTransactionTemp>(query, new tAccountingTransactionTemp { VoucherNo = voucherNo}).FirstOrDefault();
            }

            return templates;
        }

        public static int AddUnpostedVoucher(tAccountingTransactionTemp transaction)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query = SqlQueryHelper.GetInsertQuery<tAccountingTransactionTemp>();

                rowsAdded = connection.Execute(query, transaction);

            }

            return rowsAdded;
        }

        public static int UpdateUnpostedVoucher(tAccountingTransactionTemp transaction)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query = SqlQueryHelper.GetUpdateQuery<tAccountingTransactionTemp>();

                rowsAdded = connection.Execute(query, transaction);
            }

            return rowsAdded;
        }

        public static int DeleteUnpostedVoucher(tAccountingTransactionTemp transaction)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query = SqlQueryHelper.GetDeleteQuery<tAccountingTransactionTemp>();

                rowsAdded = connection.Execute(query, transaction);
            }

            return rowsAdded;
        }
        #endregion

        public static int GetLastVoucherNo()
        {
            string query = "SELECT * FROM tLastVoucherInformation";
            tLastVoucherInformation lastVoucherInformation;

            var lastVoucherNo = 0;
            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                lastVoucherInformation = connection.Query<tLastVoucherInformation>(query).FirstOrDefault();
                if(lastVoucherInformation != null)
                {
                    lastVoucherNo = lastVoucherInformation.LastVoucherNo;
                }
            }

            return lastVoucherNo;
        }

        public static int AddLastVoucherInformation(tLastVoucherInformation lastVoucher)
        {
            int rowsAdded;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                var query = SqlQueryHelper.GetInsertQuery<tLastVoucherInformation>();

                rowsAdded = connection.Execute(query, lastVoucher);

            }

            return rowsAdded;
        }
        public static int DeleteLastVoucherInformation(tLastVoucherInformation lastVoucher)
        {
            int rowsAdded;

            var query = "DELETE FROM tLastVoucherInformation";
            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                rowsAdded = connection.Execute(query, lastVoucher);
            }

            return rowsAdded;
        }


        #region Accounting Transaction

        public static IEnumerable<AccountingTransactionPrePostingDto> GetPrePostingTransaction(int accountYearId, int companyId)
        {
            IEnumerable<AccountingTransactionPrePostingDto> prePostingTransactions;

            using (IDbConnection connection = DbConnectionHelper.GetConnection())
            {
                const string query = "USP_Accounting_VoucherPosting";

                var p = new DynamicParameters();
                p.Add("@AccountingYearID", accountYearId);
                p.Add("@CompanyID", companyId);
                p.Add("@IsReviewOnly", 1);

                prePostingTransactions = connection.Query<AccountingTransactionPrePostingDto>(query, param: p, commandType: CommandType.StoredProcedure);
            }

            return prePostingTransactions;
        }

        #endregion
    }
}
