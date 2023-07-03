using System.ComponentModel.DataAnnotations;  // for Key
using System.ComponentModel.DataAnnotations.Schema;  // for Column

namespace auth.Models;

[Table("T_USER")]
public class User
{
    // 请与数据库中表字段保持一致
    [Key]
    [Column("USERID")]
    public int UserId { get; set; }
    [Column("USERNAME")]
    public string? UserName { get; set; }
    [Column("PASSWORD")]
    public string? PassWord { get; set; }
    [Column("AVATAR")]
    public string? Avatar {get;set;}
    [Column("PROFESSIONAL")]
    public string? Professional {get;set;}
    [Column("SIGNATURE")]
    public string? Signature {get;set;}
    [Column("EMAIL")]
    public string? Email {get;set;}
    [Column("TEL")]
    public string? Tel {get;set;}
    [Column("FOLLOWERNUM")]
    public int FollowerNum {get;set;}
    [Column("THEMEID")]
    public int ThemeID {get;set;}
    

}