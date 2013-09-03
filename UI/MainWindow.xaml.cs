using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.Win32;
using OfficeFinancial.Common;
using System.Globalization;
using OfficeFinancialUI.Model;
using OfficeFinancialUI.ViewModel;

using OfficeFinancialUI.ShatedData;
using OfficeFinancialUI.Infrastracture;

using OfficeFinancialUI.Common;

namespace OfficeFinancialUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LanguageViewModel languageViewModel;
        private ICommandServiceForUI _iCommandServiceForUI = null;
        private BaseUserControl _baseUserControl = null;

        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            loadRenderModuleMenu();
        }

        protected void loadRenderModuleMenu()
        {



            LanguageComboBox.DataContext = ApplicationState.LanguageViewModel;
            LanguageComboBox.SelectedIndex = -1;


        }

        public List<Module> GetModuleMenuList()
        {
            var listModule = new List<Module>();


            var securityModule = new Module { ID = (int)Constants.Module.Security, Name = "Security Administion", ModuleImage = "SecurityAdminModule.PNG", ModuleResourceKey = "moduleAdministration" };
            var companySetupModule = new Module { ID = (int)Constants.Module.Company, Name = "Company Setup", ModuleImage = "CompanySetupModule.PNG", ModuleResourceKey = "moduleCompany" };
            var accountingModule = new Module { ID = (int)Constants.Module.Accounting, Name = "Accounting", ModuleImage = "AccountingModule.PNG", ModuleResourceKey = "moduleAccounting" };

            var saleModule = new Module { ID = (int)Constants.Module.Sale, Name = "Sale", ModuleImage = "SecurityAdminModule.PNG", ModuleResourceKey = "moduleSale" };
            var purchaseModule = new Module { ID = (int)Constants.Module.Purchase, Name = "Purchase", ModuleImage = "CompanySetupModule.PNG", ModuleResourceKey = "modulePurchase" };
            var inventoryModule = new Module { ID = (int)Constants.Module.Inventory, Name = "Inventory", ModuleImage = "CompanySetupModule.PNG", ModuleResourceKey = "moduleInventory" };
            var dimentionModule = new Module { ID = (int)Constants.Module.Dimention, Name = "Dimention", ModuleImage = "AccountingModule.PNG", ModuleResourceKey = "moduleDimentions" };


            listModule.Add(securityModule);
            listModule.Add(companySetupModule);
            listModule.Add(accountingModule);

            listModule.Add(saleModule);
            listModule.Add(purchaseModule);
            listModule.Add(inventoryModule);
            listModule.Add(dimentionModule);

            return listModule;

        }

        public class Module
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string ModuleImage { get; set; }
            public string ModuleResourceKey { get; set; }

        }

        public class Screen
        {
            public string Name { get; set; }

        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedLanguage = LanguageComboBox.SelectedValue as Languages;

            if (selectedLanguage != null)
                App.SetLanguageDictionary(selectedLanguage.Code);


        }

        private void ListViewItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedMenu = sender as ListViewItem;

            var selectedModule = (Constants.Module)Convert.ToInt16(selectedMenu.Tag);

            RenderSubMenu(selectedModule);
        }

        void RenderSubMenu(Constants.Module moduleName)
        {

            listBoxSubMenu.Items.Clear();
            mainPanel.Children.Clear();
            switch (moduleName)
            {
                case Constants.Module.Security:
                    RenderAdministrationSubMenu();
                    break;
                case Constants.Module.Company:
                    RenderCompanySubMenu();
                    break;
                case Constants.Module.Accounting:
                    RenderAccountingSubMenu();
                    break;
                default:
                    //dict.Source = new Uri("..\\Resources\\EnglishLanguage.xaml", UriKind.Relative);
                    break;
            }
        }

        void RenderAdministrationSubMenu()
        {

            var userListMenu = GetSubMenuItem("menuUserList", typeof(View.Security.UserList).ToString());

            var changePassword = GetSubMenuItem("mnuChangepassword", typeof(View.Security.ChangePassword).ToString());

            listBoxSubMenu.Items.Add(userListMenu);
            listBoxSubMenu.Items.Add(changePassword);


        }

        void RenderCompanySubMenu()
        {
            var companySetupMenu = GetSubMenuItem("menuCompanySetup", typeof(View.Company.CompanySetup).ToString());
            var userListMenu = GetSubMenuItem("menuChartOfAccount", typeof(View.Company.ChartOfAccount).ToString());
            var taxSetupMenu = GetSubMenuItem("menuTaxSetup", typeof(View.Company.TaxSetup).ToString());
            var accountingYearMenu = GetSubMenuItem("menuAccountingYearSetup", typeof(View.Accounting.AccountingYearSetup).ToString());
            

            listBoxSubMenu.Items.Add(companySetupMenu);
            listBoxSubMenu.Items.Add(userListMenu);
            listBoxSubMenu.Items.Add(taxSetupMenu);
            listBoxSubMenu.Items.Add(accountingYearMenu);
            
            
        }

        void RenderAccountingSubMenu()
        {
            var voucherTemplate = GetSubMenuItem("mnuVoucherTeplate", typeof(View.Company.VoucherTemplateSetup).ToString());
            var voucherEntry = GetSubMenuItem("mnuVoucherEntry", typeof(View.Company.VoucherEntry).ToString());
            var voucherPostingMenu = GetSubMenuItem("menuVoucherPostingMenu", typeof(View.Accounting.VoucherPosting).ToString());

            listBoxSubMenu.Items.Add(voucherTemplate);
            listBoxSubMenu.Items.Add(voucherEntry);
            listBoxSubMenu.Items.Add(voucherPostingMenu);
        }

        ListBoxItem GetSubMenuItem(string resourceKey, string screenNamespace)
        {
            var subMenu = new ListBoxItem();

            subMenu.SetResourceReference(ContentProperty, resourceKey);
            subMenu.Width = 250;
            subMenu.Tag = screenNamespace;

            return subMenu;

        }



        private void listBoxSubMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;

            if (listBox == null) return;
            var listBoxSelectedItem = listBox.SelectedItem as ListBoxItem;

            if (listBoxSelectedItem == null) return;

            var screenName = listBoxSelectedItem.Tag.ToString();

            LoadUserControlHelper(sender, "OfficeFinancialUI", screenName);
        }

        private void LoadUserControlHelper(object sender, string controlAssemblyName, string controlNamespace)
        {
            //using (new WaitCursor())
            //{
            //MenuItem menuItem = sender as MenuItem;
            //Button button = null;
            //if (menuItem == null)
            //{
            //    button = sender as Button;
            //    if (button == null)
            //        return;
            //}
            //string controlName = menuItem != null ? menuItem.CommandParameter as string : button.CommandParameter as string;
            try
            {
                var instance = Activator.CreateInstance
                        (controlAssemblyName, controlNamespace);

                var control = instance.Unwrap() as UserControl;


                _iCommandServiceForUI = control as ICommandServiceForUI;

                InitApplicationCommand();

                mainPanel.Children.Clear();

                if (control != null) mainPanel.Children.Add(control);
            }
            catch (Exception ex)
            {
                //LoggerHelper.Write(TraceEventType.Error, "Error in loading  " + controlAssemblyName + " " + ex,
                //new string[] { Constants.LOGGING_CATEGORY_DEV, Constants.LOGGING_CATEGORY_PRODUCTION });
                MessageBox.Show("Oops!! Try again later.", "Error in  Processing");
            }

        }

        private void InitApplicationCommand()
        {
            if (_iCommandServiceForUI != null)
            {
                btnAddItem.IsEnabled = _iCommandServiceForUI.AddNewCommand != null;
                btnSaveItem.IsEnabled = _iCommandServiceForUI.SaveCommand != null;
                btnDeleteItem.IsEnabled = _iCommandServiceForUI.DeleteCommand != null;
            }

        }



        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            _iCommandServiceForUI.AddNewCommand();
        }


        private void btnSaveItem_Click(object sender, RoutedEventArgs e)
        {
            TryCatch.BeginTryCatch(() => _iCommandServiceForUI.SaveCommand());

        }

        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            TryCatch.BeginTryCatch(() => _iCommandServiceForUI.DeleteCommand());
        }

        private void btnExportToCSV_Click(object sender, RoutedEventArgs e)
        {
            TryCatch.BeginTryCatch(() =>
                                        {
                                            var filePath = "";
                                            var sd = new SaveFileDialog { Filter = "CSV documents (*.csv)|*.csv" };
                                            sd.ShowDialog();
                                            if (!string.IsNullOrEmpty(sd.FileName))
                                            {
                                                filePath = sd.FileName;
                                            }
                                            if (!string.IsNullOrEmpty(sd.FileName))
                                                _iCommandServiceForUI.ExportToCSVCommand(filePath);
                                        }


                                     );
        }

        private void btnExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            TryCatch.BeginTryCatch(() =>
                    {
                        var filePath = "";
                        var sd = new SaveFileDialog { Filter = "Excel documents (*.xlsx)|*.xlsx" };
                        sd.ShowDialog();
                        if (!string.IsNullOrEmpty(sd.FileName))
                        {
                            filePath = sd.FileName;
                        }
                        if (!string.IsNullOrEmpty(sd.FileName))
                            _iCommandServiceForUI.ExportToEXCELCommand(filePath);
                    }
            );

           
        }

        private void btnExportToPDF_Click(object sender, RoutedEventArgs e)
        {
            TryCatch.BeginTryCatch(() =>
            {
                var filePath = "";
                var sd = new SaveFileDialog { Filter = "Pdf documents (*.pdf)|*.pdf" };
                sd.ShowDialog();
                if (!string.IsNullOrEmpty(sd.FileName))
                {
                    filePath = sd.FileName;
                }
                if (!string.IsNullOrEmpty(sd.FileName))
                    _iCommandServiceForUI.ExportToPDFCommand(filePath);
            }
            );
           
        }

        
    }
}
