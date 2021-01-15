using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSModel
{
    public class SubjectModel
    {
        public SubjectModel()
        {
            IsActive = true;
        }
        public int SubjectId { get; set; }

        [DisplayName("Subject Name")]
        public string SubjectName { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }

        public List<SubjectModel> SubjectList { get; set; }
    }
}
