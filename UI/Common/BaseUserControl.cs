using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OfficeFinancialUI.Common
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:PI.Zobra.UI.Common.UserControls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:PI.Zobra.UI.Common.UserControls;assembly=PI.Zobra.UI.Common.UserControls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:BaseControl/>
    ///
    /// </summary>
    public class BaseUserControl : UserControl
    {
        public Func<bool > AddNewCommand;
        public Func<bool> SaveCommand;
        public Func<bool> DeleteCommand;

        public Func<bool> PrintCommand;
        public Func<bool> ExportToCSVCommand;
        public Func<bool> ExportToEXCELCommand;
        public Func<bool> ExportToPDFCommand;

        //private TextBlock _statusBar;

        //private Window _parentWindow;

        //public Window ParentWindow
        //{
        //    get { return _parentWindow; }
        //    set
        //    {
        //        _parentWindow = value;
        //        if (_parentWindow != null)
        //            _statusBar = _parentWindow.FindName("sbarStatus") as TextBlock;
        //    }
        //}

        //private bool _isError = false;

        //public bool IsError
        //{
        //    get { return _isError; }
        //    set { _isError = value; }
        //}

        //private bool _isDirty = false;
        //public bool IsDirty
        //{
        //    get { return _isDirty; }
        //    set
        //    {
        //        _isDirty = value;
        //        SharedState.GlobalDirty = value;
        //        if (_isDirty && !SharedState.DirtyUserControls.Contains(this.Name))
        //        {
        //            SharedState.DirtyUserControls.Add(this.Name);
        //        }
        //        else if (!_isDirty && SharedState.DirtyUserControls.Contains(this.Name))
        //        {
        //            SharedState.DirtyUserControls.Remove(this.Name);
        //        }
        //        if (!_isDirty)
        //            ResetStatusBar();
        //    }
        //}

        public BaseUserControl()
        {
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("bn-BD");
        }

        public void ShowInfoInStatusBar(string msg)
        {
            //_statusBar.Text = msg;
        }

        public void ShowErrorInStatusBar(string msg)
        {
            //if (!IsError)
            //{
            //    _statusBar.Text = msg;
            //}
            //else
            //{
            //    Style sbarServiceStyle = new Style(typeof(TextBlock));
            //    sbarServiceStyle.Setters.Add(new Setter(TextBlock.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF88F13"))));
            //    _statusBar.Style = sbarServiceStyle;
            //    _statusBar.Text = "Error in processing.";
            //}
        }

        public void ResetStatusBar()
        {
            //if (_statusBar != null)
            //    _statusBar.Text = String.Empty;
        }

        //private void BaseControl_Unloaded(object sender, RoutedEventArgs e)
        //{

        //}

    }
}
