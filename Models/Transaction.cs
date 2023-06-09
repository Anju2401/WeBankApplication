//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeBankApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Transaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transaction()
        {
            this.Fund_Transfer = new HashSet<Fund_Transfer>();
        }
    
        public int ID { get; set; }
        public string account_number { get; set; }
        public double amount { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime Transaction_Date { get; set; }
        public string Transaction_Type { get; set; }
        public Nullable<int> AccID { get; set; }
    
        public virtual ACCOUNT_HOLDER_DETAILS ACCOUNT_HOLDER_DETAILS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fund_Transfer> Fund_Transfer { get; set; }
    }
}
