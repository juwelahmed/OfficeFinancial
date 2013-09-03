using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using OfficeFinancialUI.ShatedData;

namespace OfficeFinancialUI.Setup
{
    public class SetupHelper
    {

      public static  void Run()
        {
            LoadTheme();
            LoadFont();
            LoadFontSize();
        }

        private static void LoadFontSize()
        {
            var fontSizeSetup = ConfigurationManager.AppSettings["fontsize"];

            if (fontSizeSetup != null)
            {
                string[] fontSizelist = fontSizeSetup.Split(',');

                foreach (string fontSize in fontSizelist)
                    ApplicationState.FontSize.Add(Convert.ToInt16(fontSize));
            
            }
            else
            {
                for (int i = 0; i < 13; i++)
                    ApplicationState.FontSize.Add(i);
            }
        }

        private static void LoadTheme()
        {
            var themes = ConfigurationManager.AppSettings["themes"];

            if (themes != null)
            {
                string[] themelist = themes.Split(',');

                foreach (string theme in themelist)
                {
                    ApplicationState.Themes.Add(theme);
                   
                }

            }
            else
                ApplicationState.Themes.Add("Default");
        }

        private static void LoadFont()
        {
             System.Drawing.Text.PrivateFontCollection fontCollection=new System.Drawing.Text.PrivateFontCollection();
             
            foreach (var font in fontCollection.Families)
                ApplicationState.Fonts.Add(font.Name);
        }
    }
}
