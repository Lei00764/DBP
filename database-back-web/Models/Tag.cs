using System.ComponentModel.DataAnnotations;  // for Key
using System.ComponentModel.DataAnnotations.Schema;  // for Column

namespace auth.Models;

[Table("TAG")]
public class Tag
{
    // 请与数据库中表字段保持一致
    [Key]
    [Column("TAGID")]
    public int? TagId { get; set; }
    [Column("TAGNAME")]
    public int? TagName { get; set; }
    
}