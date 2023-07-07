using System.ComponentModel.DataAnnotations;  // for Key
using System.ComponentModel.DataAnnotations.Schema;  // for Column

namespace auth.Models;

[Table("T_COMMENT")]
public class Comment
{
    [Key]
    [Column("MSGID")]
    public int? MsgId { get; set; }

    [Column("USERID")]
    public int? UserId { get; set; }

    [Column("POSTID")]
    public int? PostId { get; set; }

    [Column("RELEASETIME")]
    public DateTime? ReleaseTime { get; set; }

    [Column("CONTENT")]
    public string? Content { get; set; }

    [Column("ISBANNED")]
    public int? IsBanned { get; set; }
}