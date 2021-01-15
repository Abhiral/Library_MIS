using POSBLL;
using POSDAL;
using PointOfSale.Models;
using POSModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace PointOfSale.Controllers
{
    [Authorize]
    //[InitializeSimpleMembership]
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }
        PointOfSaleEntities _context = new PointOfSaleEntities();
        public ActionResult UserLists(string activeType = "All")
        {
            using (PointOfSaleEntities _context = new PointOfSaleEntities())
            {
                UserProfile userProfile = new UserProfile();
                int userId = WebSecurity.CurrentUserId;
                //Guid uniqueId = CommonService.GetCurrentUserUniqueId(userId);
                string userRole = Roles.GetRolesForUser().FirstOrDefault();

                // userProfile.ActiveStatus = activeType;
                userProfile.UserList = _context.Database.SqlQuery<UserProfile>("SpGetUserList").ToList();

                if (activeType == "Active")
                {
                    userProfile.UserList = userProfile.UserList.Where(x => x.CanLogin == true).ToList();
                }
                else if (activeType == "InActive")
                {
                    userProfile.UserList = userProfile.UserList.Where(x => x.CanLogin == false).ToList();
                }

                return View(userProfile);
            }
        }

        public ActionResult CreateEditUser(int? UserId)
        {
            using (PointOfSaleEntities _context = new PointOfSaleEntities())
            {
                UserProfile profile = new UserProfile();
                if (UserId != null)
                    profile = _context.Database.SqlQuery<UserProfile>("SpGetUserList").Where(x => x.UserId == UserId).FirstOrDefault();
                return PartialView("_CreateUser", profile);
            }
        }
        [HttpPost]
        public ActionResult CreateEditUser(UserProfile profile)
        {
            using (UsersContext _context = new UsersContext())
            {
                ReturnMessageModel rModel = new ReturnMessageModel();
                try
                {
                    if (profile.UserId == 0)
                    {
                        WebSecurity.CreateUserAndAccount(profile.UserName, profile.Password, new { FullName = profile.FullName, MobileNumber = profile.MobileNumber, CanLogin = profile.CanLogin });
                        Roles.AddUserToRole(profile.UserName, profile.RoleName);
                        rModel.Msg = "User Successfully Created !!";
                    }
                    else
                    {
                        _context.Database.ExecuteSqlCommand("Update UserProfile set FullName='" + profile.FullName + "',MobileNumber='" + profile.MobileNumber + "',CanLogin='" + profile.CanLogin + "' where UserId=" + profile.UserId);
                        
                        string userRole = Roles.GetRolesForUser(profile.UserName).FirstOrDefault();
                        Roles.RemoveUserFromRole(profile.UserName, userRole);
                        Roles.AddUserToRole(profile.UserName, profile.RoleName);
                        rModel.Msg = "User Successfully Updated !!";
                    }
                    
                    rModel.Success = true;
                }
                catch (Exception ex)
                {

                    rModel.Msg = "Error : " + ex.Message;
                    rModel.Success = false;
                }
                return Json(rModel);
            }

        }
    }
}
