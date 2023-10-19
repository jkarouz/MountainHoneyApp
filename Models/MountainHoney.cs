using System;
using System.Collections.Generic;

namespace MountainHoneyApp.Models;

public partial class MountainHoney
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? FullName { get; set; }

    public int? IdNumber { get; set; }

    public string? ContactNumber { get; set; }

    public int? RentAmount { get; set; }

    public string? Date { get; set; }
}
