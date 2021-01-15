using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSModel;

namespace POSBLL.Interface
{
    public interface IResourceInterface
    {
        List<ResourceTypeModel> GetResourceTypeList();

        ReturnMessageModel CreateEditResourceType(ResourceTypeModel rtModel);

        ReturnMessageModel CreateResourceCopies(ResourceGenerationModel rgModel);
    }
   
}
