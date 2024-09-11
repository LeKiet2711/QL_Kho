using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_Kho.Models;

[Table("tbl_DM_Kho")]
public partial class DanhMucKho
{
    [Key]
    [Column("Auto_ID")]
    public int AutoId { get; set; }

    [Column("Ma_Kho")]
    [StringLength(100)]
    public string? MaKho { get; set; }

    [Column("Ten_Kho")]
    [StringLength(100)]
    public string? TenKho { get; set; }

    [StringLength(255)]
    public string? GhiChu { get; set; }

    [Column("isDeleted")]
    public bool? IsDeleted { get; set; }

    [InverseProperty("Kho")]
    public virtual ICollection<XNK_NhapKho> TblXnkNhapKhos { get; set; } = new List<XNK_NhapKho>();

    [InverseProperty("Kho")]
    public virtual ICollection<XNK_XuatKho> TblXnkXuatKhos { get; set; } = new List<XNK_XuatKho>();
}
