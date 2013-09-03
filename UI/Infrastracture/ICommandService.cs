using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFinancialUI.Infrastracture
{
    public interface ICommandServiceForUI
    {
        ///// <summary>
        ///// It's a wrapper for call delegate add new method
        ///// </summary>
        ///// <param name="addnewMethod">delegate for save method</param>
        //void AddNew(Action addnewMethod);

        ///// <summary>
        /////  It's a wrapper for call delegate save method
        ///// </summary>
        ///// <param name="saveMethod"> delegate for save method</param>
        ///// <returns> return delegate invoked result </returns>
        //bool SaveRecord(Func<bool> saveMethod);

        ///// <summary>
        ///// It's a wrapper for call delete delete method
        ///// </summary>
        ///// <param name="deleteMethod">delete for delete method </param>
        ///// <returns>return delegate invoked result</returns>
        //bool DeleteRecord(Func<bool> deleteMethod);

        //bool AllowToAddData { get; }

        //bool AllowToSaveData { get; }
        
        //bool AllowToAddDeleteData { get; }
        Func<bool> AddNewCommand { get;  }
        Func<bool> SaveCommand { get;  }
        Func<bool> DeleteCommand { get;  }

        Func<string,bool> ExportToCSVCommand { get; }
        Func<string ,bool> ExportToEXCELCommand { get; }
        Func<string,bool> ExportToPDFCommand { get; }
    }
}
