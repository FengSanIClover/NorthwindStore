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
    
    using Repository.Pattern.Ef6;
    using Newtonsoft.Json;
    public partial class Employees : Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employees()
        {
            this.AuthTokens = new HashSet<AuthTokens>();
            this.Employees1 = new HashSet<Employees>();
            this.Orders = new HashSet<Orders>();
            this.Territories = new HashSet<Territories>();
        }
    
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<System.DateTime> HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        public Nullable<int> ReportsTo { get; set; }
        public string PhotoPath { get; set; }
        public Nullable<int> AuthorizationID { get; set; }
    
    	[JsonIgnore]
        public virtual Accounts Accounts { get; set; }
    	[JsonIgnore]
        public virtual Authorizes Authorizes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    	[JsonIgnore]
        public virtual ICollection<AuthTokens> AuthTokens { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    	[JsonIgnore]
        public virtual ICollection<Employees> Employees1 { get; set; }
    	[JsonIgnore]
        public virtual Employees Employees2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    	[JsonIgnore]
        public virtual ICollection<Orders> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    	[JsonIgnore]
        public virtual ICollection<Territories> Territories { get; set; }
    }
}
