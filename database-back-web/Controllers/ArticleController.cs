using Microsoft.AspNetCore.Mvc;
using auth.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]  // RESTful 风格
public class ArticleController : ControllerBase  // 命名规范，继承自 ControllerBase 类的类名必须与 Controller 结尾
{
    private AppDbContext _database;

    public ArticleController(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作
    }

    [HttpGet("loadArticle")]
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

    [HttpGet("viewArticle/{article_id}")]
    public async Task<IActionResult> GetArticleDetailsAsync(int article_id)
    {
<<<<<<< HEAD
        var code = 200;
        var msg = "success";
        var temp=await _database.Articles.ToListAsync();
        bool exist=false;
        if(temp!=null)//判断表内是否有改文章
        {
        foreach (var article in temp)
            {
               if(article.PostId==article_id)
               {
                exist=true;
                break;
               }
            }
        }
        if(exist){
        var article_data = await _database.Articles
            .Where(a => a.PostId == article_id)
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
                    Content = article.Content,  // 文章内容
                    IsBanned=article.IsBanned
                })
            .ToListAsync();
        
        if(article_data[0].IsBanned!=0)
        {
            code=400;
            msg="该文章已被封禁";
            return BadRequest(new
            {
                code=code,
                msg=msg
            });
        }
=======
        var code = 400;
        var msg = "success";


>>>>>>> main
        return Ok(new
        {
            code = code,
            msg = msg,
<<<<<<< HEAD
            data=article_data


        });
        }
        else
        {
            code=400;
            msg="不存在该文章";
            return BadRequest(new
            {
                code=code,
                msg=msg
            });
        }
        //!!!!!!TODO：更新Views!!!!!!!!!
=======
        });
>>>>>>> main
    }

}
