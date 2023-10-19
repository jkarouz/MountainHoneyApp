using System;
using System.Collections.Generic;

namespace MountainHoneyApp.Models;

public partial class Document
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string ProfilePicture { get; set; } = null!;
}
