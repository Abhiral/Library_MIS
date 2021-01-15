using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSModel;

namespace POSBLL.Interface
{
    public interface IResourcePublicationInterface
    {
        List<ResourcePublicationModel> GetResourcePublicationList();

        ReturnMessageModel CreateEditResourcePublication(ResourcePublicationModel rpModel);
    }
}
