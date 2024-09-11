using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_Kho.Models;

[Table("tbl_XNK_Nhap_Kho_Raw_Data")]
public partial class XNK_NhapKhoRawData
{
    [Key]
    [Column("Auto_ID")]
    public int AutoId { get; set; }

    [Column("Nhap_Kho_ID")]
    public int? NhapKhoId { get; set; }

    [Column("San_Pham_ID")]
    public int? SanPhamId { get; set; }

    [Column("SL_Nhap", TypeName = "decimal(18, 2)")]
    public decimal? SlNhap { get; set; }

    [Column("Don_Gia_Nhap", TypeName = "decimal(18, 2)")]
    public decimal? DonGiaNhap { get; set; }

    [Column("isDeleted")]
    public bool? IsDeleted { get; set; }

    [ForeignKey("NhapKhoId")]
    [InverseProperty("TblXnkNhapKhoRawData")]
    public virtual XNK_NhapKho? NhapKho { get; set; }

    [ForeignKey("SanPhamId")]
    [InverseProperty("TblXnkNhapKhoRawData")]
    public virtual DanhMucSanPham? SanPham { get; set; }
}
