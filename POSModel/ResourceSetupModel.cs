using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSModel
{
    public class ResourceSetupModel
    {

        public ResourceSetupModel()
        {
            IsPublisher = true;
            IsAuthor = true;
            IsActive = true;
        }
        
        public int ResourceId { get; set; }

        [DisplayName("Resource Type Id")]
        public int ResourceTypeId { get; set; }

        [DisplayName("Publication Id")]
        public int? PublicationId { get; set; }

        [DisplayName("Publication Name")]
        public string PublicationName { get; set; }

        [DisplayName("Author Id")]
        public int? AuthorId { get; set; }

        [DisplayName("Author Name")]
        public string AuthorName { get; set; }

        [DisplayName("Resource Name")]
        public string ResourceName { get; set; }

        public string Remarks { get; set; }

        [DisplayName("Grade Id")]
        public Nullable<int> GradeId { get; set; }

        [DisplayName("Subject Id")]
        public Nullable<int> SubjectId { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public Nullable<DateTime> ModifiedDate { get; set; }

        public Nullable<int> ModifiedBy { get; set; }

        public List<ResourceSetupModel> ResourceSetupList { get; set; }

        public string Publisher { get; set; }

        [DisplayName("Resource Type")]
        public string ResourceTypeName { get; set; }

        public string Author { get; set; }
        [DisplayName("Is Publisher")]
        public bool IsPublisher { get; set; }
        [DisplayName("Is Author")]
        public bool IsAuthor { get; set; }

    }
}
