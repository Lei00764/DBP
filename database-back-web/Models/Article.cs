using System.ComponentModel.DataAnnotations;  // for Key
using System.ComponentModel.DataAnnotations.Schema;  // for Column

namespace auth.Models;

[Table("ARTICLE")]
public class Article
{
    // 请与数据库中表字段保持一致
    [Key]
    [Column("POSTID")]
    public int? PostId { get; set; }
    [Column("TAG")]
    public string? Tag { get; set; }
    [Column("TITLE")]
    public string? Title { get; set; }
    [Column("CONTENT")]
    public string? Cotent { get; set; }
    [Column("AUTHORID")]
    public int? AuthorId { get; set; }
    [Column("PICTURE")]
    public string? Picture { get; set; }
    [Column("VIEWS")]
    public int? Views { get; set; }
    [Column("FAVOURITENUM")]
    public int? FavouriteNum { get; set; }
    [Column("LIKENUM")]
    public int? LikeNum { get; set; }
    [Column("ISBANNED")]
    public int? IsBanned { get; set; }
    [Column("SHARELINK")]
    public string? ShareLink { get; set; }

    public Article()
    {//设置默认值（初始值）
        Views = 0;
        FavouriteNum=0;
        LikeNum=0;
        IsBanned=0;
    }
}