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
    
    public partial class Accounts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Accounts()
        {
            this.AuthTokens = new HashSet<AuthTokens>();
        }
    
        public int EmployeeID { get; set; }
        public string UserAccount { get; set; }
        public string UserPassword { get; set; }
    
        public virtual Accounts Accounts1 { get; set; }
        public virtual Accounts Accounts2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AuthTokens> AuthTokens { get; set; }
        public virtual Employees Employees { get; set; }
    }
}