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

    public class ResourceAuthorService : IResourceAuthorInterface
    {
        ReturnMessageModel rModel;
        public ResourceAuthorService()
        {
            rModel = new ReturnMessageModel();
        }

        #region Resource Author Type

        public List<ResourceAuthorModel> GetResourceAuthorList()
        {
            using (PointOfSaleEntities context = new PointOfSaleEntities())
            {
                var result = context.ResourceAuthors.Select(x => new ResourceAuthorModel
                {
                    AuthorId = x.AuthorId,
                    Author = x.Author,
                    Nationality = x.Nationality,
                    Genere=x.Genere,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    ModifiedDate = x.ModifiedDate,
                    ModifiedBy = x.ModifiedBy,
                    CountryName = x.Country.CountryName
                }).ToList();
                return result;
            }

        }
        public ReturnMessageModel CreateEditResourceAuthor(ResourceAuthorModel raModel)
        {
            try
            {
                using (PointOfSaleEntities _context = new PointOfSaleEntities())
                {
                    var rtRow = _context.ResourceAuthors.Where(x => x.AuthorId == raModel.AuthorId).FirstOrDefault();
                    if (rtRow == null)
                    {
                        rtRow = new ResourceAuthor();
                    }

                    rtRow.Author = raModel.Author;
                    rtRow.Nationality = raModel.Nationality;
                    rtRow.Genere = raModel.Genere;
                    rtRow.IsActive = raModel.IsActive;

                    if (raModel.AuthorId == 0)
                    {
                        rtRow.CreatedBy = WebSecurity.CurrentUserId;
                        rtRow.CreatedDate = System.DateTime.Now;
                        _context.ResourceAuthors.Add(rtRow);
                        _context.SaveChanges();

                    }
                    else
                    {
                        rtRow.ModifiedBy = WebSecurity.CurrentUserId;
                        rtRow.ModifiedDate = System.DateTime.Now;
                        _context.ResourceAuthors.Attach(rtRow);
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
    }
}
