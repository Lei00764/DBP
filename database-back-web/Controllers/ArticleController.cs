using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

<<<<<<< HEAD
=======
using auth.Database;
>>>>>>> main

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
    public async Task<IActionResult> GetArticleByTagAsync(string tag, int page_num)
    {
        var code = 400;
        var msg = "success";

        // 根据 tag 的值，筛选不同的文章
        if (tag != "中餐" && tag != "西餐" && tag != "甜点" && tag != "其他")
        {
            code = 400;
            msg = "不存在该板块";
            return BadRequest(new
            {
                code = code,
                msg = msg
            });
        }

        code = 200;
        var page_size = 10;  // 每一页所容纳的数据量

        var article_data = await (
            from article in _database.Articles
            join user in _database.Users on article.AuthorId equals user.UserId
            where article.Tag == tag
            select new
            {
                ID = article.PostId,//文章ID
                TAG = article.Tag,  // 文章标签
                Title = article.Title,  // 文章标题
                Views = article.Views,  // 文章浏览量
                FavouriteNum = article.FavouriteNum,  // 文章收藏量
                LikeNum = article.LikeNum,  // 文章点赞量
                AuthorName = user.UserName, // 包含作者的名字
                Content = article.Content,  // 文章内容
                IsBanned = article.IsBanned  // 是否被封禁
            }
        ).ToListAsync();


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
        var code = 200;
        var msg = "success";
        var temp = await _database.Articles.ToListAsync();
        bool exist = false;
        if (temp != null)//判断表内是否有改文章
        {
            foreach (var article in temp)
            {
                if (article.PostId == article_id)
                {
<<<<<<< HEAD
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
        async void UpdateData()//更新数据库数据的规范写法
        {
            //需要先查询
            var a = _database.Articles.Where(x => x.PostId == article_id);
            //再对查询到的数据进行修改
            foreach (var item in a)
            {
                item.Views+=1;
=======
                    exist = true;
                    break;
                }
>>>>>>> main
            }
        }
        if (exist)
        {
            var article_data = await _database.Articles
                .Where(a => a.PostId == article_id)
                .Join(_database.Users,
                    article => article.AuthorId,
                    user => user.UserId,
                    (article, user) => new
                    {
                        ID = article.PostId,//文章ID
                        TAG = article.Tag,  // 文章标签
                        Title = article.Title,  // 文章标题
                        Views = article.Views,  // 文章浏览量
                        FavouriteNum = article.FavouriteNum,  // 文章收藏量
                        LikeNum = article.LikeNum,  // 文章点赞量
                        AuthorName = user.UserName, // 包含作者的名字
                        Content = article.Content,  // 文章内容
                        IsBanned = article.IsBanned
                    })
                .ToListAsync();

            if (article_data[0].IsBanned != 0)
            {
                code = 400;
                msg = "该文章已被封禁";
                return BadRequest(new
                {
                    code = code,
                    msg = msg
                });
            }
            async void UpdateData()
            {
                //需要先查询
                var a = _database.Articles.Where(x => x.PostId == article_id);
                //再对查询到的数据进行修改
                foreach (var item in a)
                {
                    item.Views += 1;
                }
                //再save更改
                await _database.SaveChangesAsync();

            }
            UpdateData();
            return Ok(new
            {
                code = code,
                msg = msg,
                data = article_data
            });
        }
        else
        {
            code = 400;
            msg = "不存在该文章";
            return BadRequest(new
            {
                code = code,
                msg = msg
            });
        }
        //!!!!!!TODO：更新Views!!!!!!!!!
    }

}
