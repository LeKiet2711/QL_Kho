using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_Kho.Models;

[Table("QL_NhomNguoiDung")]
public partial class QlNhomNguoiDung
{
    [Key]
    [StringLength(100)]
    public string MaNhom { get; set; } = null!;

    [StringLength(100)]
    public string? TenNhom { get; set; }

    [StringLength(100)]
    public string? GhiChu { get; set; }
}
