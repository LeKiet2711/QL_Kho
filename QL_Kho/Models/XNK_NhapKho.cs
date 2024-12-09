using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_Kho.Models;

[Table("tbl_XNK_Nhap_Kho")]
public partial class XNK_NhapKho
{
    [Key]
    [Column("Auto_ID")]
    public int AutoId { get; set; }

    [Column("So_Phieu_Nhap")]
    [StringLength(100)]
    [Required(ErrorMessage = "Vui lòng không để trống số phiếu nhập")]
    public string SoPhieuNhap { get; set; }

    [Column("Kho_ID")]
    [Required(ErrorMessage = "Vui lòng chọn kho")]
    public int? KhoId { get; set; }

    [Column("NCC_ID")]
    [Required(ErrorMessage = "Vui lòng chọn nhà cung cấp")]
    public int? NccId { get; set; }

    [Column("Ngay_Nhap_Kho")]
    [Required(ErrorMessage = "Vui lòng không để trống ngày nhập")]
    public DateTime? NgayNhapKho { get; set; }

    [StringLength(255)]
    public string? GhiChu { get; set; }

    [Column("isDeleted")]
    public bool IsDeleted { get; set; }

    [ForeignKey("KhoId")]
    [InverseProperty("TblXnkNhapKhos")]
    public virtual DanhMucKho Kho { get; set; }

    [ForeignKey("NccId")]
    [InverseProperty("TblXnkNhapKhos")]
    public virtual DanhMucNCC Ncc { get; set; }

    [InverseProperty("NhapKho")]
    public virtual ICollection<XNK_NhapKhoRawData> TblXnkNhapKhoRawData { get; set; } = new List<XNK_NhapKhoRawData>();
}
