using System.ComponentModel.DataAnnotations;  // for Key
using System.ComponentModel.DataAnnotations.Schema;  // for Column

namespace auth.Models;

[Table("NOTICE")]
public class Notice
{
    // 请与数据库中表字段保持一致
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 告知 Entity Framework Core NOTICEID 是由数据库生成的  --- 自增
    [Column("NOTICEID")]
    public int? NoticeId { get; set; }
    [Column("NOTICETIME")]
    public DateTime? NoticeTime { get; set; }
    [Column("NOTICECONTENT")]
    public string? NoticeContent { get; set; }
    [Column("ADMINID")]
    public int? AdminId { get; set; }
    [Column("USERID")]
    public int? UserId { get; set; }
    
}