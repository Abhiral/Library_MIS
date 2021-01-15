using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSBLL
{
   public class EnumCollection
    {

        public enum TransactionType
        {
            Domestic,
           International,
           //Express
        }

       
       
        public enum UserRoles
        {
            CounterUser,
            Administrator,
            DeliveryAgent
            
        }

        public enum RegistryTypes
        {
            Domestic,
            SAARC,
            Others

        }
       
    }
}
