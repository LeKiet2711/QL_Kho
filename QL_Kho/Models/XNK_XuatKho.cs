using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_Kho.Models;

[Table("tbl_XNK_Xuat_Kho")]
public partial class XNK_XuatKho
{
    [Key]
    [Column("Auto_ID")]
    public int AutoId { get; set; }

    [Column("So_Phieu_Xuat")]
    [StringLength(100)]
    public string? SoPhieuXuat { get; set; }

    [Column("Kho_ID")]
    public int? KhoId { get; set; }

    [Column("Ngay_Xuat_Kho")]
    public DateOnly? NgayXuatKho { get; set; }

    [StringLength(255)]
    public string? GhiChu { get; set; }

    [Column("isDeleted")]
    public bool? IsDeleted { get; set; }

    [ForeignKey("KhoId")]
    [InverseProperty("TblXnkXuatKhos")]
    public virtual DanhMucKho? Kho { get; set; }

    [InverseProperty("XuatKho")]
    public virtual ICollection<XNK_XuatKhoRawData> TblXnkXuatKhoRawData { get; set; } = new List<XNK_XuatKhoRawData>();
}
