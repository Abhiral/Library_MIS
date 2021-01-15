using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace POSModel
{
    public class ResourceIssueModel
    {
        public ResourceIssueModel()
        {
            IsAuthor = true;
            IsPublisher = true;
            IsActive = true;
        }
        public int IssueId { get; set; }

        [DisplayName("Resource Copy Id")]
        public int ResourceCopyId { get; set; }

        [DisplayName("Issue Date")]
        public DateTime IssueDate { get; set; }

        [DisplayName("Return Back Date")]
        public DateTime ReturnBackDate { get; set; }
        [DisplayName("Returned Date")]
        public DateTime ReturnedDate { get; set; }
        public string Remarks { get; set; }
        
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }

        [DisplayName("Issue Date (Nepali)")]
        public string IssueDateNepali { get; set; }

        [DisplayName("Return Back Date (Nepali)")]
        public string ReturnBackDateNepali { get; set; }


        public List<ResourceIssueModel> ResourceIssueList { get; set; }
        [DisplayName("Resource Name")]
        public string ResourceName { get; set; }

        public string FullName { get; set; }

        [DisplayName("Resource Name")]
        public int ResourceId { get; set; }

        [DisplayName("Publication")]
        public int? PublicationId { get; set; }

        [DisplayName("Author")]
        public int? AuthorId { get; set; }

        [DisplayName("Grade")]
        public int? GradeId { get; set; }

        [DisplayName("Subject Name")]
        public int? SubjectId { get; set; }

        [DisplayName("Subscriber")]
        public int SubscriberId { get; set; }

        [DisplayName("Resource Copy Number")]
        public string ResourceCopyNumber { get; set; }
        
        [DisplayName("Is Available")]
        public bool IsAvailable { get; set; }

        [DisplayName("Is Publisher")]
        public bool IsPublisher { get; set; }

        [DisplayName("Is Author")]
        public bool IsAuthor { get; set; }

        [DisplayName("Author Name")]
        public string AuthorName { get; set; }

        [DisplayName("Publication Name")]
        public string PublicationName { get; set; }

    }
}
