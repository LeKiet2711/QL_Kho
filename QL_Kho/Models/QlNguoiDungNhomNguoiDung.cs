using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_Kho.Models;

[PrimaryKey("TenDangNhap", "MaNhomNguoiDung")]
[Table("QL_NguoiDungNhomNguoiDung")]
public partial class QlNguoiDungNhomNguoiDung
{
    [Key]
    [StringLength(100)]
    public string TenDangNhap { get; set; } = null!;

    [Key]
    [StringLength(100)]
    public string MaNhomNguoiDung { get; set; } = null!;

    [StringLength(100)]
    public string? GhiChu { get; set; }

    [Column("isDeleted")]
    public bool? IsDeleted { get; set; }
}
