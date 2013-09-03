using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using OfficeFinancial.Common;
using OfficeFinancial.Common.Dto.Accounting;
using OfficeFinancial.Data.DataProvider;
using OfficeFinancial.Entities.AccountingEntities;

namespace OfficeFinancial.Business
{
    public class AccountingService
    {
        #region Chart Of Account
        public static IEnumerable<tChartOfAccount> GetAllChartOfAccount()
        {
            try
            {
                return AccountingDataProvider.GetAllChartOfAccount();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static tChartOfAccount GetAccountByCode(int companyId, int accountcode)
        {
            try
            {
                return AccountingDataProvider.GetAccountByCode(companyId, accountcode);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }

        public static int AddAccount(tChartOfAccount account)
        {
            try
            {
                return AccountingDataProvider.AddAccount(account);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static int UpdateAccount(tChartOfAccount account)
        {
            try
            {
                return AccountingDataProvider.UpdateAccount(account);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static int DeleteAccount(tChartOfAccount account)
        {
            try
            {
                return AccountingDataProvider.DeleteAccount(account);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        #endregion

        #region Tax Setup
        public static IEnumerable<tTaxSetup> GetAllTaxes()
        {
            try
            {
                return AccountingDataProvider.GetAllTaxes();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static tTaxSetup GetTaxesByCode(int companyId, string taxCode)
        {
            try
            {
                return AccountingDataProvider.GetTaxesByCode(companyId, taxCode);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }

        public static bool AddTax(tTaxSetup taxSetup)
        {
            try
            {
                var result = AccountingDataProvider.AddTax(taxSetup);
                if (result == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static bool UpdateTax(tTaxSetup taxSetup)
        {
            try
            {
                var result = AccountingDataProvider.UpdateTax(taxSetup);
                if (result == 1)
                {
                    return true;
                }
                return false;

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static bool DeleteTax(tTaxSetup taxSetup)
        {
            try
            {
                var result = AccountingDataProvider.DeleteTax(taxSetup);
                if (result == 1)
                {
                    return true;
                }
                return false;

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        #endregion

        #region Currency
        public static IEnumerable<tCurrency> GetAllCurrencies()
        {
            try
            {
                return AccountingDataProvider.GetAllCurrencies();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static tCurrency GetCurrencyByCode(string currencyCode)
        {
            try
            {
                return AccountingDataProvider.GetCurrencyByCode(currencyCode);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }

        public static int AddCurrency(tCurrency currency)
        {
            try
            {
                return AccountingDataProvider.AddCurrency(currency);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static int UpdateCurrency(tCurrency currency)
        {
            try
            {
                return AccountingDataProvider.UpdateCurrency(currency);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static int DeleteCurrency(tCurrency currency)
        {
            try
            {
                return AccountingDataProvider.DeleteCurrency(currency);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        #endregion

        #region Currency Rate
        public static IEnumerable<tCurrencyRate> GetAllCurrencieRates()
        {
            try
            {
                return AccountingDataProvider.GetAllCurrencieRates();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static int AddCurrencyRate(tCurrencyRate currencyRate)
        {
            try
            {
                return AccountingDataProvider.AddCurrencyRate(currencyRate);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static int UpdateCurrencyRate(tCurrencyRate currencyRate)
        {
            try
            {
                return AccountingDataProvider.UpdateCurrencyRate(currencyRate);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static int DeleteCurrencyRate(tCurrencyRate currencyRate)
        {
            try
            {
                return AccountingDataProvider.DeleteCurrencyRate(currencyRate);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        #endregion

        #region Debtor Group
        public static IEnumerable<tDebtorGroup> GetAllDebtorGroups()
        {
            try
            {
                return AccountingDataProvider.GetAllDebtorGroups();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static tDebtorGroup GetDebtorGroupById(string debtorGroupId)
        {
            try
            {
                return AccountingDataProvider.GetDebtorGroupById(debtorGroupId);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }

        public static int AddDebtorGroup(tDebtorGroup debtorGroup)
        {
            try
            {
                return AccountingDataProvider.AddDebtorGroup(debtorGroup);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static int UpdateDebtorGroup(tDebtorGroup debtorGroup)
        {
            try
            {
                return AccountingDataProvider.UpdateDebtorGroup(debtorGroup);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static int DeleteDebtorGroup(tDebtorGroup debtorGroup)
        {
            try
            {
                return AccountingDataProvider.DeleteDebtorGroup(debtorGroup);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        #endregion

        #region Accounting Year
        public static IEnumerable<tAccountingYear> GetAllAccountingYears()
        {
            try
            {
                return AccountingDataProvider.GetAllAccountingYear();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static tAccountingYear GetAccountingYearById(int companyId, int accountYearId)
        {
            try
            {
                return AccountingDataProvider.GetAccountingYearById(companyId, accountYearId);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }

        public static int AddAccountingYear(tAccountingYear tAccountingYear)
        {
            try
            {
                return AccountingDataProvider.AddAccountingYear(tAccountingYear);
            }
            catch (SqlException ex)
            {
                throw new BusinessApplicationException("Error in busines logic of AddAccountingYear", ex);
            }
            //catch (Exception exception)
            //{
            //    throw new Exception(exception.Message);
            //}
        }

        public static int UpdateAccountingYear(tAccountingYear tAccountingYear)
        {
            try
            {
                return AccountingDataProvider.UpdateAccountingYear(tAccountingYear);
            }
            catch (SqlException ex)
            {
                throw new BusinessApplicationException("Error in busines logic of UpdateAccountingYear", ex);
            }
        }

        public static int DeleteAccountingYear(tAccountingYear tAccountingYear)
        {
            try
            {
                return AccountingDataProvider.DeleteAccountingYear(tAccountingYear);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        #endregion

        #region Voucher Template
        public static IEnumerable<VoucherTemplateDto> GetAllVoucherTemplates()
        {
            try
            {
                var vouchers = (List<tVoucherTemplate>)AccountingDataProvider.GetAllVoucherTemplates();
                var voucherViewModel = new List<VoucherTemplateDto>();
                VoucherTemplateDto model;

                if (vouchers != null)
                {
                    vouchers.ForEach(delegate(tVoucherTemplate voucherTemplate)
                                         {
                                             model = new VoucherTemplateDto
                                                         {
                                                             CompanyId = voucherTemplate.CompanyId,
                                                             CompanyName = "",
                                                             CreditAccountCode = voucherTemplate.CreditAccount,
                                                             CreditAccountName = AccountingDataProvider.GetAccountByCode(voucherTemplate.CompanyId, voucherTemplate.CreditAccount).AccountName,
                                                             DebitAccountCode = voucherTemplate.DebitAccount,
                                                             DebitAccountName = AccountingDataProvider.GetAccountByCode(voucherTemplate.CompanyId, voucherTemplate.DebitAccount).AccountName,
                                                             Narration = voucherTemplate.Narration,
                                                             ShortCode = voucherTemplate.ShortCode,

                                                         };
                                             voucherViewModel.Add(model);
                                         }
                        );

                    return voucherViewModel;
                }
            }

            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

            return null;
        }

        public static VoucherTemplateDto GetlVoucherTemplateByCode(string shortCode, int companyId)
        {
            try
            {

                var voucherTemplate = AccountingDataProvider.GetVoucherTemplateByCode(shortCode, companyId);

                var viewModel = new VoucherTemplateDto
                {
                    CompanyId = voucherTemplate.CompanyId,
                    CompanyName = "",
                    CreditAccountCode = voucherTemplate.CreditAccount,
                    CreditAccountName = AccountingDataProvider.GetAccountByCode(voucherTemplate.CompanyId, voucherTemplate.CreditAccount).AccountName,
                    DebitAccountCode = voucherTemplate.DebitAccount,
                    DebitAccountName = AccountingDataProvider.GetAccountByCode(voucherTemplate.CompanyId, voucherTemplate.DebitAccount).AccountName,
                    Narration = voucherTemplate.Narration,
                    ShortCode = voucherTemplate.ShortCode,

                };

                return viewModel;
            }

            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }

        public static bool AddVoucherTemplate(VoucherTemplateDto voucherTemplate)
        {
            try
            {
                var entity = new tVoucherTemplate
                                 {
                                     CompanyId = voucherTemplate.CompanyId,
                                     CreditAccount = voucherTemplate.CreditAccountCode,
                                     DebitAccount = voucherTemplate.DebitAccountCode,
                                     Narration = voucherTemplate.Narration,
                                     ShortCode = voucherTemplate.ShortCode,
                                 };
                var result = AccountingDataProvider.AddVoucherTemplate(entity);

                if (result == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static bool UpdateVoucherTemplate(VoucherTemplateDto voucherTemplate)
        {
            try
            {
                var entity = new tVoucherTemplate
                {
                    CompanyId = voucherTemplate.CompanyId,
                    CreditAccount = voucherTemplate.CreditAccountCode,
                    DebitAccount = voucherTemplate.DebitAccountCode,
                    Narration = voucherTemplate.Narration,
                    ShortCode = voucherTemplate.ShortCode,
                };

                var result = AccountingDataProvider.UpdateVoucherTemplate(entity);

                if (result == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static bool DeleteVoucherTemplate(VoucherTemplateDto voucherTemplate)
        {
            try
            {
                var entity = new tVoucherTemplate
                {
                    CompanyId = voucherTemplate.CompanyId,
                    CreditAccount = voucherTemplate.CreditAccountCode,
                    DebitAccount = voucherTemplate.DebitAccountCode,
                    Narration = voucherTemplate.Narration,
                    ShortCode = voucherTemplate.ShortCode,
                };

                var result = AccountingDataProvider.DeleteVoucherTemplate(entity);

                if (result == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        #endregion

        #region Voucher Template
        public static IEnumerable<AccountingTransactionTempDto> GetAllUnpostedVoucher()
        {
            try
            {
                var vouchers = AccountingDataProvider.GetAllUnpostedVoucher() as List<tAccountingTransactionTemp>;
                var voucherViewModel = new List<AccountingTransactionTempDto>();
                AccountingTransactionTempDto model;

                if (vouchers != null)
                {
                    vouchers.ForEach(delegate(tAccountingTransactionTemp table)
                    {
                        model = new AccountingTransactionTempDto
                        {
                            CompanyId = table.CompanyId,
                            CreditAccount = table.CreditAccount,
                            CreditAccountName = AccountingDataProvider.GetAccountByCode(table.CompanyId, table.CreditAccount).AccountName,
                            DebitAccount = table.DebitAccount,
                            DebitAccountName = AccountingDataProvider.GetAccountByCode(table.CompanyId, table.DebitAccount).AccountName,
                            AccountingYearId = table.AccountingYearId,
                            VoucherNo = table.VoucherNo,
                            CurrencyCode = table.Currency,
                            LineId = table .LineId,
                            Total = table.Total,
                            UserId = table.UserId,
                            ValueTotal = table.ValueTotal,
                            VoucherDate = table.VoucherDate

                        };
                        voucherViewModel.Add(model);
                    }
                        );

                    return voucherViewModel;
                }
            }

            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

            return null;
        }

        public static AccountingTransactionTempDto GetUnpostedVoucherByNo(int voucherNo)
        {
            try
            {
                var table = AccountingDataProvider.GetUnpostedVoucherByNo(voucherNo);

                var model = new AccountingTransactionTempDto
                {
                    CompanyId = table.CompanyId,
                    CreditAccount = table.CreditAccount,
                    CreditAccountName = AccountingDataProvider.GetAccountByCode(table.CompanyId, table.CreditAccount).AccountName,
                    DebitAccount = table.DebitAccount,
                    DebitAccountName = AccountingDataProvider.GetAccountByCode(table.CompanyId, table.DebitAccount).AccountName,
                    AccountingYearId = table.AccountingYearId,
                    VoucherNo = table.VoucherNo,
                    CurrencyCode = table.Currency,
                    LineId = table.LineId,
                    Total = table.Total,
                    UserId = table.UserId,
                    ValueTotal = table.ValueTotal,
                    VoucherDate = table.VoucherDate

                };

                return model;
            }

            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }

        public static bool AddUnpostedVoucher(AccountingTransactionTempDto voucherEntryDto)
        {
            try
            {
                var newVoucherNo = AccountingDataProvider.GetLastVoucherNo();

                var entity = new tAccountingTransactionTemp
                {
                    CompanyId = voucherEntryDto.CompanyId,
                    CreditAccount = voucherEntryDto.CreditAccount,
                    DebitAccount = voucherEntryDto.DebitAccount,
                    AccountingYearId = voucherEntryDto.AccountingYearId,
                    VoucherNo = newVoucherNo,
                    Currency = voucherEntryDto.CurrencyCode,
                    LineId = voucherEntryDto.LineId,
                    Total = voucherEntryDto.Total,
                    UserId = voucherEntryDto.UserId,
                    ValueTotal = voucherEntryDto.ValueTotal,
                    VoucherDate = voucherEntryDto.VoucherDate,
                    
                };
                var result = AccountingDataProvider.AddUnpostedVoucher(entity);

                if (result == 1)
                {
                    AccountingDataProvider.DeleteLastVoucherInformation(new tLastVoucherInformation
                                                                            {LastVoucherNo = newVoucherNo});
                    AccountingDataProvider.AddLastVoucherInformation(new tLastVoucherInformation
                                                                         {LastVoucherNo = newVoucherNo + 1});
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static bool UpdateUnpostedVoucher(AccountingTransactionTempDto voucherEntryDto)
        {
            try
            {
                var entity = new tAccountingTransactionTemp
                {
                    CompanyId = voucherEntryDto.CompanyId,
                    CreditAccount = voucherEntryDto.CreditAccount,
                    DebitAccount = voucherEntryDto.DebitAccount,
                    AccountingYearId = voucherEntryDto.AccountingYearId,
                    VoucherNo = voucherEntryDto.VoucherNo,
                    Currency = voucherEntryDto.CurrencyCode,
                    LineId = voucherEntryDto.LineId,
                    Total = voucherEntryDto.Total,
                    UserId = voucherEntryDto.UserId,
                    ValueTotal = voucherEntryDto.ValueTotal,
                    VoucherDate = voucherEntryDto.VoucherDate,

                };

                var result = AccountingDataProvider.UpdateUnpostedVoucher(entity);

                if (result == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static bool DeleteUnpostedVoucher(AccountingTransactionTempDto voucherEntryDto)
        {
            try
            {
                var entity = new tAccountingTransactionTemp
                {
                    CompanyId = voucherEntryDto.CompanyId,
                    CreditAccount = voucherEntryDto.CreditAccount,
                    DebitAccount = voucherEntryDto.DebitAccount,
                    AccountingYearId = voucherEntryDto.AccountingYearId,
                    VoucherNo = voucherEntryDto.VoucherNo,
                    Currency = voucherEntryDto.CurrencyCode,
                    LineId = voucherEntryDto.LineId,
                    Total = voucherEntryDto.Total,
                    UserId = voucherEntryDto.UserId,
                    ValueTotal = voucherEntryDto.ValueTotal,
                    VoucherDate = voucherEntryDto.VoucherDate,

                };

                var result = AccountingDataProvider.DeleteUnpostedVoucher(entity);

                if (result == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        #endregion
        #region Accounting Transaction

        public static IEnumerable<AccountingTransactionPrePostingDto> GetPrePostingTransaction(int accountYearId, int companyId)
        {
            try
            {
                return AccountingDataProvider.GetPrePostingTransaction(accountYearId, companyId);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }


        #endregion
    }
}
