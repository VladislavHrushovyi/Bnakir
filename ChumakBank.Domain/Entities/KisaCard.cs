﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using ChumakBank.Domain.Common;

namespace ChumakBank.Domain.Entities;

public class KisaCard : BaseEntity
{
    public Guid CardIdFromSystem { get; set; }
    
    [DefaultValue(0)]
    public float Balance { get; set; }

    public Guid UserId { get; set; }
}