using POSDAL;
using POSModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMatrix.WebData;

namespace POSBLL
{
    public class SetupService : ISetupService
    {
        PointOfSaleEntities _context;

        ReturnMessageModel rModel;

        public SetupService()
        {
            rModel = new ReturnMessageModel();
            _context = new PointOfSaleEntities();
        }

        #region ConfigChoiceCategory
        public List<ConfigChoiceCategoryModel> GetConfigChoiceCategoryList()
        {
            return _context.ConfigChoiceCategories.Select(x => new ConfigChoiceCategoryModel
            {
                CategoryId = x.CategoryId,
                Category = x.Category,
                CategoryNepali = x.CategoryNepali,
                CategoryDescription = x.CategoryDescription,
                IsActive = x.IsActive
            }).ToList();
        }
        public ReturnMessageModel CreateEditConfigChoiceCategory(ConfigChoiceCategoryModel iModel)
        {
            try
            {
                var cccRow = _context.ConfigChoiceCategories.Where(x => x.CategoryId == iModel.CategoryId).FirstOrDefault();
                if (cccRow == null)
                {
                    cccRow = new ConfigChoiceCategory();
                }

                cccRow.Category = iModel.Category;
                cccRow.CategoryNepali = iModel.CategoryNepali;
                cccRow.CategoryDescription = iModel.CategoryDescription;
                cccRow.IsActive = iModel.IsActive;
                if (cccRow.CategoryId == 0)
                {
                    _context.ConfigChoiceCategories.Add(cccRow);
                    _context.SaveChanges();
                }
                else
                {
                    _context.ConfigChoiceCategories.Attach(cccRow);
                    _context.Entry(cccRow).State = EntityState.Modified;
                    _context.SaveChanges();
                }

                rModel.Msg = "ConfigChoiceCategory Saved Successfully!!";
                rModel.Success = true;
                return rModel;
            }
            catch (Exception ex)
            {
                rModel.Msg = "ConfigChoiceCategory Save Failed!!";
                rModel.Success = false;
                return rModel;
            }
        }
        #endregion

        #region ConfigChoice
        public List<ConfigChoiceModel> GetConfigChoiceList(int CategoryId)
        {
            return (from cc in _context.ConfigChoices
                    join ccc in _context.ConfigChoiceCategories on cc.CategoryId equals ccc.CategoryId
                    where cc.CategoryId == CategoryId
                    select new ConfigChoiceModel
                     {
                         ChoiceId = cc.ChoiceId,
                         CategoryId = cc.CategoryId,
                         CategoryName = ccc.Category,
                         Choice = cc.Choice,
                         ChoiceNepali = cc.ChoiceNepali,
                         ChoiceDescription = cc.ChoiceDescription,
                         Val = cc.Val,
                         IsActive = cc.IsActive
                     }).ToList();
        }
        public ReturnMessageModel CreateEditConfigChoice(ConfigChoiceModel model)
        {
            try
            {
                var ccRow = _context.ConfigChoices.Where(x => x.ChoiceId == model.ChoiceId).FirstOrDefault();
                if (ccRow == null)
                {
                    ccRow = new ConfigChoice();
                }

                ccRow.CategoryId = model.CategoryId;
                ccRow.Choice = model.Choice;
                ccRow.ChoiceNepali = model.ChoiceNepali;
                ccRow.ChoiceDescription = model.ChoiceDescription;
                ccRow.Val = model.Val;
                ccRow.IsActive = model.IsActive;
                if (ccRow.ChoiceId == 0)
                {
                    _context.ConfigChoices.Add(ccRow);
                    _context.SaveChanges();
                }
                else
                {
                    _context.ConfigChoices.Attach(ccRow);
                    _context.Entry(ccRow).State = EntityState.Modified;
                    _context.SaveChanges();
                }

                rModel.Msg = "ConfigChoice Saved Successfully!!";
                rModel.Success = true;
                return rModel;
            }
            catch (Exception ex)
            {
                rModel.Msg = "ConfigChoice Save Failed!!";
                rModel.Success = false;
                return rModel;
            }
        }
        #endregion



        //roles
        #region roles
        public List<RoleModel> GetRoleList()
        {
            List<RoleModel> rList = _context.Database.SqlQuery<RoleModel>("Select RoleId,RoleName from webpages_Roles").ToList();
            return rList;

        }

        public ReturnMessageModel CreateEditUserRoles(RoleModel roModel)
        {
            try
            {

                if (roModel.RoleId == 0)
                {
                    _context.Database.ExecuteSqlCommand("insert into webpages_Roles (RoleName) VALUES ('" + roModel.RoleName + "')");
                    // var role = _context.Database.SqlQuery<RoleModel>("select * from webpages_Roles where UniqueId='" + roModel.UniqueId + "'").FirstOrDefault();
                    rModel.Msg = "Role Successfully Saved!";
                }
                else
                {
                    _context.Database.ExecuteSqlCommand("update webpages_Roles set [RoleName]='" + roModel.RoleName + "' where RoleId= " + roModel.RoleId);
                    //var role = _context.Database.SqlQuery<RoleModel>("select * from webpages_Roles where UniqueId='" + roModel.UniqueId + "'").FirstOrDefault();
                    rModel.Msg = "Role Updated Successfully!";
                }
                rModel.Success = true;
                return rModel;
            }

            catch (Exception ex)
            {
                rModel.Msg = " Saving Role Failed!!";
                rModel.Success = false;
                return rModel;
            }
        }
        #endregion



        #region Grade
        public List<GradeModel> GetGradeList()
        {
            return _context.Grades.Select(x => new GradeModel
            {
                GradeId = x.GradeId,
                GradeNameEng = x.GradeNameEng,
                GradeNameNep = x.GradeNameNep,
                Remarks = x.Remarks,
                IsActive = x.IsActive,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate

            }).ToList();
        }


        public ReturnMessageModel CreateGrade(GradeModel gModel)
        {
            try
            {
                var gRow = _context.Grades.Where(x => x.GradeId == gModel.GradeId).FirstOrDefault();
                if (gRow == null)
                {
                    gRow = new Grade();
                }

                gRow.GradeNameEng = gModel.GradeNameEng;
                gRow.GradeNameNep = gModel.GradeNameNep;
                gRow.Remarks = gModel.Remarks;
                gRow.IsActive = gModel.IsActive;


                if (gRow.GradeId == 0)
                {
                    gRow.CreatedBy = WebSecurity.CurrentUserId;
                    gRow.CreatedDate = System.DateTime.Now;
                    _context.Grades.Add(gRow);
                    _context.SaveChanges();

                }
                else
                {
                    gRow.ModifiedBy = WebSecurity.CurrentUserId;
                    gRow.ModifiedDate = System.DateTime.Now;
                    _context.Grades.Attach(gRow);
                    _context.Entry(gRow).State = EntityState.Modified;
                    _context.SaveChanges();
                }

                rModel.Msg = "Grade Saved Successfully";
                rModel.Success = true;
                return rModel;
            }
            catch (Exception ex)
            {
                rModel.Msg = "Grade Saved Failed";
                rModel.Success = false;
                return rModel;

            }

        }

        #endregion



        #region Subject
        public List<SubjectModel> GetSubjectList()
        {
            return _context.Subjects.Select(x => new SubjectModel
            {
                SubjectId = x.SubjectId,
                SubjectName = x.SubjectName,
                IsActive = x.IsActive,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate

            }).ToList();
        }


        public ReturnMessageModel CreateSubject(SubjectModel sModel)
        {
            try
            {
                var sRow = _context.Subjects.Where(x => x.SubjectId == sModel.SubjectId).FirstOrDefault();
                if (sRow == null)
                {
                    sRow = new Subject();
                }

                sRow.SubjectName = sModel.SubjectName;
                sRow.IsActive = sModel.IsActive;


                if (sRow.SubjectId == 0)
                {
                    sRow.CreatedBy = WebSecurity.CurrentUserId;
                    sRow.CreatedDate = System.DateTime.Now;
                    _context.Subjects.Add(sRow);
                    _context.SaveChanges();

                }
                else
                {
                    sRow.ModifiedBy = WebSecurity.CurrentUserId;
                    sRow.ModifiedDate = System.DateTime.Now;
                    _context.Subjects.Attach(sRow);
                    _context.Entry(sRow).State = EntityState.Modified;
                    _context.SaveChanges();
                }

                rModel.Msg = "Subject Saved Successfully";
                rModel.Success = true;
                return rModel;
            }
            catch (Exception ex)
            {
                rModel.Msg = "Subject Saved Failed";
                rModel.Success = false;
                return rModel;

            }

        }

        #endregion



        #region ResourceSetup

        public List<ResourceSetupModel> GetResourceSetupList()
        {
            return _context.ResourceSetups.Select(x => new ResourceSetupModel
            {
                ResourceId = x.ResourceId,
                ResourceTypeId = x.ResourceTypeId,
                ResourceName = x.ResourceName,
                PublicationId = x.PublicationId,
                PublicationName = x.ResourcePublication.Publisher ?? x.PublicationName,
                AuthorId = x.AuthorId,
                AuthorName = x.ResourceAuthor.Author ?? x.AuthorName,
                Remarks = x.Remarks,
                GradeId = x.GradeId,
                SubjectId = x.SubjectId,
                IsActive = x.IsActive,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate,

                Publisher = x.ResourcePublication.Publisher,
                ResourceTypeName = x.ResourceType.ResourceTypeName,
                Author = x.ResourceAuthor.Author

            }).ToList();
        }


        public ReturnMessageModel CreateResourceSetup(ResourceSetupModel rsModel)
        {
            try
            {
                var rRow = _context.ResourceSetups.Where(x => x.ResourceId == rsModel.ResourceId).FirstOrDefault();
                if (rRow == null)
                {
                    rRow = new ResourceSetup();
                }

                rRow.ResourceTypeId = rsModel.ResourceTypeId;
                rRow.ResourceName = rsModel.ResourceName;
                rRow.PublicationId = rsModel.PublicationId;
                rRow.PublicationName = rsModel.PublicationName;
                rRow.AuthorId = rsModel.AuthorId;
                rRow.AuthorName = rsModel.AuthorName;
                rRow.Remarks = rsModel.Remarks;
                rRow.GradeId = rsModel.GradeId;
                rRow.SubjectId = rsModel.SubjectId;
                rRow.IsActive = rsModel.IsActive;


                if (rRow.ResourceId == 0)
                {
                    rRow.CreatedBy = WebSecurity.CurrentUserId;
                    rRow.CreatedDate = System.DateTime.Now;
                    _context.ResourceSetups.Add(rRow);
                    _context.SaveChanges();

                }
                else
                {
                    rRow.ModifiedBy = WebSecurity.CurrentUserId;
                    rRow.ModifiedDate = System.DateTime.Now;
                    _context.ResourceSetups.Attach(rRow);
                    _context.Entry(rRow).State = EntityState.Modified;
                    _context.SaveChanges();
                }

                rModel.Msg = "Resource Saved Successfully";
                rModel.Success = true;
                return rModel;
            }
            catch (Exception ex)
            {
                rModel.Msg = "Resource Saved Failed ";
                rModel.Success = false;
                return rModel;

            }

        }

        #endregion



        #region ResourceSubscriber

        public List<ResourceSubscriberModel> GetResourceSubscriberList()
        {
            var result = _context.ResourceSubscribers.Select(x => new ResourceSubscriberModel
            {
                SubscriberId = x.SubscriberId,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                MembershipNumber = x.MembershipNumber,
                MemberDate = x.MemberDate,
                IsStudent = x.IsStudent,
                IsEmployee = x.IsEmployee,
                IsActive = x.IsActive,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate

            }).ToList();

            foreach (var item in result)
            {
                if (item.MiddleName != null)
                {
                    item.FullName = item.FirstName + " " + item.MiddleName + " " + item.LastName;
                }
                else
                {
                    item.FullName = item.FirstName + " " + item.LastName;

                }
            }
            return result;
        }


        public ReturnMessageModel CreateResourceSubscriber(ResourceSubscriberModel rsbModel)
        {
            try
            {
                var rRow = _context.ResourceSubscribers.Where(x => x.SubscriberId == rsbModel.SubscriberId).FirstOrDefault();
                if (rRow == null)
                {
                    rRow = new ResourceSubscriber();
                }

                rRow.FirstName = rsbModel.FirstName;
                rRow.MiddleName = rsbModel.MiddleName;
                rRow.LastName = rsbModel.LastName;
                rRow.MembershipNumber = rsbModel.MembershipNumber;
                rRow.MemberDate = CommonService.GetEnglishDate(rsbModel.MemberDateNepali);  
                rRow.IsStudent = rsbModel.IsStudent;
                rRow.IsEmployee = rsbModel.IsEmployee;
                rRow.IsActive = rsbModel.IsActive;


                if (rRow.SubscriberId == 0)
                {
                    rRow.CreatedBy = WebSecurity.CurrentUserId;
                    rRow.CreatedDate = System.DateTime.Now;
                    _context.ResourceSubscribers.Add(rRow);
                    _context.SaveChanges();

                }
                else
                {
                    rRow.ModifiedBy = WebSecurity.CurrentUserId;
                    rRow.ModifiedDate = System.DateTime.Now;
                    _context.ResourceSubscribers.Attach(rRow);
                    _context.Entry(rRow).State = EntityState.Modified;
                    _context.SaveChanges();
                }

                rModel.Msg = "Subscriber Saved Successfully";
                rModel.Success = true;
                return rModel;
            }
            catch (Exception ex)
            {
                rModel.Msg = "Subscriber Saved Failed";
                rModel.Success = false;
                return rModel;

            }

        }

        #endregion



        #region ResourceGeneration

        public List<ResourceGenerationModel> GetResourceGenerationList()
        {
            return _context.ResourceGenerations.Select(x => new ResourceGenerationModel
            {
                GenerationId = x.GenerationId,
                ResourceId = x.ResourceId,
                GenerationDate = x.GenerationDate,
                GenerationCopyCount = x.GenerationCopyCount,
                Remarks = x.Remarks,
                IsActive = x.IsActive,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate,

                ResourceName = x.ResourceSetup.ResourceName




            }).ToList();
        }


        public ReturnMessageModel CreateResourceGeneration(ResourceGenerationModel rgModel)
        {
            try
            {
                var rRow = _context.ResourceGenerations.Where(x => x.GenerationId == rgModel.GenerationId).FirstOrDefault();
                if (rRow == null)
                {
                    rRow = new ResourceGeneration();
                }

                rRow.ResourceId = rgModel.ResourceId;
                rRow.GenerationDate = rgModel.GenerationDate;
                rRow.GenerationCopyCount = rgModel.GenerationCopyCount;
                rRow.Remarks = rgModel.Remarks;
                rRow.IsActive = rgModel.IsActive;


                if (rRow.GenerationId == 0)
                {
                    rRow.CreatedBy = WebSecurity.CurrentUserId;
                    rRow.CreatedDate = System.DateTime.Now;
                    _context.ResourceGenerations.Add(rRow);
                    _context.SaveChanges();

                }
                else
                {
                    rRow.ModifiedBy = WebSecurity.CurrentUserId;
                    rRow.ModifiedDate = System.DateTime.Now;
                    _context.ResourceGenerations.Attach(rRow);
                    _context.Entry(rRow).State = EntityState.Modified;
                    _context.SaveChanges();
                }

                rModel.Msg = "Resource Generation Saved Successfully";
                rModel.Success = true;
                return rModel;
            }
            catch (Exception ex)
            {
                rModel.Msg = "Resource Generation Saved Failed ";
                rModel.Success = false;
                return rModel;

            }

        }

        #endregion



        #region ResourceCopy

        public List<ResourceCopyModel> GetResourceCopyList()
        {
            return _context.ResourceCopies.Select(x => new ResourceCopyModel
            {
                ResourceCopyId = x.ResourceCopyId,
                GenerationId = x.ResourceGeneration.GenerationId,
                ResourceId = x.ResourceSetup.ResourceId,
                ResourceCopyCount = x.ResourceCopyCount,
                ResourceCopyNumber = x.ResourceCopyNumber,
                PublishedDate = x.PublishedDate,
                Edition = x.Edition,
                Remarks = x.Remarks,
                IsAvailable = x.IsAvailable,
                IsActive = x.ResourceGeneration.IsActive,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate,
                ResourceName = x.ResourceSetup.ResourceName,


            }).ToList();
        }


        public ReturnMessageModel CreateResourceCopy(ResourceCopyModel rcModel)
        {
            try
            {
                var rRow = _context.ResourceCopies.Where(x => x.ResourceCopyId == rcModel.ResourceCopyId).FirstOrDefault();
                if (rRow == null)
                {
                    rRow = new ResourceCopy();
                }

                rRow.GenerationId = rcModel.GenerationId;
                rRow.ResourceId = rcModel.ResourceId;
                rRow.ResourceCopyCount = rcModel.ResourceCopyCount;
                rRow.ResourceCopyNumber = rcModel.ResourceCopyNumber;
                rRow.Remarks = rcModel.Remarks;
                rRow.IsAvailable = rcModel.IsAvailable;
                rRow.PublishedDate = rcModel.PublishedDate;
                rRow.Edition = rcModel.Edition;
                rRow.IsActive = rcModel.IsActive;



                if (rRow.ResourceCopyId == 0)
                {
                    rRow.CreatedBy = WebSecurity.CurrentUserId;
                    rRow.CreatedDate = System.DateTime.Now;
                    _context.ResourceCopies.Add(rRow);
                    _context.SaveChanges();

                }
                else
                {
                    rRow.ModifiedBy = WebSecurity.CurrentUserId;
                    rRow.ModifiedDate = System.DateTime.Now;
                    _context.ResourceCopies.Attach(rRow);
                    _context.Entry(rRow).State = EntityState.Modified;
                    _context.SaveChanges();
                }

                rModel.Msg = "Resource Copy Saved Successfully";
                rModel.Success = true;
                return rModel;
            }
            catch (Exception ex)
            {
                rModel.Msg = "Resource Copy Saved Failed ";
                rModel.Success = false;
                return rModel;

            }

        }

        #endregion



        #region ResourceIssue

        public List<ResourceIssueModel> GetResourceIssueList()
        {
            return _context.Database.SqlQuery<ResourceIssueModel>("SpGetResourceIssues").ToList();

        }


        public ReturnMessageModel CreateResourceIssue(ResourceIssueModel riModel)
        {
            try
            {
                var rRow = _context.ResourceIssues.Where(x => x.IssueId == riModel.IssueId).FirstOrDefault();
                if (rRow == null)
                {
                    rRow = new ResourceIssue();
                }

                rRow.ResourceCopyId = riModel.ResourceCopyId;
                rRow.IssueDate = CommonService.GetEnglishDate(riModel.IssueDateNepali);
                rRow.ReturnBackDate = CommonService.GetEnglishDate(riModel.ReturnBackDateNepali);
                rRow.Remarks = riModel.Remarks;
                rRow.Subscriber = riModel.SubscriberId;
                rRow.IsActive = riModel.IsActive;


                if (rRow.IssueId == 0)
                {
                    rRow.CreatedBy = WebSecurity.CurrentUserId;
                    rRow.CreatedDate = System.DateTime.Now;
                    _context.ResourceIssues.Add(rRow);
                    _context.SaveChanges();
                }
                else
                {
                    rRow.ModifiedBy = WebSecurity.CurrentUserId;
                    rRow.ModifiedDate = System.DateTime.Now;
                    _context.ResourceIssues.Attach(rRow);
                    _context.Entry(rRow).State = EntityState.Modified;
                    _context.SaveChanges();
                }

                rModel.Msg = "Resource Issued Successfully";
                rModel.Success = true;
                return rModel;
            }
            catch (Exception ex)
            {
                rModel.Msg = "Resource Issue Failed ";
                rModel.Success = false;
                return rModel;
            }

        }


        public List<ResourceIssueModel> GetSearchedResourceCopy(ResourceIssueModel riModel)
        {
            using (PointOfSaleEntities context = new PointOfSaleEntities())
            {
                return _context.Database.SqlQuery<ResourceIssueModel>("SpGetResourceCopy @resourceCopyId,@authorId,@publicationId,@gradeId,@subjectId",
                    new SqlParameter("resourceCopyId", riModel.ResourceId),
                    new SqlParameter("authorId", riModel.AuthorId ?? SqlInt32.Null),
                    new SqlParameter("publicationId", riModel.PublicationId ?? SqlInt32.Null),
                    new SqlParameter("gradeId", riModel.GradeId ?? SqlInt32.Null),
                    new SqlParameter("subjectId", riModel.SubjectId ?? SqlInt32.Null)
                    ).ToList();
            }
        }

        #endregion


        //#region ResourceReturn

        //public List<ResourceReturnModel> GetResourceReturnList()
        //{
        //    return _context.Database.SqlQuery<ResourceReturnModel>("SpGetResourceReturns").ToList();

        //}


        //public ReturnMessageModel ResourceReturn(ResourceReturnModel riModel)
        //{
        //    try
        //    {
        //        var rRow = _context.ResourceIssues.Where(x => x.IssueId == riModel.IssueId).FirstOrDefault();
        //        if (rRow == null)
        //        {
        //            rRow = new ResourceIssue();
        //        }

        //        rRow.ResourceCopyId = riModel.ResourceCopyId;
        //        rRow.IssueDate = CommonService.GetEnglishDate(riModel.IssueDateNepali);
        //        rRow.ReturnBackDate = CommonService.GetEnglishDate(riModel.ReturnBackDateNepali);
        //        rRow.Remarks = riModel.Remarks;
        //        rRow.Subscriber = riModel.SubscriberId;
        //        rRow.IsActive = riModel.IsActive;


        //        if (rRow.IssueId == 0)
        //        {
        //            rRow.CreatedBy = WebSecurity.CurrentUserId;
        //            rRow.CreatedDate = System.DateTime.Now;
        //            _context.ResourceIssues.Add(rRow);
        //            _context.SaveChanges();
        //        }
        //        else
        //        {
        //            rRow.ModifiedBy = WebSecurity.CurrentUserId;
        //            rRow.ModifiedDate = System.DateTime.Now;
        //            _context.ResourceIssues.Attach(rRow);
        //            _context.Entry(rRow).State = EntityState.Modified;
        //            _context.SaveChanges();
        //        }

        //        rModel.Msg = "Resource Issued Successfully";
        //        rModel.Success = true;
        //        return rModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        rModel.Msg = "Resource Issue Failed ";
        //        rModel.Success = false;
        //        return rModel;
        //    }

        //}

        //#endregion

    }
}
