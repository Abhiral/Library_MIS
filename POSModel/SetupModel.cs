using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSModel
{
    //AuthorisationModel Section
    #region AuthorisationModel
    public class AuthorisationModel
    {
        public int RoleId { get; set; }
        public Guid UniqueId { get; set; }
        public int ActionId { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Is Authorise")]
        public bool IsAuthorise { get; set; }
        public int? AuthoriseId { get; set; }
        public List<AuthorisationModel> AuthorisationList { get; set; }
    }
    #endregion

    //Role Section
    #region Role Section
    public class RoleModel
    {
        public RoleModel()
        {
            RoleList = new List<RoleModel>();
        }
        public int RoleId { get; set; }
        [Required]
        [DisplayName("Role Name")]
        public string RoleName { get; set; }
        public List<RoleModel> RoleList { get; set; }
    }
    #endregion
}
