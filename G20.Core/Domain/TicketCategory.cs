﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Core.Domain
{
    public partial class TicketCategory : BaseEntityWithTacking
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? FileId { get; set; }
        public virtual File? File { get; set; }
    }
}
