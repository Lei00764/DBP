using System.ComponentModel.DataAnnotations;  // for Key
using System.ComponentModel.DataAnnotations.Schema;  // for Column

namespace auth.Models;

[Table("SEARCHHISTORY")]
public class SearchHistory
{
    // 请与数据库中表字段保持一致
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 告知 Entity Framework Core UserId 是由数据库生成的  --- 自增
    [Column("SEARCHID")]
    public int? SearchId { get; set; }
    [Column("USERID")]
    public int? UserId { get; set; }
    [Column("CONTENT")]
    public string? Content { get; set; }
}