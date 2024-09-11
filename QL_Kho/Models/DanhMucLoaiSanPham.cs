using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_Kho.Models;

[Table("tbl_DM_Loai_San_Pham")]
public partial class DanhMucLoaiSanPham
{
    [Key]
    [Column("Auto_ID")]
    public int AutoId { get; set; }

    [Column("Ma_LSP")]
    [StringLength(100)]
    [Required(ErrorMessage ="Vui lòng không để trống mã loại sản phẩm")]
    public string MaLsp { get; set; }

    [Column("Ten_LSP")]
    [StringLength(100)]
    [Required(ErrorMessage = "Vui lòng không để trống tên loại sản phẩm")]
    public string TenLsp { get; set; }

    [StringLength(255)]
    public string? GhiChu { get; set; }

    [Column("isDeleted")]
    public bool IsDeleted { get; set; }

    [InverseProperty("LoaiSanPham")]
    public virtual ICollection<DanhMucSanPham> TblDmSanPhams { get; set; } = new List<DanhMucSanPham>();
}
