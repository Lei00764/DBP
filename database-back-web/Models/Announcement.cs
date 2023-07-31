using System.ComponentModel.DataAnnotations;  // for Key
using System.ComponentModel.DataAnnotations.Schema;  // for Column

namespace auth.Models;

[Table("ANNOUNCEMENT")]
public class Announcement
{
    // 请与数据库中表字段保持一致
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 告知 Entity Framework Core ANNOUNCEMENTID 是由数据库生成的  --- 自增
    [Column("ANNOUNCEMENTID")]
    public int? AnnouncementId { get; set; }
    [Column("ANNOUNCEMENTTIME")]
    public DateTime? AnnouncementTime { get; set; }
    [Column("ANNOUNCEMENTCONTENT")]
    public string? AnnouncementContent { get; set; }
    [Column("ADMINID")]
    public int? AdminId { get; set; }
    [Column("ISTOP")]
    public int? IsTop { get; set; }
    
    public Announcement()
    {//设置默认值（初始值）
        IsTop = 0;
    }
}