using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWeb.Services
{
    public interface IPurchase
    {
        void ProcessPurchase(int user, int id); //Purchase item//

    }
}
