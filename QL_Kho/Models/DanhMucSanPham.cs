using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_Kho.Models;

[Table("tbl_DM_San_Pham")]
public partial class DanhMucSanPham
{
    [Key]
    [Column("Auto_ID")]
    public int AutoId { get; set; }

    [Column("Ma_San_Pham")]
    [StringLength(100)]
    public string? MaSanPham { get; set; }

    [Column("Ten_San_Pham")]
    [StringLength(100)]
    public string? TenSanPham { get; set; }

    public int? LoaiSanPhamId { get; set; }

    public int? DonViTinhId { get; set; }

    [StringLength(255)]
    public string? GhiChu { get; set; }

    [Column("isDeleted")]
    public bool? IsDeleted { get; set; }

    [ForeignKey("DonViTinhId")]
    [InverseProperty("TblDmSanPhams")]
    public virtual DanhMucDonViTinh? DonViTinh { get; set; }

    [ForeignKey("LoaiSanPhamId")]
    [InverseProperty("TblDmSanPhams")]
    public virtual DanhMucLoaiSanPham? LoaiSanPham { get; set; }

    [InverseProperty("SanPham")]
    public virtual ICollection<XNK_NhapKhoRawData> TblXnkNhapKhoRawData { get; set; } = new List<XNK_NhapKhoRawData>();

    [InverseProperty("SanPham")]
    public virtual ICollection<XNK_XuatKhoRawData> TblXnkXuatKhoRawData { get; set; } = new List<XNK_XuatKhoRawData>();
}
