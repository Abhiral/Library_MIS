using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSModel
{
    public class ResourceGenerationModel
    {
        public ResourceGenerationModel()
        {
            IsActive = true;
        }
        public int GenerationId { get; set; }

         [DisplayName("Resource Name")]
        public int ResourceId { get; set; }

        [DisplayName("Generation Date")]
        public DateTime GenerationDate { get; set; }

        [DisplayName("Generation Copy Count")]
        public int GenerationCopyCount { get; set; }
        public string Remarks { get; set; }
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }

        public List<ResourceGenerationModel> GetResourceGenerationList { get; set; }
        [DisplayName("Resource Name")]
        public string ResourceName { get; set; }
        [DisplayName("Resource Copy Id")]
        public int ResourceCopyId { get; set; }

        [DisplayName("Generation Date (Nepali)")]
        public string GenerationDateNepali { get; set; }
    }
}
