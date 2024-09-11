using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_Kho.Models;

[Table("tbl_DM_NCC")]
public partial class DanhMucNCC
{
    [Key]
    [Column("Auto_ID")]
    public int AutoId { get; set; }

    [Column("Ma_NCC")]
    [StringLength(100)]
    public string? MaNcc { get; set; }

    [Column("Ten_NCC")]
    [StringLength(100)]
    public string? TenNcc { get; set; }

    [StringLength(255)]
    public string? GhiChu { get; set; }

    [Column("isDeleted")]
    public bool? IsDeleted { get; set; }

    [InverseProperty("Ncc")]
    public virtual ICollection<XNK_NhapKho> TblXnkNhapKhos { get; set; } = new List<XNK_NhapKho>();
}
