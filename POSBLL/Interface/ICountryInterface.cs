using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSModel;

namespace POSBLL.Interface
{
    public interface ICountryInterface
    {
        List<CountryModel> GetCountryList();

        ReturnMessageModel CreateEditCountry(CountryModel cModel);
    }

}