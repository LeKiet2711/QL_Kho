using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_Kho.Models;

[Table("tbl_XNK_Xuat_Kho_Raw_Data")]
public partial class XNK_XuatKhoRawData
{
    [Key]
    [Column("Auto_ID")]
    public int AutoId { get; set; }

    [Column("Xuat_Kho_ID")]
    public int XuatKhoId { get; set; }

    [Column("San_Pham_ID")]
    public int SanPhamId { get; set; }

    [Column("SL_Xuat", TypeName = "decimal(18, 2)")]
    public decimal SlXuat { get; set; }

    [Column("Don_Gia_Xuat", TypeName = "decimal(18, 2)")]
    public decimal DonGiaXuat { get; set; }

    [Column("isDeleted")]
    public bool IsDeleted { get; set; }

    [ForeignKey("SanPhamId")]
    [InverseProperty("TblXnkXuatKhoRawData")]
    public virtual DanhMucSanPham SanPham { get; set; }

    [ForeignKey("XuatKhoId")]
    [InverseProperty("TblXnkXuatKhoRawData")]
    public virtual XNK_XuatKho XuatKho { get; set; }
}
