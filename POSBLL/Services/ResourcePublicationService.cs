using POSBLL.Interface;
using POSDAL;
using POSModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMatrix.WebData;

namespace POSBLL.Services
{
    public class ResourcePublicationService:IResourcePublicationInterface
    {
        ReturnMessageModel rModel;
        public ResourcePublicationService()
        {
            rModel = new ReturnMessageModel();
        }

        public List<ResourcePublicationModel> GetResourcePublicationList()
        {
            using (PointOfSaleEntities context = new PointOfSaleEntities())
            {
                var result = context.ResourcePublications.Select(x => new ResourcePublicationModel
                {
                    PublicationId = x.PublicationId,
                    Publisher = x.Publisher,
                    PublisherOrigin = x.PublisherOrigin,
                    Genere = x.Genere,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    ModifiedDate = x.ModifiedDate,
                    ModifiedBy = x.ModifiedBy,
                    CountryName=x.Country.CountryName
                }).ToList();
                return result;
            }

        }
        public ReturnMessageModel CreateEditResourcePublication(ResourcePublicationModel rpModel)
        {
            try
            {
                using (PointOfSaleEntities _context = new PointOfSaleEntities())
                {
                    var rtRow = _context.ResourcePublications.Where(x => x.PublicationId == rpModel.PublicationId).FirstOrDefault();
                    if (rtRow == null)
                    {
                        rtRow = new ResourcePublication();
                    }

                    rtRow.Publisher = rpModel.Publisher;
                    rtRow.PublisherOrigin = rpModel.PublisherOrigin;
                    rtRow.Genere = rpModel.Genere;
                    rtRow.IsActive = rpModel.IsActive;


                    if (rpModel.PublicationId == 0)
                    {
                        rtRow.CreatedBy = WebSecurity.CurrentUserId;
                        rtRow.CreatedDate = System.DateTime.Now;
                        _context.ResourcePublications.Add(rtRow);
                        _context.SaveChanges();

                    }
                    else
                    {
                        rtRow.ModifiedBy = WebSecurity.CurrentUserId;
                        rtRow.ModifiedDate = System.DateTime.Now;
                        _context.ResourcePublications.Attach(rtRow);
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


    }
}
