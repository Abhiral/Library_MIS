using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSModel
{
    public class ConfigChoiceModel
    {
        public int ChoiceId { get; set; }
        [DisplayName("Category Name")]

        public int CategoryId { get; set; }
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        public string Choice { get; set; }
        [DisplayName("Choice Nepali")]

        public string ChoiceNepali { get; set; }
        [DisplayName("Choice Description")]
        public string ChoiceDescription { get; set; }
        public string Val { get; set; }
        [DisplayName("Is Active")]

        public bool IsActive { get; set; }
        public List<ConfigChoiceModel> ConfigChoiceList { get; set; }
    }
}
