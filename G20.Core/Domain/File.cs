﻿using G20.Core.Domain;
using System;
using System.Collections.Generic;

namespace G20.Core.Domain;

public partial class File : BaseEntity
{
    public string? Name { get; set; }
    public string? OriginalName { get; set; }

    public int TypeId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual AspNetUser? CreatedByNavigation { get; set; }


    public virtual AspNetUser? UpdatedByNavigation { get; set; }
}
