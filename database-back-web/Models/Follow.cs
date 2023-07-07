using System.ComponentModel.DataAnnotations;  // for Key
using System.ComponentModel.DataAnnotations.Schema;  // for Column
using Microsoft.EntityFrameworkCore;

namespace auth.Models;

[Table("FOLLOW")]
[PrimaryKey(nameof(UserId),nameof(FollowerUserId))]
public class Follow
{
    // 请与数据库中表字段保持一致
    [Column("USERID")]
    public int? UserId { get; set; }
    [Column("FOLLOWERUSERID")]
    public int? FollowerUserId { get; set; }
}