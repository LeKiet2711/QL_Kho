using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_Kho.Models;

[Table("TheoDoi")]
public partial class TheoDoi
{
    [Key]
    [Column("Auto_ID")]
    public int AutoId { get; set; }

    [StringLength(100)]
    public string? TenDangNhap { get; set; }

    [StringLength(100)]
    public int? TrangThai { get; set; }

    [StringLength(100)]
    public DateTime? NgayThucHien { get; set; }

    [StringLength(100)]
    public string? lenh { get; set; }

    public string? ThongTin { get; set; }

    [Column("isDeleted")]
    public bool? IsDeleted { get; set; }

    [ForeignKey("TenDangNhap")]
    [InverseProperty("TheoDois")]
    public virtual QlNguoiDung? TenDangNhapNavigation { get; set; }
}
