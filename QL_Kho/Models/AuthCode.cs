using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_Kho.Models;

[Table("Auth_Code")]
public partial class AuthCode
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("Key_value")]
    [StringLength(100)]
    public string? KeyValue { get; set; }
}
