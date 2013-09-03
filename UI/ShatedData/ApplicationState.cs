using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OfficeFinancialUI.ViewModel;

namespace OfficeFinancialUI.ShatedData
{
    public class ApplicationState
    {
        public static string LoginUserName { get; set; }
        public static string SelectedLanguage { get; set; }

        private static LanguageViewModel _languageViewModel;
        public static LanguageViewModel LanguageViewModel
        {
            get
            {
                if (_languageViewModel == null)
                    _languageViewModel = new LanguageViewModel();

                return _languageViewModel;
            }
        }

        private static List<string> _themes;
        public static List<string> Themes
        {
            get
            {
                if (_themes == null)
                    _themes = new List<string>();

                return _themes;
            }
        }

        private static List<int> _fontSize;
        public static List<int> FontSize
        {
            get
            {
                if (_fontSize == null)
                    _fontSize = new List<int>();

                return _fontSize;
            }
        }


        private static List<string> _fonts;
        public static List<string> Fonts
        {
            get
            {
                if (_fonts == null)
                    _fonts = new List<string>();

                return _fonts;
            }
        }

        public static int SelectedCompanyId { get; set; }
        public static int SelectedAccountingYearId { get; set; }
        public static string SelectedCompanyCurrency { get; set; }
    }
}
