using System.ComponentModel.DataAnnotations;  // for Key
using System.ComponentModel.DataAnnotations.Schema;  // for Column
using Microsoft.EntityFrameworkCore;

namespace auth.Models;

[Table("REPORTCOMMENTS")]
[PrimaryKey(nameof(MsgReportId), nameof(UserId), nameof(MsgId))]
public class ReportComments
{
    // 请与数据库中表字段保持一致
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 告知 Entity Framework Core UserId 是由数据库生成的  --- 自增
    [Column("MSGREPORTID")]
    public int? MsgReportId { get; set; }
    [Column("USERID")]
    public int? UserId { get; set; }
    [Column("MSGID")]
    public int? MsgId { get; set; }
    [Column("ADMINID")]
    public int? AdminId { get; set; }
    [Column("REASON")]
    public string? Reason { get; set; }
    [Column("TIME")]
    public DateTime? Time { get; set; }
    [Column("RESULT")]
    public string? Result { get; set; }
    [Column("ISTRUE")]
    public int? IsTrue { get; set; }
}