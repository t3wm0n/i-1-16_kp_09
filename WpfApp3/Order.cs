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
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.Financial_Report = new HashSet<Financial_Report>();
        }
    
        public int ID_Order { get; set; }
        public string Promocode { get; set; }
        public int Order_Cost { get; set; }
        public int Trip_ID { get; set; }
        public int Payment_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Financial_Report> Financial_Report { get; set; }
        public virtual Payment_Method Payment_Method { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
