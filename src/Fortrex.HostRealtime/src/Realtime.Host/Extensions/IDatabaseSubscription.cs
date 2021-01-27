using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtimeRealtimeDatabaseSubscriptionSubscription.Web
{
    public interface IDatabaseSubscription
    {
        void Configure(string connectionString);
    }
}
