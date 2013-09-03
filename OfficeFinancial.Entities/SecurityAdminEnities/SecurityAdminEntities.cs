using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFinancial.Entities.SecurityAdminEnities
{
    public class tUser 
    {
        public virtual string ID { get; set; }
        public virtual string Password { get; set; }
        public virtual string Notes { get; set; }
        public virtual string LanguageCode { get; set; }
        public virtual string Theme { get; set; }
        public virtual string FontName { get; set; }
        public virtual int FontSize { get; set; }

    }
}
