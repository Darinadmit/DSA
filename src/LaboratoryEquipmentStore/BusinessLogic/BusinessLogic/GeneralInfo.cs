using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessLogic
{
    public class GeneralInfo
    {
        public static Repository.IRepository DataProvider { get; private set; }
        public const int Reserve = 2;

        public GeneralInfo(Repository.IRepository dataProvider)
        {
            DataProvider = dataProvider;
        }

        public enum OrderStatus
        {
            InProcessing = 0,
            WaitingForConfirmation = 1,
            WaitingForPayment = 2,
            WaitingForDelivery = 3,
            Completed = 4,
            Removed = 5
        };
    }
}
