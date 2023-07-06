using Microsoft.AspNetCore.Mvc;
using auth.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

[ApiController]
[Route("api/forum/[controller]")]  // RESTful 风格
public class LoadArticleController : ControllerBase  // 命名规范，继承自 ControllerBase 类的类名必须与 Controller 结尾
{
    private readonly AppDbContext _database;

    public LoadArticleController(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作。
    }

    [HttpGet]
    public async Task<IActionResult> GetArticleByTagAsync(string tag)
    {
        var code = 400;
        var msg = "success";

        // 根据tag的值，筛选不同的文章
        if (tag != "中餐" && tag != "西餐" && tag != "甜点" && tag != "其他")
        {
            code = 400;
            msg = "不存在该板块";
            return Ok(new
            {
                code = code,
                msg = msg,
            });
        }

        code = 200;
        var article_data = await _database.Articles
            .Where(a => a.Tag == tag)
            .Join(_database.Users,
                article => article.AuthorId,
                user => user.UserId,
                (article, user) => new
                {
                    ID=article.PostId,//文章ID
                    TAG = article.Tag,  // 文章标签
                    Title = article.Title,  // 文章标题
                    Views = article.Views,  // 文章浏览量
                    FavouriteNum = article.FavouriteNum,  // 文章收藏量
                    LikeNum = article.LikeNum,  // 文章点赞量
                    AuthorName = user.UserName, // 包含作者的名字
                    preContent = (article.Content ?? "").Substring(0, Math.Min(30, (article.Content ?? "").Length))  // 前30个汉字
                })
            .ToListAsync();


        return Ok(new
        {
            code = code,
            msg = msg,
            data = article_data
        });
    }
}
