
using POSModel;
using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using POSBLL;
using System.IO;
using System.Text.RegularExpressions;
using POSDAL;
using POSBLL.Interface;
using POSBLL.Services;

namespace PointOfSale.Controllers
{
    [Authorize]
    public class SetupController : Controller
    {
        ISetupService _iSetupService;
        IResourceInterface _iResourceService;
        ICountryInterface _iCountryService;
        IResourceAuthorInterface _iResourceAuthorService;
        IResourcePublicationInterface _iResourcePublicationService;


        ReturnMessageModel rModel;
        PointOfSaleEntities context;

        public SetupController()
        {
            _iSetupService = new SetupService();
            rModel = new ReturnMessageModel();
            _iResourceService = new ResourceService();
            context = new PointOfSaleEntities();
            _iCountryService = new CountryService();
            context = new PointOfSaleEntities();
            _iResourceAuthorService = new ResourceAuthorService();
            context = new PointOfSaleEntities();
            _iResourcePublicationService = new ResourcePublicationService();
            context = new PointOfSaleEntities();
        }

        //ConfigChoiceCategory
        #region ConfigChoiceCategory
        public ActionResult ConfigChoiceCategoryList()
        {
            ConfigChoiceCategoryModel cccModel = new ConfigChoiceCategoryModel();
            cccModel.ConfigChoiceCategoryList = _iSetupService.GetConfigChoiceCategoryList();
            return View(cccModel);
        }

        public ActionResult CreateEditConfigChoiceCategory(int? CategoryId)
        {
            ConfigChoiceCategoryModel cccModel = new ConfigChoiceCategoryModel();
            if (CategoryId != null)
            {
                cccModel = _iSetupService.GetConfigChoiceCategoryList().Where(x => x.CategoryId == CategoryId).FirstOrDefault();
            }
            return PartialView("_CreateEditConfigChoiceCategory", cccModel);
        }

        [HttpPost]
        public ActionResult CreateEditConfigChoiceCategory(ConfigChoiceCategoryModel cccModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iSetupService.CreateEditConfigChoiceCategory(cccModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        //ConfigChoice
        #region ConfigChoice
        public ActionResult ConfigChoiceList(int catId)
        {
            ConfigChoiceModel cccModel = new ConfigChoiceModel();
            cccModel.ConfigChoiceList = _iSetupService.GetConfigChoiceList(catId);
            cccModel.CategoryId = catId;
            cccModel.CategoryName = CommonService.GetConfigChoiceCategoryById(catId).Category;
            return View(cccModel);
        }

        public ActionResult CreateEditConfigChoice(int categoryId, int? choiceId)
        {
            ConfigChoiceModel cccModel = new ConfigChoiceModel();
            if (choiceId != null)
            {
                cccModel = _iSetupService.GetConfigChoiceList(categoryId).Where(x => x.ChoiceId == choiceId).FirstOrDefault();
            }
            return PartialView("_CreateEditConfigChoice", cccModel);
        }

        [HttpPost]
        public ActionResult CreateEditConfigChoice(ConfigChoiceModel cccModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iSetupService.CreateEditConfigChoice(cccModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion





        //User Roles Section
        #region UserRoles
        public ActionResult UserRolesIndex()
        {
            RoleModel roModel = new RoleModel();
            roModel.RoleList = _iSetupService.GetRoleList();
            return View(roModel);
        }
        public ActionResult CreateEditUserRoles(int? roleId)
        {

            RoleModel roModel = new RoleModel();
            if (roleId != null)
            {
                roModel = _iSetupService.GetRoleList().Where(x => x.RoleId == roleId).FirstOrDefault();
            }
            return PartialView("_CreateEditRole", roModel);

        }
        [HttpPost]
        public ActionResult CreateEditUserRoles(RoleModel roModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iSetupService.CreateEditUserRoles(roModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion



        #region Grade
        public ActionResult GradeIndex()
        {
            GradeModel gModel = new GradeModel();
            gModel.GradeList = _iSetupService.GetGradeList();

            return View(gModel);
        }

        public ActionResult CreateGrade(int? gradeId)
        {
            GradeModel gModel = new GradeModel();
            if (gradeId != null)
            {
                gModel = _iSetupService.GetGradeList().Where(x => x.GradeId == gradeId).FirstOrDefault();
            }
            return PartialView("_CreateEditGrade", gModel);
        }

        [HttpPost]
        public ActionResult CreateGrade(GradeModel gModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iSetupService.CreateGrade(gModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion
         

        #region Subject

        public ActionResult SubjectIndex()
        {
            SubjectModel sModel = new SubjectModel();
            sModel.SubjectList = _iSetupService.GetSubjectList();

            return View(sModel);
        }

        public ActionResult CreateSubject(int? SubjectId)
        {
            SubjectModel sModel = new SubjectModel();
            if (SubjectId != null)
            {
                sModel = _iSetupService.GetSubjectList().Where(x => x.SubjectId == SubjectId).FirstOrDefault();
            }
            return PartialView("_CreateEditSubject", sModel);
        }

        [HttpPost]
        public ActionResult CreateSubject(SubjectModel sModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iSetupService.CreateSubject(sModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region ResourceSetup

        public ActionResult ResourceSetupIndex()
        {
            ResourceSetupModel rsModel = new ResourceSetupModel();
            rsModel.ResourceSetupList = _iSetupService.GetResourceSetupList();

            return View(rsModel);
        }

        public ActionResult CreateResourceSetup(int? ResourceId)
        {
            ResourceSetupModel rsModel = new ResourceSetupModel();


            ViewBag.ResourceTypeNameList = new SelectList(_iResourceService.GetResourceTypeList(), "ResourceTypeId", "ResourceTypeName");
            ViewBag.ResourcePublisherList = new SelectList(_iResourcePublicationService.GetResourcePublicationList(), "PublicationId", "Publisher");
            ViewBag.ResourceAuthorList = new SelectList(_iResourceAuthorService.GetResourceAuthorList(), "AuthorId", "Author");
            ViewBag.ResourceGradeNameList = new SelectList(_iSetupService.GetGradeList(), "GradeId", "GradeNameEng");
            ViewBag.ResourceSubjectList = new SelectList(_iSetupService.GetSubjectList(), "SubjectId", "SubjectName");



            if (ResourceId != null)
            {
                rsModel = _iSetupService.GetResourceSetupList().Where(x => x.ResourceId == ResourceId).FirstOrDefault();
                //if (rsModel.AuthorId > 0)
                //{
                //    rsModel.IsAuthor = true;
                //}
                //else
                //{
                //    rsModel.IsAuthor = false;
                //}

                //if (rsModel.PublicationId > 0)
                //{
                //    rsModel.IsPublisher = true;
                //}
                //else
                //{
                //    rsModel.IsPublisher = false;
                //}

                if (rsModel.AuthorId == null)
                {
                    rsModel.IsAuthor = false;
                }
                if (rsModel.PublicationId == null)
                {
                    rsModel.IsPublisher = false;
                }

            }
            return PartialView("_CreateEditResourceSetup", rsModel);
        }

        [HttpPost]
        public ActionResult CreateResourceSetup(ResourceSetupModel rsModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iSetupService.CreateResourceSetup(rsModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region ResourceSubscriber

        public ActionResult ResourceSubscriberIndex()
        {
            ResourceSubscriberModel rsbModel = new ResourceSubscriberModel();
            rsbModel.ResourceSubscriberList = _iSetupService.GetResourceSubscriberList();

            return View(rsbModel);
        }

        public ActionResult CreateResourceSubscriber(int? SubscriberId)
        {
            ResourceSubscriberModel rsbModel = new ResourceSubscriberModel();
            
            if (SubscriberId != null)
            {
                rsbModel = _iSetupService.GetResourceSubscriberList().Where(x => x.SubscriberId == SubscriberId).FirstOrDefault();
                rsbModel.MemberDateNepali = CommonService.GetCurrentNepaliDate(rsbModel.MemberDate);
            }
            else
            {
                rsbModel.MemberDateNepali = CommonService.GetCurrentNepaliDate(DateTime.Now);
            }
            return PartialView("_CreateEditResourceSubscriber", rsbModel);
        }

        [HttpPost]
        public ActionResult CreateResourceSubscriber(ResourceSubscriberModel rsbModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iSetupService.CreateResourceSubscriber(rsbModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(rsbModel, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region ResourceGeneration

        public ActionResult ResourceGenerationIndex()
        {
            ResourceGenerationModel rgModel = new ResourceGenerationModel();
            rgModel.GetResourceGenerationList = _iSetupService.GetResourceGenerationList();

            return View(rgModel);
        }

        public ActionResult CreateResourceGeneration(int GenerationId)
        {
            ResourceGenerationModel rgModel = new ResourceGenerationModel();
            rgModel.ResourceName = _iSetupService.GetResourceGenerationList().Where(x => x.GenerationId == GenerationId).FirstOrDefault().ResourceName;
            rgModel.GenerationDateNepali = CommonService.GetCurrentNepaliDate(_iSetupService.GetResourceGenerationList().Where(x => x.GenerationId == GenerationId).FirstOrDefault().GenerationDate);
            rgModel.GenerationCopyCount = _iSetupService.GetResourceGenerationList().Where(x => x.GenerationId == GenerationId).FirstOrDefault().GenerationCopyCount;
            rgModel.ResourceId = _iSetupService.GetResourceGenerationList().Where(x => x.GenerationId == GenerationId).FirstOrDefault().ResourceId;
            //rgModel.ResourceId = resourceId;
            //ViewBag.ResourceNameList = new SelectList(_iSetupService.GetResourceSetupList(), "ResourceId", "ResourceName");
            //if (GenerationId != null)
            //{
            //    rgModel = _iSetupService.GetResourceGenerationList().Where(x => x.GenerationId == GenerationId).FirstOrDefault();
            //}
            //rgModel.GenerationDateNepali = CommonService.GetCurrentNepaliDate(DateTime.Now);
            return PartialView("_EditResourceGeneration", rgModel);
        }

        [HttpPost]
        public ActionResult CreateResourceGeneration(ResourceGenerationModel rgModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iSetupService.CreateResourceGeneration(rgModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region GenerateCopies

        public ActionResult GenerateResourceCopies(int resourceId)
        {
            ResourceGenerationModel rgModel = new ResourceGenerationModel();
            rgModel.ResourceName = _iSetupService.GetResourceSetupList().Where(x => x.ResourceId == resourceId).FirstOrDefault().ResourceName;
            rgModel.ResourceId = resourceId;
            rgModel.GenerationDateNepali = CommonService.GetCurrentNepaliDate(DateTime.Now);
            return PartialView("_GenerateResourceCopies", rgModel);
        }

        [HttpPost]
        public ActionResult CreateGenerationCopies(ResourceGenerationModel rgModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iResourceService.CreateResourceCopies(rgModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);

            }
        }

        #endregion


        #region ResourceCopy

        public ActionResult ResourceCopyIndex(int generationId)
        {
            ResourceCopyModel rcModel = new ResourceCopyModel();
            rcModel.ResourceCopyList = _iSetupService.GetResourceCopyList().Where(x => x.GenerationId == generationId).ToList();

            return View(rcModel);
        }

        public ActionResult CreateResourceCopy(int? ResourceCopyId)
        {
            ResourceCopyModel rcModel = new ResourceCopyModel();


            ViewBag.ResourceGenerationList = new SelectList(_iSetupService.GetResourceGenerationList(), "GenerationId", " ");
            ViewBag.ResourceNameList = new SelectList(_iSetupService.GetResourceSetupList(), "ResourceId", "ResourceName");



            if (ResourceCopyId != null)
            {
                rcModel = _iSetupService.GetResourceCopyList().Where(x => x.ResourceCopyId == ResourceCopyId).FirstOrDefault();
            }
            return PartialView("_CreateEditResourceCopy", rcModel);
        }

        [HttpPost]
        public ActionResult CreateResourceCopy(ResourceCopyModel rcModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iSetupService.CreateResourceCopy(rcModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        //#region ResourceIssue

        //public ActionResult ResourceIssueIndex()
        //{
        //    ResourceIssueModel riModel = new ResourceIssueModel();
        //    riModel.ResourceIssueList = _iSetupService.GetResourceIssueList();

        //    return View(riModel);
        //}

        //public ActionResult CreateResourceIssue(int? IssueId)
        //{
        //    ResourceIssueModel riModel = new ResourceIssueModel();


        //    ViewBag.ResourceCopyList = new SelectList(_iSetupService.GetResourceIssueList(), "ResourceCopyId", "ResourceCopyNumber");
        //    ViewBag.ResourceSubscriberList = new SelectList(_iSetupService.GetResourceSubscriberList(), "SubscriberId", "FirstName" );


        //    if (IssueId != null)
        //    {
        //        riModel = _iSetupService.GetResourceIssueList().Where(x => x.IssueId == IssueId).FirstOrDefault();
        //    }
        //    return PartialView("_CreateEditResourceIssue", riModel);
        //}

        //[HttpPost]
        //public ActionResult CreateResourceIssue(ResourceIssueModel riModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = _iSetupService.CreateResourceIssue(riModel);
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }

        //    else
        //    {
        //        return Json(rModel, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //#endregion


        #region ResourceIssue (from ResourceCopy)

        public ActionResult ResourceIssueIndex(ResourceIssueModel riModel)
        {

            ViewBag.ResourceNameList = new SelectList(_iSetupService.GetResourceSetupList(), "ResourceId", "ResourceName");
            ViewBag.ResourcePublicationList = new SelectList(Enumerable.Empty<SelectListItem>());
            ViewBag.ResourceAuthorList = new SelectList(Enumerable.Empty<SelectListItem>());
            ViewBag.ResourceGradeList = new SelectList(Enumerable.Empty<SelectListItem>());
            ViewBag.ResourceSubjectList = new SelectList(Enumerable.Empty<SelectListItem>());

            //ViewBag.ResourcePublicationList = new SelectList(_iResourcePublicationService.GetResourcePublicationList(), "PublicationId", "Publisher");
            //ViewBag.ResourceAuthorList = new SelectList(_iResourceAuthorService.GetResourceAuthorList(), "AuthorId", "Author");
            //ViewBag.ResourceGradeList = new SelectList(_iSetupService.GetGradeList(), "GradeId", "GradeNameEng");
            //ViewBag.ResourceSubjectList = new SelectList(_iSetupService.GetSubjectList(), "SubjectId", "SubjectName");


            if (riModel.ResourceId > 0)
            {
                riModel.ResourceIssueList = _iSetupService.GetSearchedResourceCopy(riModel).Where(x => x.IsActive && x.IsAvailable).ToList();
            }

            return View(riModel);
        }

        public ActionResult CreateResourceIssue(int resourceCopyId)
        {
            ResourceIssueModel riModel = new ResourceIssueModel();
            

            //ViewBag.ResourceCopyList = new SelectList(_iSetupService.GetResourceCopyList(), "ResourceCopyId", "ResourceCopyNumber");
            riModel.ResourceCopyNumber = _iSetupService.GetResourceCopyList().Where(x => x.ResourceCopyId == resourceCopyId).FirstOrDefault().ResourceCopyNumber;
            ViewBag.ResourceSubscriberList = new SelectList(_iSetupService.GetResourceSubscriberList(), "SubscriberId", "FullName");

            //rgModel.ResourceName = _iSetupService.GetResourceSetupList().Where(x => x.ResourceId == resourceCopyId).FirstOrDefault().ResourceName;
            riModel.ResourceCopyId = resourceCopyId;
            riModel.IssueDateNepali = CommonService.GetCurrentNepaliDate(DateTime.Now);
            riModel.ReturnBackDateNepali = CommonService.GetCurrentNepaliDate(DateTime.Now.AddDays(+7));
            return PartialView("_CreateEditResourceIssue", riModel);
        }

        [HttpPost]
        public ActionResult CreateResourceIssue(ResourceIssueModel riModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iSetupService.CreateResourceIssue(riModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);

            }
        }

        #endregion


        #region ResourceIssueSubscriber

        public ActionResult ResourceIssueSubscriber(ResourceIssueModel riModel)
        {
            ViewBag.ResourceSubscriberList = new SelectList(_iSetupService.GetResourceSubscriberList(), "SubscriberId", "FullName");

            if (riModel.SubscriberId > 0)
            {
                riModel.ResourceIssueList = _iSetupService.GetResourceIssueList().Where(x => x.SubscriberId == riModel.SubscriberId).ToList();

            }
            return View(riModel);
        }

        public ActionResult CreateResourceIssueSubscriber(int resourceCopyId, int subscriberId)
        {
            ResourceIssueModel riModel = new ResourceIssueModel();

            //ViewBag.ResourceCopyList = new SelectList(_iSetupService.GetResourceCopyList(), "ResourceCopyId", "ResourceCopyNumber");

            riModel.ResourceCopyNumber = _iSetupService.GetResourceCopyList().Where(x => x.ResourceCopyId == resourceCopyId).FirstOrDefault().ResourceCopyNumber;
            //ViewBag.ResourceSubscriberList = new SelectList(_iSetupService.GetResourceSubscriberList(), "SubscriberId", "FullName");
            riModel.FullName = _iSetupService.GetResourceSubscriberList().Where(x => x.SubscriberId == subscriberId).FirstOrDefault().FullName;

            //rgModel.ResourceName = _iSetupService.GetResourceSetupList().Where(x => x.ResourceId == resourceCopyId).FirstOrDefault().ResourceName;
            riModel.ResourceCopyId = resourceCopyId;
            riModel.SubscriberId = subscriberId;
            riModel.IssueDateNepali = CommonService.GetCurrentNepaliDate(DateTime.Now);
            riModel.ReturnBackDateNepali = CommonService.GetCurrentNepaliDate(DateTime.Now.AddDays(+7));
            return PartialView("_CreateEditResourceIssueSubscriber", riModel);
        }

        [HttpPost]
        public ActionResult CreateResourceIssueSubscriber(ResourceIssueModel riModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iSetupService.CreateResourceIssue(riModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);

            }
        }

        #endregion

        public ActionResult GetResourceWisePublication(int resourceId)
        {
            var publications = (from item in _iSetupService.GetResourceSetupList()
                                join pub in _iResourcePublicationService.GetResourcePublicationList() on item.PublicationId equals pub.PublicationId
                                where item.ResourceId == resourceId && item.PublicationId!=null
                                select new SelectModel { 
                                
                                SelectId=pub.PublicationId,
                                SelectText=pub.Publisher
                                
                                }).ToList();

            return Json(publications, JsonRequestBehavior.AllowGet);
                                
            
        }
    }

    //#region ResourceReturn

    //    public ActionResult ResourceReturn(int resourceCopyId, int subscriberId)
    //        {
               
    //        }

    //#endregion

}
