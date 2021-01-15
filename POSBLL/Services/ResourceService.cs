using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSBLL.Interface;
using POSModel;
using POSDAL;
using WebMatrix.WebData;
using System.Data;
namespace POSBLL.Services
{

    public class ResourceService : IResourceInterface
    {
        ReturnMessageModel rModel;
        public ResourceService()
        {
            rModel = new ReturnMessageModel();
        }

        #region Resource Type
        public List<ResourceTypeModel> GetResourceTypeList()
        {
            using (PointOfSaleEntities context = new PointOfSaleEntities())
            {
                var result = context.ResourceTypes.Select(x => new ResourceTypeModel
                {
                    ResourceTypeId = x.ResourceTypeId,
                    ResourceTypeName = x.ResourceTypeName,
                    ResourceTypeCode = x.ResourceTypeCode,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    ModifiedDate = x.ModifiedDate,
                    ModifiedBy = x.ModifiedBy
                }).ToList();
                return result;
            }
        }


        public ReturnMessageModel CreateEditResourceType(ResourceTypeModel rtModel)
        {
            try
            {
                using (PointOfSaleEntities _context = new PointOfSaleEntities())
                {
                    var rtRow = _context.ResourceTypes.Where(x => x.ResourceTypeId == rtModel.ResourceTypeId).FirstOrDefault();
                    if (rtRow == null)
                    {
                        rtRow = new ResourceType();
                    }

                    rtRow.ResourceTypeName = rtModel.ResourceTypeName;
                    rtRow.ResourceTypeCode = rtModel.ResourceTypeCode;
                    rtRow.IsActive = rtModel.IsActive;


                    if (rtModel.ResourceTypeId == 0)
                    {
                        rtRow.CreatedBy = WebSecurity.CurrentUserId;
                        rtRow.CreatedDate = System.DateTime.Now;
                        _context.ResourceTypes.Add(rtRow);
                        _context.SaveChanges();

                    }
                    else
                    {
                        rtRow.ModifiedBy = WebSecurity.CurrentUserId;
                        rtRow.ModifiedDate = System.DateTime.Now;
                        _context.ResourceTypes.Attach(rtRow);
                        _context.Entry(rtRow).State = EntityState.Modified;
                        _context.SaveChanges();
                    }

                    rModel.Msg = "Resource Type Saved Successfully";
                    rModel.Success = true;
                    return rModel;
                }


            }
            catch (Exception ex)
            {
                rModel.Msg = "Resource Type Saved Failed";
                rModel.Success = false;
                return rModel;

            }

        }
        #endregion


        public ReturnMessageModel CreateResourceCopies(ResourceGenerationModel rgModel)
        {
            try
            {
                using (PointOfSaleEntities context = new PointOfSaleEntities())
                {

                    var gRow = new ResourceGeneration();
                    gRow.ResourceId = rgModel.ResourceId;
                    gRow.Remarks = rgModel.Remarks;
                    gRow.GenerationCopyCount = rgModel.GenerationCopyCount;
                    gRow.GenerationDate =  CommonService.GetEnglishDate(rgModel.GenerationDateNepali);
                    gRow.IsActive = rgModel.IsActive;
                    context.ResourceGenerations.Add(gRow);
                    context.SaveChanges();

                    //for Resoruce copies

                    for (int i = 0; i < rgModel.GenerationCopyCount; i++)
                    {
                        var rRow = new ResourceCopy();
                        rRow.GenerationId = gRow.GenerationId;
                        rRow.ResourceId = rgModel.ResourceId;
                        rRow.ResourceCopyCount = i + 1;
                        rRow.ResourceCopyNumber = rgModel.ResourceName + "-" + rgModel.ResourceId.ToString() + ":" + gRow.GenerationId.ToString() + ":" + i.ToString();
                        rRow.Remarks = rgModel.Remarks;
                        rRow.IsAvailable = true;
                        rRow.PublishedDate = CommonService.GetEnglishDate(rgModel.GenerationDateNepali);
                        rRow.Edition = 2016;
                        context.ResourceCopies.Add(rRow);
                    }
                    context.SaveChanges();
                    rModel.Msg = "Success";
                    rModel.Success = true;
                    return rModel;
                }

            }
            catch (Exception ex)
            {
                rModel.Msg = "fail";
                rModel.Success = false;
                return rModel;
            }

        }

    }
}
