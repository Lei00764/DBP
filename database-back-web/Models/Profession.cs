using System.ComponentModel.DataAnnotations;  // for Key
using System.ComponentModel.DataAnnotations.Schema;  // for Column
using Microsoft.EntityFrameworkCore;

namespace auth.Models;

[Table("PROFESSION")]
public class Profession 
{
    // 请与数据库中表字段保持一致
    [Column("USERID")]
    public int? UserId { get; set; }
    [Column("APPLYTIME")]
    public DateTime? ApplyTime { get; set; }
    [Column("ILLUSTRATE")]
    public string? Illustrate { get; set; }
    [Column("EVIDENCE")]
    public string? Evidence { get; set; }
    [Column("ISACCEPTED")]
    public int? IsAccepted { get; set; }
    [Key]
    [Column("REQUESTID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? RequestId { get; set; }
}