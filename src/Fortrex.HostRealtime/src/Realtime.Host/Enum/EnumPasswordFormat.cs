using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lib.Domain.User
{
    public enum EnumPasswordFormat
    {
        Encrypted = 1,
        Hashed = 2,
        EncryptAbc283 = 3,
        EncryptCodeAES256 = 4
    }
}