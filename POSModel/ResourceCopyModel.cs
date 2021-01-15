using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSModel
{
    public class ResourceCopyModel
    {
        public ResourceCopyModel()
        {
            IsActive = true;
        }
        public int ResourceCopyId { get; set; }

        [DisplayName("Generation Id")]
        public int GenerationId { get; set; }

        [DisplayName("Resource Id")]
        public int ResourceId { get; set; }

        [DisplayName("Resource Copy Count")]
        public int ResourceCopyCount { get; set; }

        [DisplayName("Resource Copy Number")]
        public string ResourceCopyNumber { get; set; }
        public string Remarks { get; set; }
        [DisplayName("Is Available")]
        public bool IsAvailable { get; set; }

        [DisplayName("Published Date")]
        public DateTime PublishedDate { get; set; }
        public int Edition { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }

        public List<ResourceCopyModel> ResourceCopyList { get; set; }

        public string GenerationDateNepali { get; set; }
        [DisplayName("Resource Name")]
        public string ResourceName { get; set; }
    }
}
