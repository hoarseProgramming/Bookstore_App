﻿using System;
using System.Collections.Generic;

namespace Bookstore_App.Domain;

public partial class TitlesPerAuthor
{
    public string Name { get; set; } = null!;

    public int? Age { get; set; }

    public int? Titles { get; set; }

    public decimal? InventoryValue { get; set; }
}
