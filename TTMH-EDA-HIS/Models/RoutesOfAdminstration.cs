using System;
using System.Collections.Generic;

namespace TTMH_EDA_HIS.Models;

public partial class RoutesOfAdminstration
{
    public string Roaid { get; set; } = null!;

    public string BodyParts { get; set; } = null!;

    public virtual ICollection<Drug> Drugs { get; set; } = new List<Drug>();
}
