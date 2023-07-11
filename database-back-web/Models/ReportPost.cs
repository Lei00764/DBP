using System.ComponentModel.DataAnnotations;  // for Key
using System.ComponentModel.DataAnnotations.Schema;  // for Column
using Microsoft.EntityFrameworkCore;

namespace auth.Models;

[Table("REPORTPOST")]
[PrimaryKey(nameof(PostReportId), nameof(UserId), nameof(PostId), nameof(AdminId))]
public class ReportPost
{
    // 请与数据库中表字段保持一致
    [Column("POSTREPORTID")]
    public int? PostReportId { get; set; }
    [Column("USERID")]
    public int? UserId { get; set; }
    [Column("POSTID")]
    public int? PostId { get; set; }
    [Column("ADMINID")]
    public int? AdminId { get; set; }
    [Column("REASON")]
    public string? Reason { get; set; }
    [Column("TIME")]
    public string? Time { get; set; }
    [Column("RESULT")]
    public string? Result { get; set; }
}