//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POSDAL
{
    using System;
    
    public partial class SpGetTransactions_Result
    {
        public int TransactionMasterID { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public string TransactionDateNepali { get; set; }
        public bool IsActive { get; set; }
        public string TransactionType { get; set; }
        public string PartyName { get; set; }
        public string ContactPerson { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string BilledBy { get; set; }
    }
}
