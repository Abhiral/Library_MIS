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
    using System.Collections.Generic;
    
    public partial class ResourceSubscriber
    {
        public ResourceSubscriber()
        {
            this.ResourceIssues = new HashSet<ResourceIssue>();
        }
    
        public int SubscriberId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public bool IsStudent { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public System.DateTime MemberDate { get; set; }
        public int MembershipNumber { get; set; }
    
        public virtual ICollection<ResourceIssue> ResourceIssues { get; set; }
    }
}
