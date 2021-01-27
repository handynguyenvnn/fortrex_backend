using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lib.Domain.User
{
    public enum EnumRole
    {
        SUPPERADMIN = 1,
        USER = 2,
        ADMIN = 3,
        OWNER = 4
    }

    public enum CourtStatus
    {
        Pending = 1,
        Approve = 2,
        Cencel = 3
    }
}