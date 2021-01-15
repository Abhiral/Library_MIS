using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace POSModel
{
    public class CountryModel
    {
        public CountryModel()
        {
            IsActive = true;
        }
        public int CountryId { get; set; }
        [DisplayName("Country Name")]
        public string CountryName { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public List<CountryModel> CountryList { get; set; }
    }
}
