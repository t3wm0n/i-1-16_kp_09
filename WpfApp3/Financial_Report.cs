//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp3
{
    using System;
    using System.Collections.Generic;
    
    public partial class Financial_Report
    {
        public int ID_Report { get; set; }
        public int Order_ID { get; set; }
        public System.DateTime Report_Date { get; set; }
        public int Costs { get; set; }
        public int Profit { get; set; }
    
        public virtual Order Order { get; set; }
    }
}
