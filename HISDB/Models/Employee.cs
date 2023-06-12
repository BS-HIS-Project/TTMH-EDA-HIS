using System;
using System.Collections.Generic;

namespace HISDB.Models;

public partial class Employee
{
    public string EmployeeId { get; set; } = null!;

    public string EmployeeName { get; set; } = null!;

    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Cashier? Cashier { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Pharmacist? Pharmacist { get; set; }
}
