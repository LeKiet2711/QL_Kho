using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_Kho.Models;

[Table("tbl_DM_Don_Vi_Tinh")]
public partial class DanhMucDonViTinh
{
    [Key]
    [Column("Auto_ID")]
    public int AutoId { get; set; }

    [Column("Ma_DVT")]
    [StringLength(100)]
    [Required(ErrorMessage ="Vui lòng không để trống mã đơn vị tính")]
    public string MaDvt { get; set; }

    [Column("Ten_DVT")]
    [StringLength(100)]
    [Required(ErrorMessage = "Vui lòng không để trống tên đơn vị tính")]
    public string TenDvt { get; set; }

    [StringLength(255)]
    public string? GhiChu { get; set; }

    [Column("isDeleted")]
    public bool IsDeleted { get; set; }

    [InverseProperty("DonViTinh")]
    public virtual ICollection<DanhMucSanPham> TblDmSanPhams { get; set; } = new List<DanhMucSanPham>();
}
