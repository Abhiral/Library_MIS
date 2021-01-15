using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace POSModel
{
    public class ResourceAuthorModel
    {
        public ResourceAuthorModel()
        {
            IsActive = true;
        }
        public int AuthorId { get; set; }
        public string Author { get; set; }
        public int Nationality { get; set; }
        public string Genere { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        [DisplayName("Country Name")]
        public string CountryName { get; set; }

        public List<ResourceAuthorModel> ResourceAuthorList { get; set; }
    }
}
