using auth.Models;
using Microsoft.EntityFrameworkCore;

// 创建数据库上下文：
// 数据库上下文类是为给定数据模型协调 EF Core 功能的主类。 
// 上下文派生自 Microsoft.EntityFrameworkCore.DbContext。 
// 上下文指定数据模型中包含哪些实体。

namespace auth.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }  // 对应 Models/User.cs
    public DbSet<Administrator> Administrators { get; set; }  // 对应 Models/Administrator.cs
    public DbSet<Article> Articles { get; set; }  // 对应 Models/Article.cs
    //public DbSet<Article> Tags { get; set; }  // 对应 Models/Tag.cs
    public DbSet<Like> Likes { get; set; }  // 对应 Models/Like.cs
    public DbSet<Follow> Follows { get; set; }  // 对应 Models/Follow.cs
    public DbSet<Profession> Professions { get; set; }  // 对应 Models/Profession.cs
    public DbSet<ReportPost> ReportPost { get; set; }  // 对应 Models/ReportPost.cs
    public DbSet<Favourite> Favourites { get; set; }  // 对应 Models/Favourite.cs
    public DbSet<Comment> Comments { get; set; }  // 对应 Models/Comment.cs
    public DbSet<ReportComments> ReportComments { get; set; }  // 对应 Models/ReportComments.cs
    public DbSet<Announcement> Announcements { get; set; }  // 对应 Models/Announcement.cs
    public DbSet<Notice> Notices { get; set; }  // 对应 Models/Notice.cs
    public DbSet<SearchHistory> SearchHistories { get; set; }  // 对应 Models/SearchHistory.cs
}