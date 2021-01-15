using POSModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSBLL.Interface
{
    public interface IGenerationInterface
    {
        List<ResourceGenerationModel> GetResourceGenerationList();

        ReturnMessageModel CreateResourceGeneration(ResourceGenerationModel rgModel);

        
    }
}
