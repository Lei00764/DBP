using System.ComponentModel.DataAnnotations;  // for Key
using System.ComponentModel.DataAnnotations.Schema;  // for Column
using Microsoft.EntityFrameworkCore;

namespace auth.Models;

[Table("FAVOURITE")]
[PrimaryKey(nameof(UserId), nameof(PostId))]
public class Favourite
{
    // 请与数据库中表字段保持一致
    [Column("USERID")]
    public int? UserId { get; set; }
    [Column("POSTID")]
    public int? PostId { get; set; }
}