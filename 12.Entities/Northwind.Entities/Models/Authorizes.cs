//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Northwind.Entities.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Authorizes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Authorizes()
        {
            this.Employees = new HashSet<Employees>();
        }
    
        public int AuthorizationID { get; set; }
        public string AuthorizationType { get; set; }
        public string Description { get; set; }
    
        public virtual Authorizes Authorizes1 { get; set; }
        public virtual Authorizes Authorizes2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
