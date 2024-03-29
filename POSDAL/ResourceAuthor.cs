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
    
    public partial class ResourceAuthor
    {
        public ResourceAuthor()
        {
            this.ResourceSetups = new HashSet<ResourceSetup>();
        }
    
        public int AuthorId { get; set; }
        public string Author { get; set; }
        public int Nationality { get; set; }
        public string Genere { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual ICollection<ResourceSetup> ResourceSetups { get; set; }
    }
}
