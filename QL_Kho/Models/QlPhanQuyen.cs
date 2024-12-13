using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_Kho.Models;

[Keyless]
[Table("QL_PhanQuyen")]
public partial class QlPhanQuyen
{
    [StringLength(100)]
    public string? MaNhomNguoiDung { get; set; }

    [StringLength(100)]
    public string? MaManHinh { get; set; }

    public int? CoQuyen { get; set; }

    [Column("isDeleted")]
    public bool? IsDeleted { get; set; }
}
