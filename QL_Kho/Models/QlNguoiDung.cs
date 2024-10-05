using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_Kho.Models;

[Table("QL_NguoiDung")]
public partial class QlNguoiDung
{
    [Key]
    [StringLength(100)]
    public string TenDangNhap { get; set; } = null!;

    [StringLength(100)]
    public string? MatKhau { get; set; }

    public int? TrangThai { get; set; }
}
