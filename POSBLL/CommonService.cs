
using POSDAL;
using POSModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace POSBLL
{
    public class RoleControllerActionModel
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public List<RoleControllerActionModel> AuthLists { get; set; }

    }
    public class NepaliMonthModel
    {
        public int Month { get; set; }
        public string MonthName { get; set; }
    }

    public class SelectModel
    {

        public int SelectId { get; set; }
        public string SelectText { get; set; }
    }
    public class CommonService
    {

        public static string GetMonthNameByMonth(int month)
        {
            List<NepaliMonthModel> NepaliMonths = new List<NepaliMonthModel>();
            var baisakh = new NepaliMonthModel() { Month = 1, MonthName = "Baisakh" }; NepaliMonths.Add(baisakh);
            var jestha = new NepaliMonthModel() { Month = 2, MonthName = "Jestha" }; NepaliMonths.Add(jestha);
            var ashadh = new NepaliMonthModel() { Month = 3, MonthName = "Ashadh" }; NepaliMonths.Add(ashadh);
            var shrawan = new NepaliMonthModel() { Month = 4, MonthName = "Shrawan" }; NepaliMonths.Add(shrawan);
            var bhadra = new NepaliMonthModel() { Month = 5, MonthName = "Bhadra" }; NepaliMonths.Add(bhadra);
            var ashoj = new NepaliMonthModel() { Month = 6, MonthName = "Ashoj" }; NepaliMonths.Add(ashoj);
            var kartik = new NepaliMonthModel() { Month = 7, MonthName = "Kartik" }; NepaliMonths.Add(kartik);
            var mangsir = new NepaliMonthModel() { Month = 8, MonthName = "Mangsir" }; NepaliMonths.Add(mangsir);
            var poush = new NepaliMonthModel() { Month = 9, MonthName = "Poush" }; NepaliMonths.Add(poush);
            var magh = new NepaliMonthModel() { Month = 10, MonthName = "Magh" }; NepaliMonths.Add(magh);
            var falgun = new NepaliMonthModel() { Month = 11, MonthName = "Falgun" }; NepaliMonths.Add(falgun);
            var chait = new NepaliMonthModel() { Month = 12, MonthName = "Chaitra" }; NepaliMonths.Add(chait);
            return NepaliMonths.Where(x => x.Month == month).Select(x => x.MonthName).FirstOrDefault();
        }

        public static List<RoleControllerActionModel> AssignRoleAuthorization(string Role)
        {
            using (PointOfSaleEntities _context = new PointOfSaleEntities())
            {
                List<RoleControllerActionModel> AuthoriseActions = _context.Database.SqlQuery<RoleControllerActionModel>("Exec SJ_SpGetRoleAuthorization '" + Role + "'").ToList();
                if (AuthoriseActions == null)
                {
                    AuthoriseActions = new List<RoleControllerActionModel>();
                }
                return AuthoriseActions;
            }

        }

        public static List<SelectModel> GetUsers(string userRole)
        {
            using (PointOfSaleEntities _context = new PointOfSaleEntities())
            {

                return _context.Database.SqlQuery<SelectModel>("SpGetUsers @userRole",
                    new SqlParameter("userRole", userRole)
                    ).ToList();

            }
        }
        public static int GetUserIdByName(string userName)
        {
            using (PointOfSaleEntities _context = new PointOfSaleEntities())
            {
                return _context.Database.SqlQuery<int>("select UserId from UserProfile where UserName=" + userName).FirstOrDefault();
            }
        }
        public static ConfigChoiceCategoryModel GetConfigChoiceCategoryById(int categoryId)
        {
            using (PointOfSaleEntities _context = new PointOfSaleEntities())
            {
                var cccModel = _context.ConfigChoiceCategories
                            .Where(x => x.CategoryId == categoryId)
                            .Select(x => new ConfigChoiceCategoryModel
                            {
                                CategoryId = x.CategoryId,
                                Category = x.Category,
                                CategoryDescription = x.CategoryDescription
                            }).FirstOrDefault();

                return cccModel;
            }
        }
        public static List<ConfigChoiceModel> GetConfigChoiceListByCategory(string categoryName)
        {
            using (PointOfSaleEntities _context = new PointOfSaleEntities())
            {
                var ccModel = (from ccc in _context.ConfigChoiceCategories
                               join cc in _context.ConfigChoices on ccc.CategoryId equals cc.CategoryId
                               where ccc.Category == categoryName
                               select new ConfigChoiceModel
                               {
                                   CategoryId = ccc.CategoryId,
                                   CategoryName = ccc.Category,
                                   Choice = cc.Choice,
                                   ChoiceDescription = cc.ChoiceDescription,
                                   ChoiceId = cc.ChoiceId,
                                   Val = cc.Val
                               }).ToList();

                return ccModel;
            }
        }

        public static SelectList GetTrueFalse()
        {
            return new SelectList(new List<SelectListItem>
                {

                    new SelectListItem { Text = "True", Value = "true"},
                    new SelectListItem { Text = "False", Value = "false"},
                }, "Value", "Text");

        }
        public static List<RoleModel> GetRoleList()
        {
            using (PointOfSaleEntities _context = new PointOfSaleEntities())
            {
                List<RoleModel> rList = _context.Database.SqlQuery<RoleModel>("Select RoleId,RoleName from webpages_Roles").ToList();
                return rList;
            }

        }



        public static ConfigChoiceModel GetConfigChoiceModel(int choiceId)
        {
            using (PointOfSaleEntities _context = new PointOfSaleEntities())
            {
                ConfigChoiceModel model = new ConfigChoiceModel();
                model = _context.ConfigChoices.Where(x => x.ChoiceId == choiceId).Select(x => new ConfigChoiceModel
                {
                    ChoiceId = x.ChoiceId,
                    Choice = x.Choice
                }).FirstOrDefault();
                return model;
            }

        }
       


        public static List<SelectModel> GetConfigChoices(string category)
        {
            using (PointOfSaleEntities _context = new PointOfSaleEntities())
            {
                List<SelectModel> rList = (from c in _context.ConfigChoices
                                           where c.ConfigChoiceCategory.Category == category && c.IsActive == true
                                           select new SelectModel()
                                           {
                                               SelectId = c.ChoiceId,
                                               SelectText = c.Choice
                                           }).OrderBy(p => p.SelectText).ToList();
                return rList;
            }
        }
        public static string GetCurrentNepaliDate(DateTime englishDate)
        {
            using (PointOfSaleEntities _context = new PointOfSaleEntities())
            {
                string NepaliDate = _context.Database.SqlQuery<string>("exec EnglishDateToNepali '" + englishDate + "'").FirstOrDefault();
                return NepaliDate;
            }
        }
        public static DateTime GetEnglishDate(string nepaliDate)
        {
            using (PointOfSaleEntities _context = new PointOfSaleEntities())
            {
                var englishDate = _context.Database.SqlQuery<DateTime>("exec NepaliDateToEnglish '" + nepaliDate + "'").FirstOrDefault();
                return englishDate;
            }
        }
        public static bool ValidateNepaliDate(string nepaliDate)
        {
            var DateArray = nepaliDate.Split('-');
            if (DateArray.Length == 3)
            {
                if (DateArray[0].Length == 4 && DateArray[1].Length == 2 && DateArray[2].Length == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }


        //public static string ToPropertyName<T>(this Expression<Func<T, object>> expression)
        //{
        //    var body = expression.Body as MemberExpression ?? ((UnaryExpression)expression.Body).Operand as MemberExpression;
        //    if (body != null)
        //    {
        //        return body.Member.Name;
        //    }
        //    return string.Empty;
        //}

        //public static SelectList ToSelectList<T>(this IEnumerable<T> items, Expression<Func<T, object>> value,
        //   Expression<Func<T, Object>> text, bool isLocalized, object selectedValue = null)
        //{
        //    //if (isLocalized)
        //    //{
        //    //    var langName = System.Threading.Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName;

        //    //    if (!langName.EqualsIgnoreCase("en"))
        //    //    {
        //    //        var textPropertyName = string.Concat(text.ToPropertyName().Remove(text.ToPropertyName().Length - 3), langName);

        //    //        return new SelectList(items, value.ToPropertyName(), textPropertyName, selectedValue);
        //    //    }
        //    //}
        //    return new SelectList(items, value.ToPropertyName(), text.ToPropertyName(), selectedValue);
        //}



        public static string GetCurrencyFormat(decimal amount)
        {
            CultureInfo hindi = new CultureInfo("hi-IN");
            return string.Format(hindi, "{0:#,0.00}", amount);
        }

    }
}
