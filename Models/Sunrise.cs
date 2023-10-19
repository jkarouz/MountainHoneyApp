using System;
using System.Collections.Generic;

namespace MountainHoneyApp.Models;

public partial class Sunrise
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? FullName { get; set; }

    public string? IdNumber { get; set; }

    public string? ContactNumber { get; set; }

    public int? RentAmount { get; set; }

    public string? Comments { get; set; }
    public DateTime? DateOnlyTime { get; set; }

    public string? DateOnly { get; set; }
    public Sunrise()
    {
        FullName = Name + " " + Surname;
  
    }
 
}
