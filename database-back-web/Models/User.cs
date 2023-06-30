using System.ComponentModel.DataAnnotations;  // for Key
using System.ComponentModel.DataAnnotations.Schema;  // for Column

namespace auth.Models;

[Table("T_USER")]
public class User
{
    // 请与数据库中表字段保持一致
    [Key]
    [Column("USERID")]
    public int UserId { get; set; }
    [Column("USERNAME")]
    public string? UserName { get; set; }
    [Column("PASSWORD")]
    public string? PassWord { get; set; }
}