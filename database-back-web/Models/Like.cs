using System.ComponentModel.DataAnnotations;  // for Key
using System.ComponentModel.DataAnnotations.Schema;  // for Column
using Microsoft.EntityFrameworkCore;

namespace auth.Models;

[Table("T_LIKE")]
[PrimaryKey(nameof(UserId), nameof(PostId))]
public class Like
{
    // 请与数据库中表字段保持一致
    [Column("USERID")]
    public int? UserId { get; set; }
    [Column("POSTID")]
    public int? PostId { get; set; }
}