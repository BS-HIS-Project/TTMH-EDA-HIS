using System;
using System.Collections.Generic;

namespace HISDB;

public partial class Drug
{
    public string DrugId { get; set; } = null!;

    public string Atccode { get; set; } = null!;

    public string? Nhicode { get; set; }

    public string DrugName { get; set; } = null!;

    public string GenericName { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public string Dcontent { get; set; } = null!;

    public string? Appearance { get; set; }

    public string? ClinicalUses { get; set; }

    public string? SuggestedUsage { get; set; }

    public string AdverseReactions { get; set; } = null!;

    public string? WarningPrecautions { get; set; }

    public string? PointOfHealthEducation { get; set; }

    public string StorageConditions { get; set; } = null!;

    public string? OtherInstructions { get; set; }

    public string? ProcessingMethod { get; set; }

    public string Roaid { get; set; } = null!;

    public virtual ICollection<ChartsDrugsDosage> ChartsDrugsDosages { get; set; } = new List<ChartsDrugsDosage>();

    public virtual RoutesOfAdminstration Roa { get; set; } = null!;
}
