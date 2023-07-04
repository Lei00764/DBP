using System.ComponentModel.DataAnnotations;  // for Key
using System.ComponentModel.DataAnnotations.Schema;  // for Column

namespace auth.Models;

[Table("ADMINISTRATOR")]
public class Administrator
{
    // 请与数据库中表字段保持一致
    [Key]
    [Column("ADMINID")]
    public int AdminId { get; set; }
    [Column("ADMINNAME")]
    public string? AdminName { get; set; }
    [Column("TEL")]
    public string? Tel {get;set;}
    [Column("EMAIL")]
    public string? Email {get;set;}
    [Column("PASSWORD")]
    public string? PassWord { get; set; }
    [Column("AVATAR")]
    public string? Avatar {get;set;}
    
}