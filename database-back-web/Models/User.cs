using System.ComponentModel.DataAnnotations;  // for Key
using System.ComponentModel.DataAnnotations.Schema;  // for Column

namespace auth.Models;

[Table("t_user")]
public class User
{
    // 请与数据库中表字段保持一致
    [Key]
    [Column("UserId")]
    public int UserId { get; set; }
    [Column("UserName")]
    public string? UserName { get; set; }
    [Column("PassWord")]
    public string? PassWord { get; set; }
}