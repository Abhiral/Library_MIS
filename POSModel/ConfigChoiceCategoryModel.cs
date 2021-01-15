using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSModel
{
    public class ConfigChoiceCategoryModel
    {
        [DisplayName("Category Name")]

        public int CategoryId { get; set; }
        public string Category { get; set; }
        [DisplayName("Category Nepali")]

        public string CategoryNepali { get; set; }
        [DisplayName("Category Description")]
        public string CategoryDescription { get; set; }
        [DisplayName("Is Active")]

        public bool IsActive { get; set; }
        public List<ConfigChoiceCategoryModel> ConfigChoiceCategoryList { get; set; }
    }
}
