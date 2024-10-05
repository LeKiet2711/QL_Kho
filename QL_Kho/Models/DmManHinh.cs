using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_Kho.Models;

[Table("DM_ManHinh")]
public partial class DmManHinh
{
    [Key]
    [StringLength(100)]
    public string MaManHinh { get; set; } = null!;

    [StringLength(100)]
    public string? TenManHinh { get; set; }
}
