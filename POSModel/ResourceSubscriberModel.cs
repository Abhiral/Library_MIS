using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSModel
{
    public class ResourceSubscriberModel
    {
        public ResourceSubscriberModel()
        {
            IsActive = true;
        }
        public int SubscriberId { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Is Student")]
        public bool IsStudent { get; set; }

        [DisplayName("Is Employee")]
        public bool IsEmployee { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }

        [DisplayName("Member Date")]
        public DateTime MemberDate { get; set; }
        [DisplayName("Member Date (Nepali)")]
        public String MemberDateNepali { get; set; }

        [DisplayName("Membership Number")]
        public int MembershipNumber { get; set; }
        public string FullName { get; set; }
        public List<ResourceSubscriberModel> ResourceSubscriberList { get; set; }
    }
}
