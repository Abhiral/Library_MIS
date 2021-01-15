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
    public class CountryService:ICountryInterface
    {
        ReturnMessageModel rModel;

        public CountryService()
        {
            rModel = new ReturnMessageModel();
        }
        public List<CountryModel> GetCountryList()
        {
            using (PointOfSaleEntities context = new PointOfSaleEntities())
            {
                var result = context.Countries.Select(x => new CountryModel
                {
                    CountryId = x.CountryId,
                    CountryName = x.CountryName,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    ModifiedDate = x.ModifiedDate,
                    ModifiedBy = x.ModifiedBy

                }).ToList();
                return result;
            }

        }
        public ReturnMessageModel CreateEditCountry(CountryModel cModel)
        {
            try
            {
                using (PointOfSaleEntities _context = new PointOfSaleEntities())
                {
                    var rtRow = _context.Countries.Where(x => x.CountryId == cModel.CountryId).FirstOrDefault();
                    if (rtRow == null)
                    {
                        rtRow = new Country();
                    }

                    rtRow.CountryName = cModel.CountryName;
                    rtRow.IsActive = cModel.IsActive;


                    if (cModel.CountryId == 0)
                    {
                        rtRow.CreatedBy = WebSecurity.CurrentUserId;
                        rtRow.CreatedDate = System.DateTime.Now;
                        _context.Countries.Add(rtRow);
                        _context.SaveChanges();

                    }
                    else
                    {
                        rtRow.ModifiedBy = WebSecurity.CurrentUserId;
                        rtRow.ModifiedDate = System.DateTime.Now;
                        _context.Countries.Attach(rtRow);
                        _context.Entry(rtRow).State = EntityState.Modified;
                        _context.SaveChanges();
                    }

                    rModel.Msg = "Country Saved Successfully";
                    rModel.Success = true;
                    return rModel;
                }


            }
            catch (Exception ex)
            {
                rModel.Msg = "Country Saved Failed";
                rModel.Success = false;
                return rModel;

            }
        }
    }
}
