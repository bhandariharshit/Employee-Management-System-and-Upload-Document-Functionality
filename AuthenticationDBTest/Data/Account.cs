//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AuthenticationDBTest.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Account
    {
        public int AccountId { get; set; }
        public Nullable<System.DateTime> AccountCreationDate { get; set; }
        public Nullable<int> AccountTypeId { get; set; }
    
        public virtual AccountType AccountType { get; set; }
    }
}
