using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class MailTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Template { get; set; }
    }
}
