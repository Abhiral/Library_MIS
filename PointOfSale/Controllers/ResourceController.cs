using POSModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POSBLL.Interface;
using POSBLL.Services;
using POSDAL;
using POSBLL;


namespace PointOfSale.Controllers
{
    public class ResourceController : Controller
    {
        IResourceInterface _iResourceService;
        ICountryInterface _iCountry;
        ISetupService _iSetupService;
        IResourceAuthorInterface _iResourceAuthorService;
        IResourcePublicationInterface _iResourcePublicationService;
        IGenerationInterface _iResourceGenerationService;
        ReturnMessageModel rModel;
 
        public ResourceController()
        {
            _iResourceService = new ResourceService();
            _iCountry = new CountryService();
            _iResourceAuthorService = new ResourceAuthorService();
            _iResourcePublicationService = new ResourcePublicationService();
            _iResourceGenerationService = new GenerationService();
            rModel = new ReturnMessageModel();
            _iSetupService = new SetupService();
           
        }

        #region Resource type
        public ActionResult ResourceTypeIndex()
        {
            ResourceTypeModel db = new ResourceTypeModel();
            db.ResourceTypeList = _iResourceService.GetResourceTypeList();
            return View(db);
        }
 
        public ActionResult CreateEditResourceType(int? resourceTypeId)
        {
            ResourceTypeModel rtModel = new ResourceTypeModel();
            if (resourceTypeId != null)
            {
                rtModel = _iResourceService.GetResourceTypeList().Where(x => x.ResourceTypeId == resourceTypeId).FirstOrDefault();
            }
            return PartialView("_CreateEditResourceType",rtModel);
        }

        

        [HttpPost]
        public ActionResult CreateEditResourceType(ResourceTypeModel rtModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iResourceService.CreateEditResourceType(rtModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Country

        public ActionResult CountryIndex()
        {
            CountryModel db = new CountryModel();
            db.CountryList = _iCountry.GetCountryList();
            return View(db);
        }

        public ActionResult CreateEditCountry(int? CountryId)
        {
            CountryModel cModel = new CountryModel();
            if (CountryId != null)
            {
                cModel = _iCountry.GetCountryList().Where(x => x.CountryId == CountryId).FirstOrDefault();
            }
            return PartialView("_CreatedEditCountry", cModel);
        }

        [HttpPost]
        public ActionResult CreateEditCountry(CountryModel cModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iCountry.CreateEditCountry(cModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Resource Author

        public ActionResult ResourceAuthorIndex()
        {
            ResourceAuthorModel db = new ResourceAuthorModel();
            db.ResourceAuthorList = _iResourceAuthorService.GetResourceAuthorList();
            return View(db);
        }

       
        public ActionResult CreateEditResourceAuthor(int? AuthorId)
        {
            ResourceAuthorModel raModel = new ResourceAuthorModel();
            
            ViewBag.CountryName = new SelectList(_iCountry.GetCountryList(), "CountryId", "CountryName");

            if (AuthorId != null)
            {
                raModel = _iResourceAuthorService.GetResourceAuthorList().Where(x => x.AuthorId == AuthorId).FirstOrDefault();
            }
           
            return PartialView("_CreateEditResourceAuthor", raModel);
        }


        [HttpPost]
        public ActionResult CreateEditResourceAuthor(ResourceAuthorModel raModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iResourceAuthorService.CreateEditResourceAuthor(raModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }
        }
       
        #endregion

        #region Resource Publication 
        public ActionResult ResourcePublicationIndex()
        {
            ResourcePublicationModel db = new ResourcePublicationModel();
            db.ResourcePublicationList = _iResourcePublicationService.GetResourcePublicationList();
            return View(db);
        }

        public ActionResult CreateEditResourcePublication(int? PublicationId)
        {
            ResourcePublicationModel rpModel = new ResourcePublicationModel();

           
            ViewBag.CountryName = new SelectList(_iCountry.GetCountryList(), "CountryId", "CountryName");

            if (PublicationId != null)
            {
                rpModel = _iResourcePublicationService.GetResourcePublicationList().Where(x => x.PublicationId == PublicationId).FirstOrDefault();
            }
            return PartialView("_CreateEditResourcePublication", rpModel);
        }

        [HttpPost]
        public ActionResult CreateEditResourcePublication(ResourcePublicationModel rpModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iResourcePublicationService.CreateEditResourcePublication(rpModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

       // #region Generation
        //public ActionResult ResourceGenerationIndex()
        //{
        //    ResourceGenerationModel db = new ResourceGenerationModel();
        //    db.GetResourceGenerationList = _iResourceGenerationService.GetResourceGenerationList();
        //    return View(db);
        //}


        //public ActionResult CreateResourceGeneration(int GenerationId)
        //{
        //    ResourceGenerationModel rgModel = new ResourceGenerationModel();
        //    ViewBag.ResourceName = new SelectList(_iResourceService.GetResourceTypeList(), "ResourceId", "ResourceName");
        //    return PartialView("_CreateResourceGeneration", rgModel);
        //}

        //[HttpPost]
        //public ActionResult CreateResourceGeneration(ResourceGenerationModel rgModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = _iResourceGenerationService.CreateResourceGeneration(rgModel);
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(rModel, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //#endregion

       


    }
}
