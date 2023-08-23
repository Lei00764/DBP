using System.ComponentModel.DataAnnotations;  // for Key
using System.ComponentModel.DataAnnotations.Schema;  // for Column

namespace auth.Models;

[Table("T_USER")]
public class User
{
    // 请与数据库中表字段保持一致
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 告知 Entity Framework Core UserId 是由数据库生成的  --- 自增
    [Column("USERID")]
    public int? UserId { get; set; }
    [Column("USERNAME")]
    public string? UserName { get; set; }
    [Column("PASSWORD")]
    public string? PassWord { get; set; }
    [Column("AVATAR")]
    public string? Avatar { get; set; }
    [Column("PROFESSIONAL")]
    public string? Professional { get; set; }
    [Column("SIGNATURE")]
    public string? Signature { get; set; }
    [Column("EMAIL")]
    public string? Email { get; set; }
    [Column("TEL")]
    public string? Tel { get; set; }
    [Column("FOLLOWERNUM")]
    public int? FollowerNum { get; set; }
    [Column("THEMEID")]
    public int? ThemeID { get; set; }
    [Column("POINTS")]
    public int? Points { get; set; }
    [Column("LEVELS")]
    public int? Levels { get; set; }
    [Column("FOLLOWNUM")]
    public int? FollowNum { get; set; }


    public User()
    {
        Professional = "NONE";
        Avatar = "https://img-qn.51miz.com/Element/00/88/59/99/8de317d3_E885999_245f75fb.png!/quality/90/unsharp/true/compress/true/format/png/fw/720";
        FollowerNum = 0;
        ThemeID = 1;  // 默认主题
        Points=0;
        Levels=0;
    }
}