using POSBLL.Interface;
using POSDAL;
using POSModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSBLL.Services
{
    public class GenerationService : IGenerationInterface
    {
        ReturnMessageModel rModel;
        

       

        public GenerationService()
        {
            rModel = new ReturnMessageModel();
        }
        public List<ResourceGenerationModel> GetResourceGenerationList()
        {
            using (PointOfSaleEntities context = new PointOfSaleEntities())
            {

                var result = context.ResourceGenerations.Select(x => new ResourceGenerationModel
                {
                    GenerationId = x.GenerationId,
                    ResourceId = x.ResourceId,
                    GenerationDate = x.GenerationDate,
                    GenerationCopyCount = x.GenerationCopyCount,
                    Remarks = x.Remarks,
                    IsActive = x.IsActive,
                    ResourceName = x.ResourceSetup.ResourceName

                }).ToList();
                return result;
            }

        }
        public ReturnMessageModel CreateResourceGeneration(ResourceGenerationModel rgModel)
        {
            try
            {
                using (PointOfSaleEntities _context = new PointOfSaleEntities())
                {
                    var rtRow = _context.ResourceGenerations.Where(x => x.GenerationId == rgModel.GenerationId).FirstOrDefault();

                    if (rtRow == null)
                    {
                        rtRow = new ResourceGeneration();
                    }
                    rtRow.ResourceId = rgModel.ResourceId;
                    //rtRow.GenerationDate = rgModel.GenerationDate;
                    rtRow.GenerationCopyCount = rgModel.GenerationCopyCount;
                    rtRow.Remarks = rgModel.Remarks;


                    if (rgModel.GenerationId == 0)
                    {

                        rtRow.GenerationDate = System.DateTime.Now;
                        _context.ResourceGenerations.Add(rtRow);
                        _context.SaveChanges();

                    }
                    else
                    {

                        _context.ResourceGenerations.Attach(rtRow);
                        _context.Entry(rtRow).State = EntityState.Modified;
                        _context.SaveChanges();
                    }


                    var gRow = _context.ResourceCopies.Where(x => x.ResourceCopyId == rgModel.ResourceCopyId).FirstOrDefault();
                    if (gRow == null)
                    {
                        gRow = new ResourceCopy();
                    }

                    for (int i = 1; i < rgModel.GenerationCopyCount; i++)
                    {
                        gRow.ResourceCopyId = rgModel.ResourceCopyId;
                        gRow.ResourceId = rgModel.ResourceId;
                        gRow.GenerationId = rgModel.GenerationId;
                        gRow.ResourceCopyCount = i;
                        //gRow.ResourceCopyNumber = ResourceId.ToString() + rgModel.GenerationId + rgModel.ResourceCopyId;

                    }
                    //rtRow.GenerationId


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
    }
}
